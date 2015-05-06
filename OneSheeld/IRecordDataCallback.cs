using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IRecordDataCallback
    {
        void OnRecordData(byte id, byte[] data, byte dataLength);
    }
}
