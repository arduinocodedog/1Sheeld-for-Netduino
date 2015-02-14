using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimplePHONE
{
    public class Phone
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (sheeld.PUSHBUTTON.isPressed())
            {
                led.Write(true);
                sheeld.PHONE.call("1234567890");
                Thread.Sleep(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
