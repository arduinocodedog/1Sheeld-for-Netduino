﻿using System;
using Microsoft.SPOT;

namespace SimpleINTERNET
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Use Callback
            Internet internet = new Internet();
            internet.Setup();
            while (true)
                internet.Loop();
        }

    }
}
