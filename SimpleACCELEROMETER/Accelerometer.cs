using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleACCELEROMETER
{
    public class Accelerometer
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
            if (sheeld.ACCELEROMETER.getX() > 8 ||
                sheeld.ACCELEROMETER.getY() > 8 ||
                sheeld.ACCELEROMETER.getZ() > 8)
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
