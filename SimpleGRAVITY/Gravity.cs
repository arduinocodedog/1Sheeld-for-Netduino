using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleGRAVITY
{
    class Gravity
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
            if (OneSheeld.GRAVITY.getX() > 9 ||
                OneSheeld.GRAVITY.getY() > 9 ||
                OneSheeld.GRAVITY.getZ() > 9)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
