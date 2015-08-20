using OneSheeldClasses;

namespace AdvancedEMAIL
{
    public class Email : OneSheeldUser, IOneSheeldSketch, IBoolCallback
    {
        bool IsButtonPressed = false;
        bool MessageSent = false;

        public void Setup()
        {
            OneSheeld.begin();

            PUSHBUTTON.setOnButtonStatusChange(this);
        }

        public void Loop()
        {
            if (!MessageSent)
            {
                if (IsButtonPressed)
                {
                    EMAIL.send("example@example.com", "Button pressed!", "Hi, someone pressed the button!");
                    MessageSent = true;
                }
            }
        }

        public void OnChange(bool isPressed)
        {
            IsButtonPressed = isPressed;
            if (!IsButtonPressed)
                MessageSent = false;
        }
    }
}
