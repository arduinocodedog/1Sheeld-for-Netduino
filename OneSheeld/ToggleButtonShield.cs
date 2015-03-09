using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ToggleButtonShield : BoolInputShield
    {
        public ToggleButtonShield(OneSheeld onesheeld)
            : base(onesheeld, TOGGLEBUTTON_VALUE, (byte) ShieldIds.TOGGLE_BUTTON_ID) { }

        public bool getStatus()
        {
            return (value != 0x00);
        }

        const byte TOGGLEBUTTON_VALUE = 0x01;
    }
}
