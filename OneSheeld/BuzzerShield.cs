namespace OneSheeldClasses
{
    public class BuzzerShield : ShieldParent
    {
        bool buzzing = false;

        //Buzz Setter 
        void setValue(byte data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.BUZZER_ID,0,BUZZER_SET,1,args);
        }

        // ----------------------  Public Methods -----------------------

        //Constructor
        public BuzzerShield()
            :base(ShieldIds.BUZZER_ID)
        {
        }

        //Buzzing On
        public void buzzOn()
        {
            if (!buzzing)
            {
                setValue(BUZZER_ON);
                buzzing = true;
            }
        }

        //Buzzing Off
        public void buzzOff()
        {
            if (buzzing)
            {
                setValue(BUZZER_OFF);
                buzzing = false;
            }
        }

        //Output Functions ID's
        const byte LED_SET_VALUE = 0x01;

        //Ouput Functions ID's 
        const byte BUZZER_SET = 0x01;
        //Parameters
        const byte BUZZER_OFF = 0x00;
        const byte BUZZER_ON = 0x01;
    }
}
