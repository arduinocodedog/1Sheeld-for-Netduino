using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IRowColCallback
    {
        void OnChange(byte row, byte col);
    }
}
