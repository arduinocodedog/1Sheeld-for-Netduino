using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpJsonArrayLengthResponseCallback
    {
        void OnJsonArrayLengthResponse(JsonKeyChain chain, ulong size);
    }
}
