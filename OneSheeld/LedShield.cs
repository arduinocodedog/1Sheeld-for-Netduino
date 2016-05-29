namespace OneSheeldClasses
{
    public class LedShield : ShieldParent
    {
        //Setter 
        void setValue(byte data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LED_ID,0,LED_SET_VALUE,1,args);
        }

        // ----------------------  Public Methods -----------------------

        public LedShield()
            : base(ShieldIds.LED_ID)
        {
        }

        //LedOff Setter
        public void setLow()
        {
            setValue(LED_SET_LOW);
        }

        //LedOn Setter
        public void setHigh()
        {
            setValue(LED_SET_HIGH);
        }

        //Output Functions ID's
        const byte LED_SET_VALUE = 0x01;

        //Parameters
        const byte LED_SET_LOW = 0x00;
        const byte LED_SET_HIGH = 0x01;
    }
}
