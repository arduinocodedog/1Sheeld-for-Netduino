using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleFACEBOOK
{
    class Facebook
    {
        OneSheeld sheeld = null;

        InputPort button = null;
        OutputPort led = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, true, Port.ResistorMode.Disabled);
            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (button.Read())
            {
                led.Write(true);
                sheeld.FACEBOOK.post("Posting to Facebook with a 1Sheeld on a Netduino!");
                Thread.Sleep(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
