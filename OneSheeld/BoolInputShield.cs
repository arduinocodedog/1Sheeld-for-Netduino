namespace OneSheeldClasses
{
    public class BoolInputShield : ShieldParent    
    {
        bool isCallBackAssigned = false;
        IBoolCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;
        ShieldIds ShieldID = 0x00;

        protected byte value = 0x00;

        public BoolInputShield(byte funcid, ShieldIds shieldid)
            : base(shieldid)
        {
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
