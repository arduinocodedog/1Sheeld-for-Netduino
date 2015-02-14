using System;
using Microsoft.SPOT;

namespace AdvancedSKYPE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Skype skype = new Skype();
            skype.Setup();
            while (true)
                skype.Loop();

        }

    }
}
