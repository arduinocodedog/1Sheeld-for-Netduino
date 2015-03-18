using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class SliderShield : ByteInputShield
    {
        public SliderShield()
            : base(SLIDER_VALUE, ShieldIds.SLIDER_ID) { }

        const byte SLIDER_VALUE = 0x01;
    }
}
