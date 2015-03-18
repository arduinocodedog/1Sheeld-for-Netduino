using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleTWITTER
{
    public class Twitter : OneSheeldUser, IOneSheeldSketch
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
                TWITTER.tweet("I'm tweeting from @Netduino via @1Sheeld!");
                Thread.Sleep(300);
            }
            else
            {
                led.Write(false);
            }
        }

    }
}
