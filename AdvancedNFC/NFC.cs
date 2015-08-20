using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedNFC
{
    public class NFC : OneSheeldUser, IOneSheeldSketch, 
        INFCNewTagScanned, 
        INFCError, 
        IRecordParsedCallback
    {
        string redString = "red";
        string greenString = "green";
        string blueString = "blue";

        OutputPort red = null;
        OutputPort green = null;
        OutputPort blue = null;

        public void Setup()
        {
            red = new OutputPort(Pins.GPIO_PIN_D8, false);
            green = new OutputPort(Pins.GPIO_PIN_D9, false);
            blue = new OutputPort(Pins.GPIO_PIN_D10, false);

            OneSheeld.begin();
            NFC.setOnNewTagScanned(this);
            NFC.setOnError(this);
        }

        public void Loop()
        {
            // Leave Empty
        }

        public void OnNewTagScanned(NFCTag tag)
        {
          /* Create and object of type NFCRecord. */
          NFCRecord myRecordNumber0 = tag.getRecord(0);
          /* Subscribe to record parsed data response event. */
          tag.setOnRecordParsedDataResponse(this);
          /* Check if there's a record with number 0 in Tag. */
          if(!myRecordNumber0.isNull())
          {
            /* Parse and query the data in the first record. */
            myRecordNumber0.queryParsedData();  
          }
          else
          {
            /* Print out no record found. */
            TERMINAL.println("No Record found"); 
          }
        }

        /* A function to be called once a new parsed data is received. */
        public void OnRecordParsed(byte id, byte[] data)
        {
          byte[] redArray = System.Text.Encoding.UTF8.GetBytes(redString);
          byte[] blueArray = System.Text.Encoding.UTF8.GetBytes(blueString);
          byte[] greenArray = System.Text.Encoding.UTF8.GetBytes(greenString);

          /* Check response and compare data. */
          if(data.Equals(redArray))
          {
            TERMINAL.println("Red");
            red.Write(true);
            green.Write(false);
            blue.Write(false);
          }
          else if(data.Equals(blueArray))
          {
            TERMINAL.println("blue");
            red.Write(false);
            green.Write(false);
            blue.Write(true);
          }
          else if(data.Equals(greenArray))
          {
            TERMINAL.println("green");
            red.Write(false);
            green.Write(true);
            blue.Write(false);
          }
        }

        public void OnError(byte errorNumber)
        {
            switch (errorNumber)
            {
                case NFCShield.INDEX_OUT_OF_BOUNDS: TERMINAL.println("INDEX_OUT_OF_BOUNDS"); break;
                case NFCShield.RECORD_CAN_NOT_BE_PARSED: TERMINAL.println("RECORD_CAN_NOT_BE_PARSED"); break;
                case NFCShield.TAG_NOT_SUPPORTED: TERMINAL.println("TAG_NOT_SUPPORTED"); break;
                case NFCShield.NO_ENOUGH_BYTES: TERMINAL.println("NO_ENOUGH_BYTES"); break;
                case NFCShield.TAG_READING_ERROR: TERMINAL.println("TAG_READING_ERROR"); break;
                case NFCShield.RECORD_NOT_FOUND: TERMINAL.println("RECORD_NOT_FOUND"); break;
            }
        }
    
    }
}
