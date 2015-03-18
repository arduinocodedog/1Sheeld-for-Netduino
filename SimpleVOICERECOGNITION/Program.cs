using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace SimpleVOICERECOGNITION
{
    public class Program
    {
        public static void Main()
        {
            OneSheeldClasses.OneSheeldUser.Run(new VoiceRecognition());
        }
    }
}
