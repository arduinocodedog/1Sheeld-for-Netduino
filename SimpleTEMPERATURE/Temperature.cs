using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleTEMPERATURE
{
    public class Temperature : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (TEMPERATURE.getValue() < 10)
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
