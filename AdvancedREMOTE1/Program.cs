using System;
using Microsoft.SPOT;

namespace AdvancedREMOTE1
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Remote1 remote1 = new Remote1();
            remote1.Setup();
            while (true)
                remote1.Loop();
        }
    }
}
