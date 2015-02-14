using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IRemoteCallback
    {
        void OnNewMessage(string address, string key, float value);
        void OnNewMessage(string address, string key, string value);
    }
}
