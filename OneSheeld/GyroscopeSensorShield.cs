using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GyroscopeSensorShield : XYZSensorShield
    {
        public GyroscopeSensorShield(OneSheeld onesheeld) : 
            base(onesheeld, GYROSCOPE_VALUE, (byte) ShieldIds.GYROSCOPE_ID) {}

        //Input Function ID 
        const byte GYROSCOPE_VALUE = 0x01;
    }
}
