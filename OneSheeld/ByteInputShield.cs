using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ByteInputShield : ShieldParent, IShieldChild
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        IByteCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;
        byte ShieldID = 0x00;

        protected byte value = 0x00;

        public ByteInputShield(OneSheeld onesheeld, byte funcid, byte shieldid)
            : base(onesheeld, shieldid)
        {
            Sheeld = onesheeld;
            ShieldFunctionID = funcid;
            ShieldID = shieldid;

            SetChild(this);
         }

        public byte getValue()
        {
            return value;
        }

        void IShieldChild.processData()
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
