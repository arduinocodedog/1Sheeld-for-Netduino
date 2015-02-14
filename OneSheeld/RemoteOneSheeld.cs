using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class RemoteOneSheeld
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        bool isSubscribeAssigned = false;
        IRemoteShieldCallback remoteCallBack = null;
        ISubscribeCallback subscribeCallBack = null;
        string remoteOneSheeldAddress = "";
        
        public RemoteOneSheeld(OneSheeld onesheeld, string address)
        {
            Sheeld = onesheeld;
            remoteOneSheeldAddress = address;
        }

        //Setting Pins mode
        public void pinMode(byte pinNumber, byte pinDirection)
        {
	        pinNumber = checkAnalogPinNumbers(pinNumber);
	
	        if((pinNumber>= 0||pinNumber <= 19) && (pinDirection == 0 || pinDirection == 1))
	        {
                ArrayList args = new ArrayList();

                FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

                args.Add(arg1);

                byte[] pn = new byte[1];
                pn[0] = pinNumber;

                FunctionArg arg2 = new FunctionArg(1, pn);

                args.Add(arg2);

                byte[] pd = new byte[1];
                pd[0] = pinDirection;

                FunctionArg arg3 = new FunctionArg(1, pd);

                args.Add(arg3);

		        Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID,0,REMOTEONESHEELD_PIN_MODE,3,args);
	        }
        }

        //Setting digital pin state
        public void digitalWrite(byte pinNumber, byte pinValue)
        {
            pinNumber = checkAnalogPinNumbers(pinNumber);

            if ((pinNumber >= 0 || pinNumber <= 19) && (pinValue == 0 || pinValue == 1))
            {
                ArrayList args = new ArrayList();

                FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

                args.Add(arg1);

                byte[] pn = new byte[1];
                pn[0] = pinNumber;

                FunctionArg arg2 = new FunctionArg(1, pn);

                args.Add(arg2);

                byte[] pv = new byte[1];
                pv[0] = pinValue;

                FunctionArg arg3 = new FunctionArg(1, pv);

                args.Add(arg3);

                Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_WRITE, 3, args);
            }
        }

        //Setting analog pin status
        public void analogWrite(byte pinNumber, int pinValue)
        {
            byte tempValue = 0x00;
            pinNumber = checkAnalogPinNumbers(pinNumber);

            if (pinValue < 0)
                pinValue = 0;
            if (pinValue > 255)
                pinValue = 255;

            tempValue = (byte) pinValue;

            if ((pinNumber == 5 || pinNumber == 6 || pinNumber == 10 || pinNumber == 11) || (pinNumber >= 14 && pinNumber <= 19))
            {
                ArrayList args = new ArrayList();

                FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

                args.Add(arg1);

                byte[] pn = new byte[1];
                pn[0] = pinNumber;

                FunctionArg arg2 = new FunctionArg(1, pn);

                args.Add(arg2);

                byte[] pv = new byte[1];
                pv[0] = tempValue;

                FunctionArg arg3 = new FunctionArg(1, pv);

                args.Add(arg3);

                Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_ANALOG_WRITE, 3, args);
            }
        }

        //Getting digital/analog pin status
        public void digitalRead(byte pinNumber)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn = new byte[1];
            pn[0] = pinNumber;

            FunctionArg arg2 = new FunctionArg(1, pn);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_READ, 2, args);
        }

        //Sending Message Remotely
        public void sendMessage(string key, float value)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(key.Length, System.Text.Encoding.UTF8.GetBytes(key));

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(sizeof(float), Sheeld.convertFloatToBytes(value));

            args.Add(arg3);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SEND_FLOAT, 3, args);
        }

        public void sendMessage(string key, string stringData)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(key.Length, System.Text.Encoding.UTF8.GetBytes(key));

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(stringData.Length, System.Text.Encoding.UTF8.GetBytes(stringData));

            args.Add(arg3);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SEND_STRING, 3, args);
        }

        public void processData()
        {
            string remoteAddress = "";
            for (int i = 0; i < 36; i++)
                remoteAddress += Convert.ToChar(Sheeld.getArgumentData(0)[i]);

            if (!remoteOneSheeldAddress.Equals(remoteAddress))
                return;

	        byte functionId = Sheeld.getFunctionId();

	        if(functionId == DIGITAL_SUBSCRIBE_VALUE)
	        {
		        int argumentNo = Sheeld.getArgumentNo();
		        byte pinData;
		        byte pinNo;
		        bool pinValue;

		        for (int i=1 ; i <argumentNo; i++)
		        {
			        pinData = Sheeld.getArgumentData((byte)i)[0];
			        pinNo = (byte) (pinData & 0x7F);
			        pinValue = (pinData >> 7) != 0x00;

                    if (isSubscribeAssigned)
			            subscribeCallBack.OnSubscribeOrDigitalChange(pinNo,pinValue);
		        }
	        }
	        else if(functionId == READ_MESSAGE_FLOAT)
	        {
                string floatKey = "";
                int keyLength = Sheeld.getArgumentLength(1);
                for (int i = 0; i < keyLength; i++)
                    floatKey += Convert.ToChar(Sheeld.getArgumentData(1)[i]);

                float incommingFloatValue = Sheeld.convertBytesToFloat(Sheeld.getArgumentData(2));

    	        if(isCallBackAssigned)
    	        {
    		        remoteCallBack.OnNewMessage(floatKey,incommingFloatValue);	
    	        }

	        }
	        else if(functionId == READ_MESSAGE_STRING )
	        {
                string stringKey = "";
                int keyLength = Sheeld.getArgumentLength(1);
                for (int i = 0; i < keyLength; i++)
                    stringKey += Convert.ToChar(Sheeld.getArgumentData(1)[i]);

                string incommingStringData = "";
                int stringDataLength = Sheeld.getArgumentLength(2);
                for (int i = 0; i < stringDataLength; i++)
                    incommingStringData += Convert.ToChar(Sheeld.getArgumentData(2)[i]);

    	        if(isCallBackAssigned)
    	        {
    		        remoteCallBack.OnNewMessage(stringKey,incommingStringData);	
    	        }
        	}
        }

        public byte checkAnalogPinNumbers(byte pinNumber)
        {
            switch (pinNumber)
            {
                case 0xA0: pinNumber = 14; break;
                case 0xA1: pinNumber = 15; break;
                case 0xA2: pinNumber = 16; break;
                case 0xA3: pinNumber = 17; break;
                case 0xA4: pinNumber = 18; break;
                case 0xA5: pinNumber = 19; break;
                default: return pinNumber;
            }

            return pinNumber;
        }

        public void setOnNewMessage(IRemoteShieldCallback userCallback)
        {
            remoteCallBack = userCallback;
            isCallBackAssigned = true;
        }

        public void setOnSubsribeOrDigitalChange(ISubscribeCallback userCallback)
        {
            subscribeCallBack = userCallback;
            isSubscribeAssigned = true;
        }

        //Subscribe to a certain pin on remote device
        public void subscribeToChanges(byte pin0)
        {
            pin0 = checkAnalogPinNumbers(pin0);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SUBSCRIBE, 2, args);
        }

        //Subscribe to a certain pin on remote device
        public void subscribeToChanges(byte pin0, byte pin1)
        {
	        pin0 = checkAnalogPinNumbers(pin0);
	        pin1 = checkAnalogPinNumbers(pin1);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SUBSCRIBE, 3, args);
        }

        //Subscribe to a certain pin on remote device
        public void subscribeToChanges(byte pin0, byte pin1, byte pin2)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);
            pin2 = checkAnalogPinNumbers(pin2);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            byte[] pn2 = new byte[1];
            pn2[0] = pin2;

            FunctionArg arg4 = new FunctionArg(1, pn2);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SUBSCRIBE, 4, args);
        }

        //Subscribe to a certain pin on remote device
        public void subscribeToChanges(byte pin0, byte pin1, byte pin2, byte pin3)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);
            pin2 = checkAnalogPinNumbers(pin2);
            pin3 = checkAnalogPinNumbers(pin3);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            byte[] pn2 = new byte[1];
            pn2[0] = pin2;

            FunctionArg arg4 = new FunctionArg(1, pn2);

            byte[] pn3 = new byte[1];
            pn3[0] = pin3;

            FunctionArg arg5 = new FunctionArg(1, pn3);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SUBSCRIBE, 5, args);
        }

        //Subscribe to a certain pin on remote device
        public void subscribeToChanges(byte pin0, byte pin1, byte pin2, byte pin3, byte pin4)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);
            pin2 = checkAnalogPinNumbers(pin2);
            pin3 = checkAnalogPinNumbers(pin3);
            pin4 = checkAnalogPinNumbers(pin4);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            byte[] pn2 = new byte[1];
            pn2[0] = pin2;

            FunctionArg arg4 = new FunctionArg(1, pn2);

            byte[] pn3 = new byte[1];
            pn3[0] = pin3;

            FunctionArg arg5 = new FunctionArg(1, pn3);

            byte[] pn4 = new byte[1];
            pn4[0] = pin4;

            FunctionArg arg6 = new FunctionArg(1, pn4);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_SUBSCRIBE, 6, args);
        }

        //imSubscribe to a certain pin on remote device
        public void unsubscribeToChanges(byte pin0)
        {
            pin0 = checkAnalogPinNumbers(pin0);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_UNSUBSCRIBE, 2, args);
        }

        //unSubscribe to a certain pin on remote device
        public void unsubscribeToChanges(byte pin0, byte pin1)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_UNSUBSCRIBE, 3, args);
        }

        //unSubscribe to a certain pin on remote device
        public void unsubscribeToChanges(byte pin0, byte pin1, byte pin2)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);
            pin2 = checkAnalogPinNumbers(pin2);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            byte[] pn2 = new byte[1];
            pn2[0] = pin2;

            FunctionArg arg4 = new FunctionArg(1, pn2);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_UNSUBSCRIBE, 4, args);
        }

        //unSubscribe to a certain pin on remote device
        public void unsubscribeToChanges(byte pin0, byte pin1, byte pin2, byte pin3)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);
            pin2 = checkAnalogPinNumbers(pin2);
            pin3 = checkAnalogPinNumbers(pin3);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            byte[] pn2 = new byte[1];
            pn2[0] = pin2;

            FunctionArg arg4 = new FunctionArg(1, pn2);

            byte[] pn3 = new byte[1];
            pn3[0] = pin3;

            FunctionArg arg5 = new FunctionArg(1, pn3);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_UNSUBSCRIBE, 5, args);
        }

        //unSubscribe to a certain pin on remote device
        public void unsubscribeToChanges(byte pin0, byte pin1, byte pin2, byte pin3, byte pin4)
        {
            pin0 = checkAnalogPinNumbers(pin0);
            pin1 = checkAnalogPinNumbers(pin1);
            pin2 = checkAnalogPinNumbers(pin2);
            pin3 = checkAnalogPinNumbers(pin3);
            pin4 = checkAnalogPinNumbers(pin4);

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(remoteOneSheeldAddress.Length, System.Text.Encoding.UTF8.GetBytes(remoteOneSheeldAddress));

            args.Add(arg1);

            byte[] pn0 = new byte[1];
            pn0[0] = pin0;

            FunctionArg arg2 = new FunctionArg(1, pn0);

            byte[] pn1 = new byte[1];
            pn1[0] = pin1;

            FunctionArg arg3 = new FunctionArg(1, pn1);

            byte[] pn2 = new byte[1];
            pn2[0] = pin2;

            FunctionArg arg4 = new FunctionArg(1, pn2);

            byte[] pn3 = new byte[1];
            pn3[0] = pin3;

            FunctionArg arg5 = new FunctionArg(1, pn3);

            byte[] pn4 = new byte[1];
            pn4[0] = pin4;

            FunctionArg arg6 = new FunctionArg(1, pn4);

            Sheeld.sendPacket(ShieldIds.REMOTE_SHEELD_ID, 0, REMOTEONESHEELD_UNSUBSCRIBE, 6, args);
        }

        //Output function ID's 
        const byte REMOTEONESHEELD_PIN_MODE = 0x01;
        const byte REMOTEONESHEELD_WRITE = 0x02;
        const byte REMOTEONESHEELD_READ = 0x03;
        const byte REMOTEONESHEELD_ANALOG_WRITE = 0x04;
        const byte REMOTEONESHEELD_SEND_FLOAT = 0x05;
        const byte REMOTEONESHEELD_SEND_STRING = 0x06;
        const byte REMOTEONESHEELD_SUBSCRIBE = 0x07;
        const byte REMOTEONESHEELD_UNSUBSCRIBE = 0x08;

        //Input function ID's
        const byte DIGITAL_SUBSCRIBE_VALUE = 0x01;
        const byte READ_MESSAGE_FLOAT = 0x02;
        const byte READ_MESSAGE_STRING = 0x03;
    }
}
