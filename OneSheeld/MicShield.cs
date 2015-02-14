using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class MicShield : ByteInputShield
    {
        public MicShield(OneSheeld onesheeld)
            : base(onesheeld, MIC_VALUE) { }
        
        const byte MIC_VALUE = 0x01;
    }
}
