/*

Face Detector Shield Example

This example shows an application on 1Sheeld's face detector shield.

By using this example, you can unlock a door by winking three times to
the front camera.

*/
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleFACEDETECTOR
{
    class FaceDetector : OneSheeldUser, IOneSheeldSketch
    {
        /* led and relay output ports */
        OutputPort led = null;
        OutputPort relay = null;

        /* a counter */
        int counter = 0;

        public void Setup()
        {
            /* Start communication. */
            OneSheeld.begin();

            /* Initialize led and relay output ports */
            led = new OutputPort(Pins.GPIO_PIN_D13, false);
            relay = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            /* Get the first face and check if it winks. */
            if (FACEDETECTOR.getVisibleFace(0).leftEyeOpened == 0)
            {
                /* Turn on the LED when winking. */
                led.Write(true);
                /* Count the winks. */
                counter++;
                /* A slight delay. */
                OneSheeld.delay(1000);
            }
            else
            {
                /* Turn off the LED. */
                led.Write(false);
            }

            /* Check the number of winks. */
            if (counter == 3)
            {
                /* Send success. */
                TERMINAL.println("Success");
                /* Open the lock. */
                relay.Write(true);
                /* Reset counter. */
                counter = 0;
            }
        }
    }
}
