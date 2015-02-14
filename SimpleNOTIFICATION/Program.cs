using System;
using Microsoft.SPOT;

namespace SimpleNOTIFICATION
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Notification notification = new Notification();
            notification.Setup();
            while (true)
                notification.Loop();
        }

    }
}
