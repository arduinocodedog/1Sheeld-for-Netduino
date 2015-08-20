using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleLIGHT
{
    public class Light : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (LIGHT.getValue() < 100 && LIGHT.getValue() > 0)
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
