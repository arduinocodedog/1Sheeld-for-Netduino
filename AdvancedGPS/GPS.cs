using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedGPS
{
    public class GPS
    {
        OneSheeld sheeld = null;
        bool isInRange = false;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            /* If PushButton is pressed, check GPS and send an SMS Message */
            if (OneSheeld.PUSHBUTTON.isPressed())
            {
                if (OneSheeld.GPS.isInRange(30.0831008f, 31.3242943f, 100.0f))
                {
                    /* Check that we haven't sent the SMS already. */
                    if (!isInRange)
                    {
                        /* Send the SMS. */
                        OneSheeld.SMS.send("1234567890", "Smartphone is In Range.");
                        /* Set the flag. */
                        isInRange = true;
                    }
                }
                else
                {
                    /* Reset the flag. */
                    OneSheeld.SMS.send("1234567890", "Smartphone is not In Range.");
                    isInRange = false;
                }
            }
        }

    }
}
