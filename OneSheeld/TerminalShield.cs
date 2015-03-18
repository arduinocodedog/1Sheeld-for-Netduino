using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class TerminalShield : OneSheeldPrintln
    {
        CircularBuffer buffer = null;

        public TerminalShield()
            : base(ShieldIds.TERMINAL_ID, TERMINAL_WRITE, TERMINAL_PRINT)
        {
            buffer = new CircularBuffer(64);
        }

        //Read from Android
        public sbyte read()
        {
	        if(buffer.remain <= 0)
                return -1;

	        return (sbyte) buffer.pop();
        }

        //Flush buffer contents
        public void flush()
        {
            buffer.Clear();
        }

        //Check Data available in Buffer
        public int available()
        {
	        return buffer.remain;
        }

        //Read bytes from Buffer
        public int readBytes(sbyte[] arr, int length)
        {
	        int count = 0;
	 	    while (count < length) 
            {
	            sbyte c = read();
	            if (c < 0) 
                    break;
	            arr[count] = c;
	            count++;
 	        }
            return count;
        }

        //Terminal Incomming Data processing
        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();
            byte dataLength = getOneSheeldInstance().getArgumentLength(0);
	        if(functionID == TERMINAL_READ)
	        {
			    for (int j=0; j < dataLength; j++)
			    {
                    buffer.push((sbyte)getOneSheeldInstance().getArgumentData(0)[j]);
			    }
	        }
        }

        //Output Function ID's
        const byte TERMINAL_WRITE = 0x01;		
        const byte TERMINAL_PRINT = 0x02;

        //Input Function ID
        const byte TERMINAL_READ = 0x01;
    }
}
