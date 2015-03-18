using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedPRESSURE
{
    public class Pressure : OneSheeldUser, IOneSheeldSketch
    {
        bool isMessageSent = false;

        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            /* Always read the pressure value and check if it exceeds a certain value. */
            if (PRESSURE.getValue() > 1008)
            {
                /* Check that we haven't sent the SMS already. */
                if (!isMessageSent)
                {
                    /* Send the SMS. */
                    SMS.send("1234567890", "Pressure is getting high in here!");
                    /* Set the flag. */
                    isMessageSent = true;
                }
            }
            else
            {
                /* Reset the flag. */
                isMessageSent = false;
            }
        }
    }
}
