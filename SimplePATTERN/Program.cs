using System;
using Microsoft.SPOT;

namespace SimplePATTERN
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            /*
            // Don't Use Callback
            Pattern pattern = new Pattern();
            pattern.Setup();
            while (true)
                pattern.Loop();
            */

            // Use Callback
            PatternCallback callback = new PatternCallback();
            callback.Setup();
            while (true)
                callback.Loop();
        }

    }
}
