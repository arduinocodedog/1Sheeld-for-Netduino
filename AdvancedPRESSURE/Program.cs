using System;
using Microsoft.SPOT;

namespace AdvancedPRESSURE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Pressure pressure = new Pressure();
            pressure.Setup();
            while (true)
                pressure.Loop();

        }

    }
}
