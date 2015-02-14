using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface ISByteCallback
    {
        void OnChange(sbyte val);
    }
}
