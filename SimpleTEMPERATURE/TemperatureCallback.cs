using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;


namespace SimpleTEMPERATURE
{
    public class TemperatureCallback : ISByteCallback
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        sbyte value = 0x00;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            OneSheeld.TEMPERATURE.setOnValueChange(this);
        }

        public void Loop()
        {
            if (value < 10)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        //Callback implementation
        public void OnChange(sbyte val)
        {
            value = val;
        }
    }
}
