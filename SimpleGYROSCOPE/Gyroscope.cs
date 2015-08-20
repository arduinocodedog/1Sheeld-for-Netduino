using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleGYROSCOPE
{
    class Gravity : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (GYROSCOPE.getX() > 1 ||
                GYROSCOPE.getY() > 1 ||
                GYROSCOPE.getZ() > 1)
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