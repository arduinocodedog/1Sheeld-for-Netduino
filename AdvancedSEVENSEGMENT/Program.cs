using System;
using Microsoft.SPOT;

namespace AdvancedSEVENSEGMENT
{
    public class Program
    {
        public static void Main()
        {
            // write your code here
            SevenSegment sevensegment = new SevenSegment();
            sevensegment.Setup();
            while (true)
                sevensegment.Loop();
        }

    }
}
