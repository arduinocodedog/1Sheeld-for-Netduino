using OneSheeldClasses;

namespace SimpleNFC
{
    public class NFCCallback : OneSheeldUser, IOneSheeldSketch, 
        INFCNewTagScanned
    {
        NFCTag myTag = null;
        bool newTagScanned = false;

        public void Setup()
        {
            OneSheeld.begin();

            NFC.setOnNewTagScanned(this);
        }

        public void Loop()
        {
            /* Check if there's a new tag scanned. */
            if (newTagScanned)
            {
                /* Print all the data about the scanned tag. */
                TERMINAL.print("Number of Records in Tag = ");
                /* Print number of records. */
                TERMINAL.println(myTag.getNumberOfRecords());
                /* Print tag used size. */
                TERMINAL.print("Used Tag Size = ");
                TERMINAL.println(myTag.getSize());
                /* Print tag maximum size. */
                TERMINAL.print("Maximum Tag Size = ");
                TERMINAL.println(myTag.getMaxSize());
                TERMINAL.print("Tag id length = ");
                TERMINAL.println(myTag.getIdLength());

                switch (myTag.getRecord(0).getTypeCategory())
                {
                    case NFCRecord.TNF_UNKNOWN: TERMINAL.println("TNF_UNKNOWN"); break;
                    case NFCRecord.TNF_EMPTY: TERMINAL.println("TNF_EMPTY"); break;
                    case NFCRecord.TNF_EXTERNAL_TYPE: TERMINAL.println("TNF_EXTERNAL_TYPE"); break;
                    case NFCRecord.TNF_MIME_MEDIA: TERMINAL.println("TNF_MIME_MEDIA"); break;
                    case NFCRecord.TNF_UNCHANGED: TERMINAL.println("TNF_UNCHANGED"); break;
                    case NFCRecord.TNF_ABSOLUTE_URI: TERMINAL.println("TNF_ABSOLUTE_URI"); break;
                    case NFCRecord.RTD_TEXT: TERMINAL.println("RTD_TEXT"); break;
                    case NFCRecord.RTD_URI: TERMINAL.println("RTD_URI"); break;
                    case NFCRecord.RTD_UNSUPPORTED: TERMINAL.println("RTD_UNSUPPORTED"); break;
                }
                
                newTagScanned = false;
            }
        }

        public void OnNewTagScanned(NFCTag tag)
        {
            if (myTag != null) 
                myTag = null;

            myTag = tag;

            newTagScanned = true;
        }
    }
}
