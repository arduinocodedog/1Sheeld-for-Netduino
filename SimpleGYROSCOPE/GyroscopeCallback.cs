using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleGYROSCOPE
{
    public class GyroscopeCallback : IXYZFloatCallback
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            OneSheeld.GYROSCOPE.setOnValueChange(this);
        }

        public void Loop()
        {
            if (x > 1 ||
                y > 1 ||
                z > 1)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        //Callback implementation
        public void OnChange(float valueX, float valueY, float valueZ)
        {
            x = valueX;
            y = valueY;
            z = valueZ;
        }
    }
}