using System;
using Microsoft.SPOT;

namespace AdvancedLCD
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Lcd lcd = new Lcd();
            lcd.Setup();
            while (true)
                lcd.Loop();
        }

    }
}
