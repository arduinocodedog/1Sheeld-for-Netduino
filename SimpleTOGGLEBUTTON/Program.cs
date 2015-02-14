using System;
using Microsoft.SPOT;

namespace SimpleTOGGLEBUTTON
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            ToggleButtonCallback callback = new ToggleButtonCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            ToggleButton togglebutton = new ToggleButton();
            togglebutton.Setup();
            while (true)
                togglebutton.Loop();
            */
        }
    }
}
