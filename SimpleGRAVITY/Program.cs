using System;
using Microsoft.SPOT;

namespace SimpleGRAVITY
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new GravityCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Gravity());
        }

    }
}
