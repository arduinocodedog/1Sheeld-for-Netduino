using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleMAGNETOMETER
{
    class Magnetometer : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();

            LCD.begin();
        }

        public void Loop()
        {
            /* Move the cursor. */
            LCD.setCursor(0, 0);
            /* Print a title. */
            LCD.print("MagneticStrength");
            /* Move the cursor. */
            LCD.setCursor(1, 0);
            /* Display the magnetic strength. */
            LCD.print(MAGNETOMETER.getMagneticStrength(), 2);
            /* Move the cursor. */
            LCD.setCursor(1, 7);
            /* Print a unit. */
            LCD.print("Tesla");
            /* Wait for 1 second. */
            Thread.Sleep(1000);
        }
    }
}
