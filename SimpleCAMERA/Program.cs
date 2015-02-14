using System;
using Microsoft.SPOT;

namespace SimpleCAMERA
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Camera camera = new Camera();
            camera.Setup();
            while (true)
                camera.Loop();

        }

    }
}
