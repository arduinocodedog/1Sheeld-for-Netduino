using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleDATALOGGER
{
    class DataLogger
    {
        OneSheeld sheeld = null;
        InputPort button = null;

        int counter = 0;
        bool startFlag = false;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, true, Port.ResistorMode.Disabled);
        }

        public void Loop()
        {
            if (button.Read())
            {
                OneSheeld.DATALOGGER.stop();
                Thread.Sleep(500);
                OneSheeld.DATALOGGER.start("Mic values");
                startFlag = true;
            }

            if (startFlag)
            {
                OneSheeld.DATALOGGER.add("Decibles", OneSheeld.MIC.getValue());
                OneSheeld.DATALOGGER.log();
                Thread.Sleep(1000);
                counter++;
                if (counter == 20)
                {
                    OneSheeld.DATALOGGER.stop();
                    counter = 0;
                    startFlag = false;
                }
            }
        }
    }
}
