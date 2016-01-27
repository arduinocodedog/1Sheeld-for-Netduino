using OneSheeldClasses;

namespace SimpleVIBRATION
{
    public class Vibration : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            // Vibrate once for 1 second. 
            VIBRATION.start(1000);

            // Wait for 8 seconds.
            OneSheeld.delay(8000);

            // Vibrate for 1 second every 5 seconds.
            VIBRATION.start(1000, 5000);

            // Wait for 12 seconds.
            OneSheeld.delay(12000);

            // Stop the vibration.
            VIBRATION.stop();
        }
    }
}
