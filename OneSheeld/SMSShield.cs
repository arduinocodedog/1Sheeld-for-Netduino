namespace OneSheeldClasses
{
    public class SMSShield : ShieldParent
    {
        string number = null;
        string text = null;

        bool isCallBackAssigned = false;
  	    bool isItNewSms = false;

        ISMSCallback changeCallBack = null;

        public SMSShield()
            : base(ShieldIds.SMS_ID)
        {
        }

        public void send(string number, string text)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(number);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(text);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.SMS_ID, 0, SMS_SEND, 2, args);
        }

        //Number Getter
        public string getNumber()
        {
	        isItNewSms = false;
	        return number;
        }

        //SMS Getter
        public string getSms()
        {
	        isItNewSms = false;
	        return text;
        }

        public bool isNewSms()
        {
	        return isItNewSms;
        }

        public override void processData()
        {
          	//Checking Function-ID
            byte x = getOneSheeldInstance().getFunctionId();

            if (x == SMS_GET)
            {
                isItNewSms = true;

                if (text != null)
                {
                    text = null;
                }

                if (number != null)
                {
                    number = null;
                }

                int numberlength = getOneSheeldInstance().getArgumentLength(0);
                number = "";
                for (int j = 0; j < numberlength; j++)
                {
                    number += getOneSheeldInstance().getArgumentData(0)[j];
                }

                int textlength = getOneSheeldInstance().getArgumentLength(1);
                text = "";

                for (int i = 0; i < textlength; i++)
                {
                    text += getOneSheeldInstance().getArgumentData(1)[i];
                }

                //Users Function Invoked
                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    changeCallBack.OnSMSReceive(number, text);
                    exitingACallback();
                }
            }
        }

        public void setOnSmsReceive(ISMSCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }

        //Output Function ID
        const byte SMS_SEND = 0x01;

        //Input Functions ID's  
        const byte SMS_GET = 0x01;
    }
}
