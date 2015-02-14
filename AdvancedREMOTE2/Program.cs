using System;
using Microsoft.SPOT;

namespace AdvancedREMOTE2
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Remote2 remote2 = new Remote2();
            remote2.Setup();
            while (true)
                remote2.Loop();
        }

    }
}
