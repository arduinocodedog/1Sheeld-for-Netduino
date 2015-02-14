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
    class Magnetometer
    {
        OneSheeld sheeld = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            sheeld.LCD.begin();
        }

        public void Loop()
        {
            /* Move the cursor. */
            sheeld.LCD.setCursor(0, 0);
            /* Print a title. */
            sheeld.LCD.print("MagneticStrength");
            /* Move the cursor. */
            sheeld.LCD.setCursor(1, 0);
            /* Display the magnetic strength. */
            sheeld.LCD.print(sheeld.MAGNETOMETER.getMagneticStrength(), 2);
            /* Move the cursor. */
            sheeld.LCD.setCursor(1, 7);
            /* Print a unit. */
            sheeld.LCD.print("Tesla");
            /* Wait for 1 second. */
            Thread.Sleep(1000);
        }
    }
}
