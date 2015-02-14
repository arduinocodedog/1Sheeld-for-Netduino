using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class BoolInputShield
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        IBoolCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;

        protected byte value = 0x00;

        public BoolInputShield(OneSheeld onesheeld, byte funcid)
        {
            Sheeld = onesheeld;
            ShieldFunctionID = funcid;
        }

        public void processData()
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
