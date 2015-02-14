using System;
using Microsoft.SPOT;

namespace SimpleGRAVITY
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            GravityCallback callback = new GravityCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Gravity gravity = new Gravity();
            gravity.Setup();
            while (true)
                gravity.Loop();
            */
        }

    }
}
