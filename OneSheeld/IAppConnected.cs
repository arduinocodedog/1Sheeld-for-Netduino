using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IAppConnected
    {
        void OnAppConnected(bool connected);
    }
}
