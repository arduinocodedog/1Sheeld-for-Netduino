using OneSheeldClasses;

namespace SimpleLED
{
    public class Led : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            LED.setHigh();
            OneSheeld.delay(1000);
            LED.setLow();
            OneSheeld.delay(1000);
        }
    }
}
