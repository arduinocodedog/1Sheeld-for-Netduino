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
                OneSheeld.LCD.begin();
                OneSheeld.LCD.blink();
                Thread.Sleep(5000);
                OneSheeld.LCD.noBlink();
                OneSheeld.LCD.write('A');
                OneSheeld.LCD.write('B');
                OneSheeld.LCD.write('C');
                Thread.Sleep(2000);
                OneSheeld.LCD.clear();
                OneSheeld.LCD.print("Hello, World!");
                OneSheeld.LCD.setCursor(1, 0);
                OneSheeld.LCD.print("This is 1Sheeld");
                Thread.Sleep(10000);
                OneSheeld.LCD.clear();
                OneSheeld.LCD.print("Closing!");
                Thread.Sleep(10000);
            }
        }
    }
}
