using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedTTS
{
    public class TextToSpeech
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        string firstCommand = "good morning one shield";
        string secondCommand = "how is the weather today";
        string thirdCommand = "turn on";
        string fourthCommand = "turn off";

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (sheeld.VOICERECOGNITION.isNewCommandReceived())
            {
                string lastCommand = sheeld.VOICERECOGNITION.getLastCommand();

                if (lastCommand.CompareTo(firstCommand) == 0)
                {
                    sheeld.TTS.say("Good morning sir");
                }
                
                if (lastCommand.CompareTo(secondCommand) == 0)
                {
                    sheeld.TTS.say("the weather is pretty good sir");
                }

                if (lastCommand.Length >= thirdCommand.Length)
                {
                    if (lastCommand.Substring(0, thirdCommand.Length).CompareTo(thirdCommand) == 0)
                    {
                        led.Write(true);
                        sheeld.TTS.say("L.E.D. turned on");
                    }
                }

                if (lastCommand.Length >= fourthCommand.Length)
                {
                    if (lastCommand.Substring(0, fourthCommand.Length).CompareTo(fourthCommand) == 0)
                    {
                        led.Write(false);
                        sheeld.TTS.say("L.E.D. turned off");
                    }
                }
            }
        }
    }
}
