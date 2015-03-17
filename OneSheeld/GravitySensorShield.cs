using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GravitySensorShield : XYZSensorShield
    {
        public GravitySensorShield(OneSheeld onesheeld) : 
            base(onesheeld, GRAVITY_VALUE, ShieldIds.GRAVITY_ID) {}

        //Input Function ID 
        const byte GRAVITY_VALUE = 0x01;
    }
}
