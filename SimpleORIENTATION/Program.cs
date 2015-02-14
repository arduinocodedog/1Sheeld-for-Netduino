using System;
using Microsoft.SPOT;

namespace SimpleORIENTATION
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            OrientationCallback callback = new OrientationCallback();
            callback.Setup();
            while (true)
                callback.Loop();

            /*
            // Non-Callback Version
            Orientation orientation = new Orientation();
            orientation.Setup();
            while (true)
                orientation.Loop();
            */
        }

    }
}
