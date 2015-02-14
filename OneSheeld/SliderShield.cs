using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class SliderShield : ByteInputShield
    {
        public SliderShield(OneSheeld onesheeld)
            : base(onesheeld, SLIDER_VALUE) { }

        const byte SLIDER_VALUE = 0x01;
    }
}
