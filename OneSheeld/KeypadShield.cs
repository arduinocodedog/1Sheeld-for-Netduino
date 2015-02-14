using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class KeypadShield
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        IRowColCallback changeCallBack = null;

        byte row = 0;
        byte col = 0;

        public KeypadShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
        }

        public void processData()
        {
            byte functionID = Sheeld.getFunctionId();

            if (functionID == KEYPAD_VALUE)
            {
                row = Sheeld.getArgumentData(0)[0];
                col = Sheeld.getArgumentData(1)[0]; 
                if (isCallBackAssigned)
                {
                    byte bitrow = (byte) findbitposition(row);
                    byte bitcol = (byte) findbitposition(col);
                    changeCallBack.OnChange(bitrow, bitcol);
                }
            }
        }

        //Row Checker 
        public bool isRowPressed(byte x)
        {
            if (x > 7)
                return false;
            return (row & (1 << x)) > 0;
        }

        //Column Checker
        public bool isColumnPressed(byte x)
        {
            if (x > 7)
                return false;
            return (col & (1 << x)) > 0;
        }

        //AnyRow Checker
        public bool isAnyRowPressed()
        {
            return row > 0;
        }

        //AnyColumn Checker
        public bool isAnyColumnPressed()
        {
            return col > 0;
        }

        public void setOnButtonChange(IRowColCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }

        int findbitposition(int val)
        {
            int bitpos = 0;

            while (val > 0)
            {
                val = val >> 1;
                bitpos++;
            }

            return bitpos - 1;
        }

        const byte KEYPAD_VALUE = 0x01;
    }
}
