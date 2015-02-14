using System;
using Microsoft.SPOT;

namespace SimpleLED
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Led led = new Led();
            led.Setup();
            while (true)
                led.Loop();
        }
    }
}
