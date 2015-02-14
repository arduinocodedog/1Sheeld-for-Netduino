using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IRemoteShieldCallback
    {
        void OnNewMessage(string key, float value);
        void OnNewMessage(string key, string value);
    }
}
