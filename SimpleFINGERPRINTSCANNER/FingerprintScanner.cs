/*

FingerprintScanner Shield Example

This example shows an application on 1Sheeld's fingerprint scanner shield.

By using this example, you can turn on an LED using your fingerprint.

*/
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;
using System.Threading;

namespace SimpleFINGERPRINTSCANNER
{
    class FingerprintScanner : OneSheeldUser, IOneSheeldSketch
    {
        OutputPort led = null;

        public void Setup()
        {
            /* Start communication. */
            OneSheeld.begin();

            /* Initialize led port */
            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (FINGERPRINTSCANNER.isNewFingerScanned() && FINGERPRINTSCANNER.isVerified())
            {
                led.Write(true);
                Thread.Sleep(1000);
                led.Write(false);
            }
        }

    }
}
