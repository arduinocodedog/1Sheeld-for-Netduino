namespace OneSheeldClasses
{
    public class PressureSensorShield : ULongInputShield
    {
        public PressureSensorShield() :
            base(PRESSURE_VALUE, ShieldIds.PRESSURE_ID, 2) { }

        const byte PRESSURE_VALUE = 0x01;
    }
}
