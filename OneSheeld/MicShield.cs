using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class MicShield : ByteInputShield
    {
        public MicShield(OneSheeld onesheeld)
            : base(onesheeld, MIC_VALUE, (byte) ShieldIds.MIC_ID) { }
        
        const byte MIC_VALUE = 0x01;
    }
}
