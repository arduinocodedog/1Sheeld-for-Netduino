using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface ISMSCallback
    {
        void OnSMSReceive(string number, string text);
    }
}
