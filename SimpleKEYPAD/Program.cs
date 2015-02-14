using System;
using Microsoft.SPOT;

namespace SimpleKEYPAD
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            KeypadCallback callback = new KeypadCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Keypad keypad = new Keypad();
            keypad.Setup();
            while (true)
                keypad.Loop();
            */
        }

    }
}
