using System;
using Microsoft.SPOT;

namespace SimpleTOGGLEBUTTON
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new ToggleButtonCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new ToggleButton());
        }
    }
}
