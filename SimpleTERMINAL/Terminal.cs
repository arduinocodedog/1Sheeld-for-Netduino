using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleTERMINAL
{
    class Terminal : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (TOGGLEBUTTON.getStatus())
            {
                led.Write(true);
                TERMINAL.println("PushButton Pressed");
            }
            else
            {
                led.Write(false);
                TERMINAL.println("PushButton Released");
            }
            OneSheeld.delay(500);
        }
    }
}
