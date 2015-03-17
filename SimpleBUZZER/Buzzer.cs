using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleBUZZER
{
    public class Buzzer
    {
        OneSheeld sheeld = null;

        InputPort button = null;
        OutputPort led = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, false, Port.ResistorMode.Disabled);
            led = new OutputPort(Pins.GPIO_PIN_D13, false);
            
        }

        public void Loop()
        {
            if (button.Read())
            {
                OneSheeld.BUZZER.buzzOn();
                led.Write(true);
            }
            else
            {
                OneSheeld.BUZZER.buzzOff();
                led.Write(false);
            }
        }
    }
}
