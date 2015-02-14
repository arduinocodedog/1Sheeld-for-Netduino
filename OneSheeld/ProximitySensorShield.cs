using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ProximitySensorShield : ByteInputShield
    {
        public ProximitySensorShield(OneSheeld onesheeld)
            : base(onesheeld, PROXIMITY_VALUE) { }
        
        const byte PROXIMITY_VALUE = 0x01;
    }
}
