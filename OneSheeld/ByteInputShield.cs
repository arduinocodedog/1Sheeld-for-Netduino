using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ByteInputShield
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        IByteCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;

        protected byte value = 0x00;

        public ByteInputShield(OneSheeld onesheeld, byte funcid)
        {
            Sheeld = onesheeld;
            ShieldFunctionID = funcid;
         }

        public byte getValue()
        {
            return value;
        }

        public void processData()
        {
            byte functionID = Sheeld.getFunctionId();

            if (functionID == ShieldFunctionID)
            {
                value = 0;
                value = Sheeld.getArgumentData(0)[0];
                if (isCallBackAssigned)
                {
                    changeCallBack.OnChange(value);
                }
            }
        }

        public void setOnValueChange(IByteCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }
    }
}
