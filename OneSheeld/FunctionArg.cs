using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class FunctionArg
    {
        int length = 0;
        byte[] data = null;

        public FunctionArg(int len, byte[] d)
        {
            length = (len > 0xff) ? (byte) 0xff : (byte) len;
            data = d;
        }

        public int getLength()
        {
            return length;
        }

        public byte[] getData()
        {
            return data;
        }
    }
}
