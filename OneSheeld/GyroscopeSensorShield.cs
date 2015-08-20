namespace OneSheeldClasses
{
    public class GyroscopeSensorShield : XYZSensorShield
    {
        public GyroscopeSensorShield() : 
            base(GYROSCOPE_VALUE, ShieldIds.GYROSCOPE_ID) {}

        //Input Function ID 
        const byte GYROSCOPE_VALUE = 0x01;
    }
}
