using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleSLIDER
{
    public class Slider
    {
        OneSheeld sheeld = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            byte value = OneSheeld.SLIDER.getValue();
            
            int waitvalue = 1000 - (3 * value);

            OneSheeld.BUZZER.buzzOn();
            Thread.Sleep(waitvalue);
            OneSheeld.BUZZER.buzzOff();
            Thread.Sleep(waitvalue);
        }
    }
}
