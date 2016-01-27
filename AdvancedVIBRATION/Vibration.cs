using OneSheeldClasses;

namespace AdvancedVIBRATION
{
    public class Vibration : OneSheeldUser, IOneSheeldSketch
    {
        int[] patternOne = { 1000, 2000, 1000, 2000, 1000, 2000 };
        int patternOneSize = 6;

        int[] patternTwo = { 100, 250, 100, 400 };
        int patternTwoSize = 4; 

        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            // Vibrate as the first pattern once. 
            VIBRATION.start(patternOneSize, patternOne);

            // Wait for 9 seconds.
            OneSheeld.delay(9000);

            // Vibrate as the second pattern and keep repeating the vibration every 300 ms.
            VIBRATION.start(patternTwoSize, patternTwo, 300);

            // Wait for 10 seconds.
            OneSheeld.delay(10000);

            // Stop the vibration.
            VIBRATION.stop();
        }
    }
}