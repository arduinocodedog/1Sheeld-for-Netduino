using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IFingerprintErrorCallback
    {
        void OnError(byte errorNumber);
    }
}
