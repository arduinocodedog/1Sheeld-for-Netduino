using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class OrientationSensorShield : XYZSensorShield
    {
        public OrientationSensorShield(OneSheeld onesheeld)
            : base(onesheeld, ORIENTATION_VALUE, ShieldIds.ORIENTATION_ID) { }

        const byte ORIENTATION_VALUE = 0x01;
    }
}
