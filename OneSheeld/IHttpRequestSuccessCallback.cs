using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IHttpRequestSuccessCallback
    {
        void OnSuccess(HttpResponse response);
    }
}
