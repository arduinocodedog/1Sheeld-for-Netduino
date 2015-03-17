using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleLED
{
    public class Led
    {
        OneSheeld sheeld = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            OneSheeld.LED.setHigh();
            Thread.Sleep(1000);
            OneSheeld.LED.setLow();
            Thread.Sleep(1000);
        }
    }
}
