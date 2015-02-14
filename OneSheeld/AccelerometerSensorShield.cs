using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class AccelerometerSensorShield : XYZSensorShield
    {
        public AccelerometerSensorShield(OneSheeld onesheeld) : 
            base(onesheeld, ACCELEROMETER_VALUE) {}

        //Input Function ID 
        const byte ACCELEROMETER_VALUE = 0x01;
    }
}
