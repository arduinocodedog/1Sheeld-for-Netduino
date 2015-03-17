using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedSEVENSEGMENT
{
    class SevenSegment
    {
        OneSheeld sheeld = null;
        InputPort button = null;
        byte number = 0;

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
                OneSheeld.SEVENSEGMENT.setNumber(number);
                Thread.Sleep(1000);
                number++;
                if (number > 9)
                    number = 0;
            }
        }
    }
}
