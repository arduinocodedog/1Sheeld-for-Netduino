using OneSheeldClasses;

namespace SimpleSLIDER
{
    public class Slider : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            byte value = SLIDER.getValue();
            
            int waitvalue = 1000 - (3 * value);

            BUZZER.buzzOn();
            OneSheeld.delay(waitvalue);
            BUZZER.buzzOff();
            OneSheeld.delay(waitvalue);
        }
    }
}
