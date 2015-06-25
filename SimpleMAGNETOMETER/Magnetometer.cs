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
    class Magnetometer : OneSheeldUser, IOneSheeldSketch, ISelectedCallback
    {
        bool isFirstLCDSelection = true;

        public void Setup()
        {
            OneSheeld.begin();

            LCD.begin();

            LCD.setOnSelected(this);
        }

        public void OnSelection()
        {
            if (isFirstLCDSelection)
            {
                isFirstLCDSelection = false;

                LCD.setCursor(0, 0);
                LCD.print("MagneticStrength");
                LCD.setCursor(1, 7);
                LCD.print("Tesla");
            }
        }

        public void Loop()
        {
            //TERMINAL.println(MAGNETOMETER.getMagneticStrength());
            LCD.setCursor(1, 0);
            LCD.print(MAGNETOMETER.getMagneticStrength());
            /* Wait for 1 second. */
            OneSheeld.delay(1000);
        }
    }
}
