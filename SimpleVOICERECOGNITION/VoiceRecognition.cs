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
    public class VoiceRecognition
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        string firstCommand = "play";
        string secondCommand = "pause";

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (OneSheeld.VOICERECOGNITION.isNewCommandReceived())
            {
                string lastCommand = OneSheeld.VOICERECOGNITION.getLastCommand();

                if (lastCommand.Length >= firstCommand.Length)
                {
                    if (lastCommand.Substring(0, firstCommand.Length).CompareTo(firstCommand) == 0)
                    {
                        led.Write(true);
                        OneSheeld.MUSICPLAYER.setVolume(5);
                        OneSheeld.MUSICPLAYER.play();
                    }
                }

                if (lastCommand.Length >= secondCommand.Length)
                {
                    if (lastCommand.Substring(0, secondCommand.Length).CompareTo(secondCommand) == 0)
                    {
                        led.Write(false);
                        OneSheeld.MUSICPLAYER.pause();
                    }
                }

                Thread.Sleep(300);
            }
        }
    }
}
