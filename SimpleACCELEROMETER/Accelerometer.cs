using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleACCELEROMETER
{
    public class Accelerometer : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (ACCELEROMETER.getX() > 8 ||
                ACCELEROMETER.getY() > 8 ||
                ACCELEROMETER.getZ() > 8)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
