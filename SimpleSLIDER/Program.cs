using Microsoft.SPOT;

namespace SimpleSLIDER
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            SliderCallback callback = new SliderCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Slider slider = new Slider();
            slider.Setup();
            while (true)
                slider.Loop();
            */
        }

    }
}
