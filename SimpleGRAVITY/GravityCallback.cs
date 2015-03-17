using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleGRAVITY
{
    public class GravityCallback : IXYZFloatCallback
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

            OneSheeld.GRAVITY.setOnValueChange(this);
        }

        public void Loop()
        {
            if (x > 9 ||
                y > 9 ||
                z > 9)
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
