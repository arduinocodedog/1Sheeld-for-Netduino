using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IXYZFloatCallback
    {
        void OnChange(float valueX, float valueY, float valueZ);
    }
}
