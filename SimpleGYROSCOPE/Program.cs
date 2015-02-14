using System;
using Microsoft.SPOT;

namespace SimpleGYROSCOPE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            GyroscopeCallback callback = new GyroscopeCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Gyroscope gyroscope = new Gyroscope();
            gyroscope.Setup();
            while (true)
                gyroscope.Loop();
            */
        }

    }
}
