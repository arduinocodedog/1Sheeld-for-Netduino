using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IRecordParsedCallback
    {
        void OnRecordParsed(byte id, byte[] data);
    }
}
