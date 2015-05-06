using System;
using Microsoft.SPOT;

namespace SimpleCOLOR
{
    public class Program
    {
        public static void Main()
        {
            // Don't use Callback
            // OneSheeldClasses.OneSheeldUser.Run(new Color());

            // Use Callback
            OneSheeldClasses.OneSheeldUser.Run(new ColorCallback());
        }
    }
}
