using System;
using Microsoft.SPOT;

namespace SimpleNFC
{
    public class Program
    {
        public static void Main()
        {
            // Don't Use Callbacks
            // OneSheeldClasses.OneSheeldUser.Run(new NFC());

            // Use Callbacks
            OneSheeldClasses.OneSheeldUser.Run(new NFCCallback());
        }

    }
}
