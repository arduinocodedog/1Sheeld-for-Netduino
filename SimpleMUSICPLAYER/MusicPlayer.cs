using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleMUSICPLAYER
{
    public class MusicPlayer
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        bool MusicPlaying = false;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (OneSheeld.PUSHBUTTON.isPressed())
            { 
                if (MusicPlaying)
                {
                    led.Write(false);
                    OneSheeld.MUSICPLAYER.pause();
                    MusicPlaying = false;
                }
                else
                {
                    led.Write(true);
                    OneSheeld.MUSICPLAYER.setVolume(5);
                    OneSheeld.MUSICPLAYER.play();
                    MusicPlaying = true;
                }

                Thread.Sleep(300);
            }
        }
    }
}
