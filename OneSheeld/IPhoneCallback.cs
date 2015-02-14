using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IPhoneCallback
    {
        void OnCallStatusChange(bool isRinging, string phoneNumber);
    }
}
