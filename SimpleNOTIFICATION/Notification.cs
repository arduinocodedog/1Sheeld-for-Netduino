using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleNOTIFICATION
{
    public class Notification
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
            if (sheeld.PUSHBUTTON.isPressed())
            {
                led.Write(true);
                sheeld.NOTIFICATION.notifyPhone("Someone pressed the button!");
                Thread.Sleep(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
