using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleTOGGLEBUTTON
{
    public class ToggleButtonCallback : IBoolCallback
    {
        OutputPort led = null;

        bool value = false;
        
        public void Setup()
        {
            OneSheeld sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            OneSheeld.TOGGLEBUTTON.setOnButtonStatusChange(this);
        }

        public void Loop()
        {
            if (value)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        public void OnChange(bool val)
        {
            value = val;
        }
    }
}
