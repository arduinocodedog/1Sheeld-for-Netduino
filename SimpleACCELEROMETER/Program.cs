using System;
using Microsoft.SPOT;

namespace SimpleACCELEROMETER
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new AccelerometerCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Accelerometer());
        }

    }
}
