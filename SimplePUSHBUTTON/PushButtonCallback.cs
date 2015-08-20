using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimplePUSHBUTTON
{
    public class PushButtonCallback : OneSheeldUser, IOneSheeldSketch, IBoolCallback
    {
        OutputPort led = null;

        bool value = false;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            PUSHBUTTON.setOnButtonStatusChange(this);
        }

        public void Loop()
        {
            if (value)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        public void OnChange(bool val)
        {
            value = val;
        }
    }
}
