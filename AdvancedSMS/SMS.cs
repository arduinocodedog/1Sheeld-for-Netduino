using OneSheeldClasses;

namespace AdvancedSMS
{
    public class SMS : OneSheeldUser, IOneSheeldSketch
    {
        bool isMessageSent = false;

        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            /* If PushButton is pressed, send an SMS Message */
            if (PUSHBUTTON.isPressed())
            {
                /* Check that we haven't sent the SMS already. */
                if (!isMessageSent)
                {
                    /* Send the SMS. */
                    SMS.send("1234567890", "Push a button, send a text!");
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
