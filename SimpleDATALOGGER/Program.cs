using System;
using Microsoft.SPOT;

namespace SimpleDATALOGGER
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            DataLogger datalogger = new DataLogger();
            datalogger.Setup();
            while (true)
                datalogger.Loop();
        }

    }
}
