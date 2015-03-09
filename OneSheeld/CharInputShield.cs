using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class CharInputShield : ShieldParent, IShieldChild
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        ICharCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;
        byte ShieldID = 0x00;

        protected char character = (char) 0;

        public CharInputShield(OneSheeld onesheeld, byte funcid, byte shieldid)
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
                character = (char) 0;
                character = (char) Sheeld.getArgumentData(0)[0];
                if (isCallBackAssigned)
                {
                    changeCallBack.OnChange(character);
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
