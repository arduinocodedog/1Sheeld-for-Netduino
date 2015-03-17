using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpResponseGetHeaderCallback
    {
        void OnGetHeader(byte[] incomingheaderName, byte[] IncomingHeaderValue);
    }
}
