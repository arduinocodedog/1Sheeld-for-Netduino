using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class LightSensorShield : ULongInputShield
    {
        public LightSensorShield(OneSheeld onesheeld) :
            base(onesheeld, LIGHT_VALUE, 3) { }


        const byte LIGHT_VALUE = 0x01;
    }
}
