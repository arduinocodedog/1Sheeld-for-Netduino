using System;
using Microsoft.SPOT;

namespace SimplePROXIMITY
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            ProximityCallback callback = new ProximityCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Proximity proximity = new Proximity();
            proximity.Setup();
            while (true)
                proximity.Loop();
            */
        }

    }
}
