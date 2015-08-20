namespace OneSheeldClasses
{
    public class OneSheeldPrintln : OneSheeldPrint
    {
        public OneSheeldPrintln(ShieldIds shid, byte writefnid, byte printfnid) 
            : base(shid, writefnid, printfnid) { }
        
        //print newline
        public void println()
        {
            string buffer = "\r\n";
	        print(buffer);
        }

        //print character with newline
        public void println(char data)
        {
            string buffer = data.ToString() + "\r\n";
	        print(buffer);
        }

        //print unsigned byte with newline
        public void println(sbyte data)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print integer with newline
        public void println(int data, byte b = DEC)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print unsigned integer with newline
        public void println(uint data, byte b = DEC)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print long with newline
        public void println(long data, byte b = DEC)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print unsigned long with newline
        public void println(ulong data, byte b = DEC)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print string with newline
        public void println(string data)
        {
            string buffer = data + "\r\n";
            print(buffer);
        }

        // print byte array with newline
        public void println(byte[] data)
        {
            byte[] buffer = null;
            int Length = 0;
            if (data != null)
            {
                Length = data.Length;
                buffer = new byte[data.Length + 2];
                for (int i = 0; i < data.Length; i++)
                    buffer[i] = data[i];
            }
            else
                buffer = new byte[2];
            buffer[Length] = (byte)'\r';
            buffer[Length + 1] = (byte)'\n';

            print(buffer);
        }

        //print double with newline
        public void println(double data, int precision = 3)
        {
            string buffer = Round(data, precision) + "\r\n";
            print(buffer);
        }

        //print float with newline
        public void println(float data, int precision = 3)
        {
            string buffer = Round(data, precision) + "\r\n";
            print(buffer);
        }

        const byte DEC = 10;
    }
}
