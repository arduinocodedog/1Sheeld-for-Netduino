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
    public class Notification : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (PUSHBUTTON.isPressed())
            {
                led.Write(true);
                NOTIFICATION.notifyPhone("Someone pressed the button!");
                OneSheeld.delay(300);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
