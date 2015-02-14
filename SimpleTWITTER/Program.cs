using System;
using Microsoft.SPOT;

namespace SimpleTWITTER
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Twitter twitter = new Twitter();
            twitter.Setup();
            while (true)
                twitter.Loop();
        }

    }
}
