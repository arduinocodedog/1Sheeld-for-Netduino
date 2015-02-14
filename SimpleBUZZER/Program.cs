using System;
using Microsoft.SPOT;

namespace SimpleBUZZER
{
    public class Program
    {
        public static void Main()
        {
             // write your code here
            Buzzer buzzer = new Buzzer();
            buzzer.Setup();
            while (true)
                buzzer.Loop();
        }

    }
}
