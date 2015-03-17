using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpResponseJsonResponseCallback
    {
        void OnJsonResponse(JsonKeyChain chain, byte[] data);
    }
}
