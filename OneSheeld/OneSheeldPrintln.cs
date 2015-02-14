using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class OneSheeldPrintln : OneSheeldPrint
    {
        public OneSheeldPrintln(OneSheeld onesheeld, ShieldIds shid, byte writefnid, byte printfnid) 
            : base(onesheeld, shid, writefnid, printfnid) { }
        
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
        public void println(int data, byte b)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print unsigned integer with newline
        public void println(uint data, byte b)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print long with newline
        public void println(long data, byte b)
        {
            string buffer = data.ToString() + "\r\n";
            print(buffer);
        }

        //print unsigned long with newline
        public void println(ulong data, byte b)
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

        //print double with newline
        public void println(double data, int precision)
        {
            string buffer = Round(data, precision) + "\r\n";
            print(buffer);
        }

        //print float with newline
        public void println(float data, int precision)
        {
            string buffer = Round(data, precision) + "\r\n";
            print(buffer);
        }
    }
}
