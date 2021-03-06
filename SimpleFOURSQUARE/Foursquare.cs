using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleFOURSQUARE
{
    public class Foursquare : OneSheeldUser, IOneSheeldSketch
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
                FOURSQUARE.checkIn("511759f2e4b0b0ae6ed91067", "Back to work!");
                OneSheeld.delay(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
