using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class SMSShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        string number = null;
        string text = null;

        bool isCallBackAssigned = false;
  	    bool isItNewSms = false;

        ISMSCallback changeCallBack = null;

        public SMSShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.SMS_ID)
        {
            Sheeld = onesheeld;
        }

        public void send(string number, string text)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(number.Length, System.Text.Encoding.UTF8.GetBytes(number));
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(text.Length, System.Text.Encoding.UTF8.GetBytes(text));
            args.Add(arg2);

            Sheeld.sendPacket(ShieldIds.SMS_ID, 0, SMS_SEND, 2, args);
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
