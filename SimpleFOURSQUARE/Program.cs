using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace SimpleFOURSQUARE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            Foursquare foursquare = new Foursquare();
            foursquare.Setup();
            while (true)
                foursquare.Loop();
        }

    }
}
