using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimplePHONE
{
    public class Phone : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (PUSHBUTTON.isPressed())
            {
                led.Write(true);
                PHONE.call("1234567890");
                Thread.Sleep(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
