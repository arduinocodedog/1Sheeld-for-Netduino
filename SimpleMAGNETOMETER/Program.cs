using System;
using Microsoft.SPOT;

namespace SimpleMAGNETOMETER
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Magnetometer magnetometer = new Magnetometer();
            magnetometer.Setup();
            while (true)
                magnetometer.Loop();
        }

    }
}
