using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleKEYPAD
{
    public class Keypad
    {
        OneSheeld sheeld = null;
        OutputPort led1 = null;
        OutputPort led2 = null;
        OutputPort led3 = null;
        OutputPort led4 = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led1 = new OutputPort(Pins.GPIO_PIN_D13, false);
            led2 = new OutputPort(Pins.GPIO_PIN_D12, false);
            led3 = new OutputPort(Pins.GPIO_PIN_D11, false);
            led4 = new OutputPort(Pins.GPIO_PIN_D10, false);
        }

        public void Loop()
        {
            /* If keypad's button 1 is pressed. */
            if (sheeld.KEYPAD.isRowPressed(0) && sheeld.KEYPAD.isColumnPressed(0))
            {
                /* Turn on the LED 1. */
                led1.Write(true);
                /* Turn off the other LEDs. */
                led2.Write(false);
                led3.Write(false);
                led4.Write(false);
            }
            /* If keypad's button 2 is pressed. */
            else if (sheeld.KEYPAD.isRowPressed(0) && sheeld.KEYPAD.isColumnPressed(1))
            {
                /* Turn on the LED 2. */
                led2.Write(true);
                /* Turn off the other LEDs. */
                led1.Write(false);
                led3.Write(false);
                led4.Write(false);
            }
            /* If keypad's button 3 is pressed. */
            else if (sheeld.KEYPAD.isRowPressed(0) && sheeld.KEYPAD.isColumnPressed(2))
            {
                /* Turn on the LED 3. */
                led3.Write(true);
                /* Turn off the other LEDs. */
                led1.Write(false);
                led2.Write(false);
                led4.Write(false);
            }
            /* If keypad's button 4 is pressed. */
            else if (sheeld.KEYPAD.isRowPressed(1) && sheeld.KEYPAD.isColumnPressed(0))
            {
                /* Turn on the LED 4. */
                led4.Write(true);
                /* Turn off the other LEDs. */
                led1.Write(false);
                led2.Write(false);
                led3.Write(false);
            }
            else
            {
                /* Turn off all of LEDs. */
                led1.Write(false);
                led2.Write(false);
                led3.Write(false);
                led4.Write(false);
            }
        }
    }
}
