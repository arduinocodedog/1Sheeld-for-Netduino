using System;

namespace OneSheeldClasses
{
    public class FunctionArg
    {
        int length = 0;
        byte[] data = null;

        public FunctionArg(byte d)
        {
            byte[] datas = new byte[1];
            datas[0] = d;

            _FunctionArg(1, datas);
        }

        public FunctionArg(char c)
        {
            byte[] datas = new byte[1];
            datas[0] = (byte) c;

            _FunctionArg(1, datas);
        }

        public FunctionArg(sbyte sb)
        {
            byte[] datas = new byte[1];
            datas[0] = (byte) sb;

            _FunctionArg(1, datas);
        }

        public FunctionArg(int i)
        {
            byte[] datas = new byte[2];
            datas[1] = (byte)((i >> 8) & 0xFF);
            datas[0] = (byte)(i & 0xFF);

            _FunctionArg(2, datas);
        }

        public FunctionArg(uint ui)
        {
            byte[] datas = new byte[2];
            datas[1] = (byte)((ui >> 8) & 0xFF);
            datas[0] = (byte)(ui & 0xFF);

            _FunctionArg(2, datas);
        }

        public FunctionArg(ushort us)
        {
            _FunctionArg(2, BitConverter.GetBytes(us));
        }

        public FunctionArg(bool b)
        {
            byte[] bArray = new byte[1];
            bArray[0] = (b) ? (byte)0x01 : (byte)0x00;

            _FunctionArg(1, bArray);
        }

        public FunctionArg(float f)
        {
            _FunctionArg(4, OneSheeldMain.OneSheeld.convertFloatToBytes(f));
        }

        public FunctionArg(string s)
        {
            _FunctionArg(s.Length, System.Text.Encoding.UTF8.GetBytes(s));
        }

        public FunctionArg(ulong ul)
        {
            byte[] ulongArray = new byte[4];
            ulongArray[0] = (byte)(ul & 0xFF);
            ulongArray[1] = (byte)((ul >> 8) & 0xFF);
            ulongArray[2] = (byte)((ul >> 16) & 0xFF);
            ulongArray[3] = (byte)((ul >> 24) & 0xFF);

            _FunctionArg(4, ulongArray);
        }

        public FunctionArg(long l)
        {
            byte[] longArray = new byte[4];
            longArray[0] = (byte)(l & 0xFF);
            longArray[1] = (byte)((l >> 8) & 0xFF);
            longArray[2] = (byte)((l >> 16) & 0xFF);
            longArray[3] = (byte)((l >> 24) & 0xFF);

            _FunctionArg(4, longArray);
        }

        public FunctionArg(byte[] d)
        {
            _FunctionArg(d.Length, d);
        }

        private void _FunctionArg(int len, byte[] d)
        {
            length = (len > 0xff) ? (byte)0xff : (byte)len;
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
