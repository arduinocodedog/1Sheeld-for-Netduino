using System;
using Microsoft.SPOT;

namespace SimpleLIGHT
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            LightCallback callback = new LightCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Light light = new Light();
            light.Setup();
            while (true)
                light.Loop();
            */
        }

    }
}
