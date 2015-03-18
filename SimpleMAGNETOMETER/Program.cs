using System;
using Microsoft.SPOT;

namespace SimpleMAGNETOMETER
{
    public class Program
    {
        public static void Main()
        {
            OneSheeldClasses.OneSheeldUser.Run(new Magnetometer());
        }

    }
}
