using System;
using Microsoft.SPOT;

namespace SimpleKEYBOARD
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            KeyboardCallback callback = new KeyboardCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Keyboard keyboard = new Keyboard();
            keyboard.Setup();
            while (true)
                keyboard.Loop();
             */
        }

    }
}
