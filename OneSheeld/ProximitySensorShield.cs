using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ProximitySensorShield : ByteInputShield
    {
        public ProximitySensorShield()
            : base(PROXIMITY_VALUE, ShieldIds.PROXIMITY_ID) { }
        
        const byte PROXIMITY_VALUE = 0x01;
    }
}
