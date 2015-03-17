using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedSKYPE
{
    public class Skype
    {
        OneSheeld sheeld = null;
        bool didWeCall = false;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            /* If PushButton is pressed, send an SMS Message */
            if (OneSheeld.PUSHBUTTON.isPressed())
            {
                /* Check that we haven't sent the SMS already. */
                if (!didWeCall)
                {
                    /* Send the SMS. */
                    OneSheeld.SKYPE.call("echo123");
                    /* Set the flag. */
                    didWeCall = true;
                }
            }
            else
            {
                /* Reset the flag. */
                didWeCall = false;
            }
        }
    }
}
