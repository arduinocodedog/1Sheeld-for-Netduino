using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class LedShield
    {
        OneSheeld Sheeld = null;

        //Setter 
        void setValue(byte data)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = data;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

	        Sheeld.sendPacket(ShieldIds.LED_ID,0,LED_SET_VALUE,1,args);
        }

        // ----------------------  Public Methods -----------------------

        public LedShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
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
