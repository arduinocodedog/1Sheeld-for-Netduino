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
    public class SliderCallback : IByteCallback
    {
        OneSheeld sheeld = null;
        int waitvalue = 0;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            sheeld.SLIDER.setOnValueChange(this);
        }

        public void Loop()
        {
            sheeld.BUZZER.buzzOn();
            Thread.Sleep(waitvalue);
            sheeld.BUZZER.buzzOff();
            Thread.Sleep(waitvalue);
        }

        public void OnChange(byte value)
        {
            waitvalue = 1000 - (3 * value);
        }
    }
}
