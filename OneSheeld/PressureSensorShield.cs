using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class PressureSensorShield : ULongInputShield
    {
        public PressureSensorShield(OneSheeld onesheeld) :
            base(onesheeld, PRESSURE_VALUE, ShieldIds.PRESSURE_ID, 2) { }

        const byte PRESSURE_VALUE = 0x01;
    }
}
