using System;
using Microsoft.SPOT;

namespace AdvancedCLOCK
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Clock clock = new Clock();
            clock.Setup();
            while (true)
                clock.Loop();
        }

    }
}
