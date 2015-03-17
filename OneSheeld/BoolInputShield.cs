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
        ShieldIds ShieldID = 0x00;

        protected byte value = 0x00;

        public BoolInputShield(OneSheeld onesheeld, byte funcid, ShieldIds shieldid)
            : base(onesheeld, shieldid)
        {
            Sheeld = onesheeld;
            ShieldFunctionID = funcid;
            ShieldID = shieldid;
        }

        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();

            if (functionID == ShieldFunctionID)
            {
                value = getOneSheeldInstance().getArgumentData(0)[0];
                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    changeCallBack.OnChange(value > 0);
                    exitingACallback();
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
