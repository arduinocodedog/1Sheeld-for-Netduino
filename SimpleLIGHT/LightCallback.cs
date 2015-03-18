using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleLIGHT
{
    public class LightCallback : OneSheeldUser, IOneSheeldSketch, IULongCallback
    {
        OutputPort led = null;

        ulong value = 0L;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            LIGHT.setOnValueChange(this);
        }

        public void Loop()
        {
            if (value < 100 && value > 0)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        public void OnChange(ulong val)
        {
            value = val;
        }
    }
}
