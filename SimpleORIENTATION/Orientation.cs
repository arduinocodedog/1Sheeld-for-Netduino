using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleORIENTATION
{
    public class Orientation
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
            if (OneSheeld.ACCELEROMETER.getX() > 350 ||
                OneSheeld.ACCELEROMETER.getY() > 170 ||
                OneSheeld.ACCELEROMETER.getZ() > 80)
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
