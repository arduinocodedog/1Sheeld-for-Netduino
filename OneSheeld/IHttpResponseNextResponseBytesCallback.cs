using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpResponseNextResponseBytesCallback
    {
        void OnNextResponseBytesUpdate(HttpResponse response);
    }
}
