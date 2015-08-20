namespace OneSheeldClasses
{
    public class ToggleButtonShield : BoolInputShield
    {
        public ToggleButtonShield()
            : base(TOGGLEBUTTON_VALUE, ShieldIds.TOGGLE_BUTTON_ID) { }

        public bool getStatus()
        {
            return (value != 0x00);
        }

        const byte TOGGLEBUTTON_VALUE = 0x01;
    }
}
