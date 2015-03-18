using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleKEYBOARD
{
    public class KeyboardCallback : OneSheeldUser, IOneSheeldSketch, ICharCallback
    {
        OutputPort led = null;
        char character = (char)0;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            KEYBOARD.setOnValueChange(this);
        }

        public void Loop()
        {
            /* If keyboard's A key is pressed. */
            if (character == 'A')
            {
                /* Turn on the LED. */
                led.Write(true);
            }
            /* otherwise */
            else
            {
                /* Turn off LEDs */
                led.Write(false);
            }
        }

        public void OnChange(char val)
        {
            character = val;
        }
    }
}
