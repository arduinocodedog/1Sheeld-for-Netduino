using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IColorCallback
    {
        void OnColorReceived(ColorClass color);
    }
}
