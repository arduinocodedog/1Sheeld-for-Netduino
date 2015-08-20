using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleMUSICPLAYER
{
    public class MusicPlayer : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        bool MusicPlaying = false;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (PUSHBUTTON.isPressed())
            { 
                if (MusicPlaying)
                {
                    led.Write(false);
                    MUSICPLAYER.pause();
                    MusicPlaying = false;
                }
                else
                {
                    led.Write(true);
                    MUSICPLAYER.setVolume(5);
                    MUSICPLAYER.play();
                    MusicPlaying = true;
                }

                OneSheeld.delay(300);
            }
        }
    }
}
