using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IGPSCallback
    {
        void OnChange(float lattitude, float longitude);
    }
}
