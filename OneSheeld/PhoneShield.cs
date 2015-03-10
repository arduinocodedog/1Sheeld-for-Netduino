using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class PhoneShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        bool ringing = false;
        string number = null;
        bool isCallBackAssigned = false;
        IPhoneCallback changeCallBack = null;

        public PhoneShield(OneSheeld onesheeld)
            : base(onesheeld, (byte)ShieldIds.PHONE_ID)
        {
            Sheeld = onesheeld;
        }

        public void call(string phone)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(phone.Length, System.Text.Encoding.UTF8.GetBytes(phone));

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.PHONE_ID, 0, PHONE_CALL, 1, args);
        }

        public bool isRinging()
        {
            return ringing;
        }

        public string getNumber()
        {
            return number;
        }

        public void setOnCallStatusChange(IPhoneCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }

        public override void processData()
        {
     	    //Checking Function-ID
	        byte x= Sheeld.getFunctionId();

	        if (x == PHONE_IS_RINGING)
	        {
                ringing = (Sheeld.getArgumentData(0)[0] != 0x00);
	        }

	        else if (x == PHONE_GET_NUMBER)
	        {
		        if(number != null)
			        number = "";
		
		        byte length=Sheeld.getArgumentLength(0);
		
		        for (int i=0; i< length; i++)
			        number += Convert.ToChar(Sheeld.getArgumentData(0)[i]);

		        //Users Function Invoked
		        if (isCallBackAssigned)
		        {
			        changeCallBack.OnCallStatusChange(ringing,number);
		        }
            }
        }

        //Output Function ID
        const byte PHONE_CALL = 0x01;

        //Input Function ID's
        const byte PHONE_IS_RINGING = 0x01;
        const byte PHONE_GET_NUMBER = 0x02;
    }
}
