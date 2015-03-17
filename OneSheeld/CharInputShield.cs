using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class CharInputShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        ICharCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;
        ShieldIds ShieldID = 0x00;

        protected char character = (char) 0;

        public CharInputShield(OneSheeld onesheeld, byte funcid, ShieldIds shieldid)
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
                character = (char) 0;
                character = (char) getOneSheeldInstance().getArgumentData(0)[0];
                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    changeCallBack.OnChange(character);
                    exitingACallback();
                }
            }
        }

        public void setOnValueChange(ICharCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }
    }
}
