namespace OneSheeldClasses
{
    public class AccelerometerSensorShield : XYZSensorShield
    {
        public AccelerometerSensorShield() : 
            base(ACCELEROMETER_VALUE, ShieldIds.ACCELEROMETER_ID) {}

        //Input Function ID 
        const byte ACCELEROMETER_VALUE = 0x01;
    }
}
