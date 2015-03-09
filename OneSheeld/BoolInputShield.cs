using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class BoolInputShield : ShieldParent, IShieldChild
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        IBoolCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;
        byte ShieldID = 0x00;

        protected byte value = 0x00;

        public BoolInputShield(OneSheeld onesheeld, byte funcid, byte shieldid)
            : base(onesheeld, shieldid)
        {
            Sheeld = onesheeld;
            ShieldFunctionID = funcid;
            ShieldID = shieldid;

            SetChild(this);
        }

        void IShieldChild.processData()
        {
            byte functionID = Sheeld.getFunctionId();

            if (functionID == ShieldFunctionID)
            {
                value = Sheeld.getArgumentData(0)[0];
                if (isCallBackAssigned)
                {
                    changeCallBack.OnChange(value > 0);
                }
            }
        }

        public void setOnButtonStatusChange(IBoolCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }
    }
}
