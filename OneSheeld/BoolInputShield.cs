using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class BoolInputShield : ShieldParent    
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
        }

        public override void processData()
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
