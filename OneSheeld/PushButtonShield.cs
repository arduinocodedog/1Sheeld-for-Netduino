using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class PushButtonShield : BoolInputShield
    {
        public PushButtonShield(OneSheeld onesheeld)
            : base(onesheeld, PUSHBUTTON_VALUE, ShieldIds.PUSH_BUTTON_ID) { }

        public bool isPressed()
        {
            return (value != 0x00);
        }

        const byte PUSHBUTTON_VALUE = 0x01;
    }
}
