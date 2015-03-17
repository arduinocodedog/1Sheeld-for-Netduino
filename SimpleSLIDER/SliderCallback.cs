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

            OneSheeld.SLIDER.setOnValueChange(this);
        }

        public void Loop()
        {
            OneSheeld.BUZZER.buzzOn();
            Thread.Sleep(waitvalue);
            OneSheeld.BUZZER.buzzOff();
            Thread.Sleep(waitvalue);
        }

        public void OnChange(byte value)
        {
            waitvalue = 1000 - (3 * value);
        }
    }
}
