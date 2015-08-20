namespace OneSheeldClasses
{
    public class LightSensorShield : ULongInputShield
    {
        public LightSensorShield() :
            base(LIGHT_VALUE, ShieldIds.LIGHT_ID, 3) { }


        const byte LIGHT_VALUE = 0x01;
    }
}
