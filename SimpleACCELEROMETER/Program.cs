using System;
using Microsoft.SPOT;

namespace SimpleACCELEROMETER
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            AccelerometerCallback callback = new AccelerometerCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Accelerometer accelerometer = new Accelerometer();
            accelerometer.Setup();
            while (true)
                accelerometer.Loop();
            */
        }

    }
}
