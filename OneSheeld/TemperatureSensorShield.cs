using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class TemperatureSensorShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        sbyte value = -1;
	    bool isCallBackAssigned=false;
        ISByteCallback changeCallBack = null;

        public TemperatureSensorShield(OneSheeld onesheeld)
            : base(onesheeld, (byte)ShieldIds.TEMPERATURE_ID)
        {
            Sheeld = onesheeld;
        }

        public sbyte getValue()
        {
            return value;
        }

        public float getAsFahrenheit()
        {
            float fahrenheit;
            fahrenheit = (float)value * (1.8f) + 32.0f;
            return fahrenheit;
        }

        public override void processData()
        {
            byte functionID = Sheeld.getFunctionId();

            if (functionID == TEMPERATURE_VALUE)
            {
                value = (sbyte) Sheeld.getArgumentData(0)[0];
                if (isCallBackAssigned)
                {
                    changeCallBack.OnChange(value);
                }
            }
        }

        public void setOnValueChange(ISByteCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }

        const byte TEMPERATURE_VALUE = 0x01;
    }
}
