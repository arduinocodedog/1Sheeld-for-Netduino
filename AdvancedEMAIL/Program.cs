using System;
using Microsoft.SPOT;

namespace AdvancedEMAIL
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Email email = new Email();
            email.Setup();
            while (true)
                email.Loop();
        }

    }
}
