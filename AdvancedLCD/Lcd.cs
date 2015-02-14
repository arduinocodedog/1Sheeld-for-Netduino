using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedLCD
{
    public class Lcd
    {
        OneSheeld sheeld = null;
        InputPort button = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, false, Port.ResistorMode.Disabled);
        }

        public void Loop()
        {
            if (button.Read())
            {
                sheeld.LCD.begin();
                sheeld.LCD.blink();
                Thread.Sleep(5000);
                sheeld.LCD.noBlink();
                sheeld.LCD.write('A');
                sheeld.LCD.write('B');
                sheeld.LCD.write('C');
                Thread.Sleep(2000);
                sheeld.LCD.clear();
                sheeld.LCD.print("Hello, World!");
                sheeld.LCD.setCursor(1, 0);
                sheeld.LCD.print("This is 1Sheeld");
                Thread.Sleep(10000);
                sheeld.LCD.clear();
                sheeld.LCD.print("Closing!");
                Thread.Sleep(10000);
            }
        }
    }
}
