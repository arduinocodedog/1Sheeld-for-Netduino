using System;
using Microsoft.SPOT;

namespace SimpleGAMEPAD
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new GamePadCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new GamePad());
        }

    }
}
