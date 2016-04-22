using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleCAMERA
{
    public class Camera : OneSheeldUser, IOneSheeldSketch
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
                CAMERA.setFlash(CAMERA.ON);
                CAMERA.rearCapture();
                OneSheeld.delay(10000);
                TWITTER.tweetLastPicture("Posted by @1Sheeld and @Netduino");
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
