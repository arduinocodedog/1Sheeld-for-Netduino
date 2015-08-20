namespace OneSheeldClasses
{
    public class GravitySensorShield : XYZSensorShield
    {
        public GravitySensorShield() : 
            base(GRAVITY_VALUE, ShieldIds.GRAVITY_ID) {}

        //Input Function ID 
        const byte GRAVITY_VALUE = 0x01;
    }
}
