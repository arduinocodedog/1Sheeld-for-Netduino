using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IInteractiveShapeChangeCallback
    {
        void OnChange(byte data);
    }
}
