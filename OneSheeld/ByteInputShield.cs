namespace OneSheeldClasses
{
    public class ByteInputShield : ShieldParent
    {
        bool isCallBackAssigned = false;
        IByteCallback changeCallBack = null;
        byte ShieldFunctionID = 0x00;
        ShieldIds ShieldID = 0x00;

        protected byte value = 0x00;

        public ByteInputShield(byte funcid, ShieldIds shieldid)
            : base(shieldid)
        {
            ShieldFunctionID = funcid;
            ShieldID = shieldid;
         }

        public byte getValue()
        {
            return value;
        }

        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();

            if (functionID == ShieldFunctionID)
            {
                value = 0;
                value = getOneSheeldInstance().getArgumentData(0)[0];
                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    changeCallBack.OnChange(value);
                    exitingACallback();
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
