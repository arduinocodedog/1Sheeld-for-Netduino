using System;
using Microsoft.SPOT;

namespace SimpleKEYPAD
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new KeypadCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Keypad());
        }

    }
}
