using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleFOURSQUARE
{
    public class Foursquare
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

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
                led.Write(true);
                OneSheeld.FOURSQUARE.checkIn("511759f2e4b0b0ae6ed91067", "Back to work!");
                Thread.Sleep(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
