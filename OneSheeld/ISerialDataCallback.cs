using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface ISerialDataCallback
    {
        void OnNewSerialData(byte data);
    }
}
