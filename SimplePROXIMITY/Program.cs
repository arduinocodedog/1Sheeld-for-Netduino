using System;
using Microsoft.SPOT;

namespace SimplePROXIMITY
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new ProximityCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Proximity());
        }

    }
}
