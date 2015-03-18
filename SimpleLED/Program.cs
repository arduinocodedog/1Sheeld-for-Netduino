using System;
using Microsoft.SPOT;

namespace SimpleLED
{
    public class Program
    {
        public static void Main()
        {
            OneSheeldClasses.OneSheeldUser.Run(new Led());
        }
    }
}
