using System;
using Microsoft.SPOT;

namespace SimplePHONE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Phone phone = new Phone();
            phone.Setup();
            while (true)
                phone.Loop();
        }

    }
}
