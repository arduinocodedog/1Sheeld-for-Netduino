using System;
using Microsoft.SPOT;

namespace SimpleMIC
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            MicCallback callback = new MicCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Mic mic = new Mic();
            mic.Setup();
            while (true)
                mic.Loop();
            */
        }

    }
}
