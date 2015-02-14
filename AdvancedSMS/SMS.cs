using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedSMS
{
    public class SMS
    {
        OneSheeld sheeld = null;
        bool isMessageSent = false;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            /* If PushButton is pressed, send an SMS Message */
            if (sheeld.PUSHBUTTON.isPressed())
            {
                /* Check that we haven't sent the SMS already. */
                if (!isMessageSent)
                {
                    /* Send the SMS. */
                    sheeld.SMS.send("1234567890", "Push a button, send a text!");
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
