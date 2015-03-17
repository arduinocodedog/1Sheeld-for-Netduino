using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpResponseErrorCallback
    {
        void OnError(int errorNumber);
    }
}
