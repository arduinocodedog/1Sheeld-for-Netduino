using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IInternetErrorCallback
    {
        void OnError(int requestid, int errorNumber);
    }
}
