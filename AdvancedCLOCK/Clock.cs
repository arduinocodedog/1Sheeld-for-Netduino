using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedCLOCK
{
    public class Clock
    {
        OneSheeld sheeld = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            if (!sheeld.CLOCK.isInitialized())
            {
                sheeld.TERMINAL.println("Initializing Clock.");
                sheeld.CLOCK.begin();
            }
            else
            {
                byte hour = sheeld.CLOCK.getHours();
                byte minute = sheeld.CLOCK.getMinutes();
                byte second = sheeld.CLOCK.getSeconds();
                byte day = sheeld.CLOCK.getDay();
                byte month = sheeld.CLOCK.getMonth();
                short year = sheeld.CLOCK.getYear();

                DateTime dt = new DateTime(year, month, day, hour, minute, second);
                sheeld.TERMINAL.println(dt.ToString());
            }

            Thread.Sleep(2000);
        }
    }
}
