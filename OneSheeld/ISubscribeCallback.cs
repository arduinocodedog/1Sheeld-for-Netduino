using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface ISubscribeCallback
    {
        void OnSubscribeOrDigitalChange(byte incomingPinNumber, bool incommingPinValue);
    }
}
