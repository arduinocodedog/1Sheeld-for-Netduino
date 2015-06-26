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
    class DataLogger : OneSheeldUser, IOneSheeldSketch
    {
        InputPort button = null;

        int counter = 0;
        bool startFlag = false;

        public void Setup()
        {
            OneSheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, true, Port.ResistorMode.Disabled);
        }

        public void Loop()
        {
            if (button.Read())
            {
                DATALOGGER.stop();
                OneSheeld.delay(500);
                DATALOGGER.start("Mic values");
                startFlag = true;
            }

            if (startFlag)
            {
                DATALOGGER.add("Decibles", MIC.getValue());
                DATALOGGER.log();
                OneSheeld.delay(1000);
                counter++;
                if (counter == 20)
                {
                    DATALOGGER.stop();
                    counter = 0;
                    startFlag = false;
                }
            }
        }
    }
}
