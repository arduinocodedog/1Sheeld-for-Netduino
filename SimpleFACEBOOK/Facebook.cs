using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleFACEBOOK
{
    class Facebook : OneSheeldUser, IOneSheeldSketch
    {
        InputPort button = null;
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, true, Port.ResistorMode.Disabled);
            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (button.Read())
            {
                led.Write(true);
                FACEBOOK.post("Posting to Facebook with a 1Sheeld on a Netduino!");
                OneSheeld.delay(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
