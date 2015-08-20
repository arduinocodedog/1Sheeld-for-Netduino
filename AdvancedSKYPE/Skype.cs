using OneSheeldClasses;

namespace AdvancedSKYPE
{
    public class Skype : OneSheeldUser, IOneSheeldSketch
    {
        bool didWeCall = false;

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
                if (!didWeCall)
                {
                    /* Send the SMS. */
                    SKYPE.call("echo123");
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
