using System;
using Microsoft.SPOT;

namespace SimpleFACEBOOK
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Facebook facebook = new Facebook();
            facebook.Setup();
            while (true)
                facebook.Loop();

        }

    }
}
