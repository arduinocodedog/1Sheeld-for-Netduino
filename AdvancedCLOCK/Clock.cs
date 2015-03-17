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
            if (!OneSheeld.CLOCK.isInitialized())
            {
                OneSheeld.TERMINAL.println("Initializing Clock.");
                OneSheeld.CLOCK.begin();
            }
            else
            {
                byte hour = OneSheeld.CLOCK.getHours();
                byte minute = OneSheeld.CLOCK.getMinutes();
                byte second = OneSheeld.CLOCK.getSeconds();
                byte day = OneSheeld.CLOCK.getDay();
                byte month = OneSheeld.CLOCK.getMonth();
                short year = OneSheeld.CLOCK.getYear();

                DateTime dt = new DateTime(year, month, day, hour, minute, second);
                OneSheeld.TERMINAL.println(dt.ToString());
            }

            Thread.Sleep(2000);
        }
    }
}
