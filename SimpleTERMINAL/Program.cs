using System;
using Microsoft.SPOT;

namespace SimpleTERMINAL
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            Terminal terminal = new Terminal();
            terminal.Setup();
            while (true)
                terminal.Loop();

        }

    }
}
