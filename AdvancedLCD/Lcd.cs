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
    public class Lcd : OneSheeldUser, IOneSheeldSketch
    {
        InputPort button = null;

        public void Setup()
        {
            OneSheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, false, Port.ResistorMode.Disabled);
        }

        public void Loop()
        {
            if (button.Read())
            {
                LCD.begin();
                LCD.blink();
                Thread.Sleep(5000);
                LCD.noBlink();
                LCD.write('A');
                LCD.write('B');
                LCD.write('C');
                Thread.Sleep(2000);
                LCD.clear();
                LCD.print("Hello, World!");
                LCD.setCursor(1, 0);
                LCD.print("This is 1Sheeld");
                Thread.Sleep(10000);
                LCD.clear();
                LCD.print("Closing!");
                Thread.Sleep(10000);
            }
        }
    }
}
