using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleMIC
{
    public class Mic : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

		public void Setup()
		{
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (MIC.getValue() > 80)
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
