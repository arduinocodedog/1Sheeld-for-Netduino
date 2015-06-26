using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleVOICERECOGNITION
{
    public class VoiceRecognition : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        string firstCommand = "play";
        string secondCommand = "pause";

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (VOICERECOGNITION.isNewCommandReceived())
            {
                string lastCommand = VOICERECOGNITION.getLastCommand();

                if (lastCommand.Length >= firstCommand.Length)
                {
                    if (lastCommand.Substring(0, firstCommand.Length).CompareTo(firstCommand) == 0)
                    {
                        led.Write(true);
                        MUSICPLAYER.setVolume(5);
                        MUSICPLAYER.play();
                    }
                }

                if (lastCommand.Length >= secondCommand.Length)
                {
                    if (lastCommand.Substring(0, secondCommand.Length).CompareTo(secondCommand) == 0)
                    {
                        led.Write(false);
                        MUSICPLAYER.pause();
                    }
                }

                OneSheeld.delay(300);
            }
        }
    }
}
