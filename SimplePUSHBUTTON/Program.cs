using System;
using Microsoft.SPOT;

namespace SimplePUSHBUTTON
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            PushButtonCallback callback = new PushButtonCallback();
            callback.Setup();
            while (true)
                callback.Loop();
 
            /*
            // Non-Callback Version
            PushButton pushbutton = new PushButton();
            pushbutton.Setup();
            while (true)
                pushbutton.Loop();
            */
        }
    }
}
