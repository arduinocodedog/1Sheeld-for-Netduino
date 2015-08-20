using OneSheeldClasses;

namespace AdvancedGPS
{
    public class GPS : OneSheeldUser, IOneSheeldSketch
    {
        bool isInRange = false;

        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            /* If PushButton is pressed, check GPS and send an SMS Message */
            if (PUSHBUTTON.isPressed())
            {
                if (GPS.isInRange(30.0831008f, 31.3242943f, 100.0f))
                {
                    /* Check that we haven't sent the SMS already. */
                    if (!isInRange)
                    {
                        /* Send the SMS. */
                        SMS.send("1234567890", "Smartphone is In Range.");
                        /* Set the flag. */
                        isInRange = true;
                    }
                }
                else
                {
                    /* Reset the flag. */
                    SMS.send("1234567890", "Smartphone is not In Range.");
                    isInRange = false;
                }
            }
        }

    }
}
