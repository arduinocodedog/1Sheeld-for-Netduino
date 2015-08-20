namespace OneSheeldClasses
{
    public class MagnetometerSensorShield : XYZSensorShield
    {
        public MagnetometerSensorShield() : 
            base(MAGNETOMETER_VALUE, ShieldIds.MAGNETOMETER_ID) {}
 
        //Helper
        public float getMagneticStrength()
        {
	        return (float) System.Math.Sqrt((valueX*valueX)+(valueY*valueY)+(valueZ*valueZ));
        }

        //Input Function ID 
        const byte MAGNETOMETER_VALUE = 0x01;
    }
}
