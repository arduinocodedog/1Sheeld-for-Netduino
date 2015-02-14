using System;
using Microsoft.SPOT;

namespace SimpleGAMEPAD
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            GamePadCallback callback = new GamePadCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            GamePad gamepad = new GamePad();
            gamepad.Setup();
            while (true)
                gamepad.Loop();
            */

        }

    }
}
