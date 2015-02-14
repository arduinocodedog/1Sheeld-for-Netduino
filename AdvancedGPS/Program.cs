using System;

using Microsoft.SPOT;

namespace AdvancedGPS
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            GPS gps = new GPS();
            gps.Setup();
            while (true)
                gps.Loop();
        }

    }
}
