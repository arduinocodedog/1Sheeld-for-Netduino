using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpRequestFailureCallback
    {
        void OnFailure(HttpResponse response);
    }
}
