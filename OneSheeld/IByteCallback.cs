using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IByteCallback
    {
        void OnChange(byte val);
    }
}
