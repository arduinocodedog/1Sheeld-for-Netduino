using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface INewFingerScannedCallback
    {
        void OnFingerprintScanned(bool verified);
    }
}
