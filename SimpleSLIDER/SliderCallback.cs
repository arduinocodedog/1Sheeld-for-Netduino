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
    public class SliderCallback : OneSheeldUser, IOneSheeldSketch, IByteCallback
    {
        int waitvalue = 0;

        public void Setup()
        {
            OneSheeld.begin();

            SLIDER.setOnValueChange(this);
        }

        public void Loop()
        {
            BUZZER.buzzOn();
            Thread.Sleep(waitvalue);
            BUZZER.buzzOff();
            Thread.Sleep(waitvalue);
        }

        public void OnChange(byte value)
        {
            waitvalue = 1000 - (3 * value);
        }
    }
}
