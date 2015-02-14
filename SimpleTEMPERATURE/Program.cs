using System;
using Microsoft.SPOT;

namespace SimpleTEMPERATURE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            TemperatureCallback callback = new TemperatureCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Temperature temperature = new Temperature();
            temperature.Setup();
            while (true)
                temperature.Loop();
            */
        }

    }
}
