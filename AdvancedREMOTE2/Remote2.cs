using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedREMOTE2
{
    public class Remote2 : OneSheeldUser, IOneSheeldSketch, IRemoteCallback
    {
        public void Setup()
        {
            OneSheeld.begin();

            OneSheeld.setOnNewMessage(this);
        }

        public void Loop() {}

        public void OnNewMessage(string address, string key, float value)
        {
            throw new NotImplementedException();
        }

        public void OnNewMessage(string address, string key, string voiceCommand)
        {
            if (key.Equals("USA"))
                TTS.say(voiceCommand);
        }
    }
}
