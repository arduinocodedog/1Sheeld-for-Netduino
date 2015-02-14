using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleTERMINAL
{
    class Terminal
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
            if (sheeld.TOGGLEBUTTON.getStatus())
            {
                led.Write(true);
                sheeld.TERMINAL.println("PushButton Pressed");
            }
            else
            {
                led.Write(false);
                sheeld.TERMINAL.println("PushButton Released");
            }
            Thread.Sleep(5000);
        }
    }
}
