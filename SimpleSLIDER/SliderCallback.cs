using OneSheeldClasses;

namespace SimpleSLIDER
{
    public class SliderCallback : OneSheeldUser, IOneSheeldSketch, IByteCallback
    {
        int waitvalue = 0;

        public void Setup()
        {
            OneSheeld.begin();

            SLIDER.setOnValueChange(this);
        }

        public void Loop()
        {
            BUZZER.buzzOn();
            OneSheeld.delay(waitvalue);
            BUZZER.buzzOff();
            OneSheeld.delay(waitvalue);
        }

        public void OnChange(byte value)
        {
            waitvalue = 1000 - (3 * value);
        }
    }
}
