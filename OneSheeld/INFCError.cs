using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface INFCError
    {
        void OnError(byte errorNumber);
    }
}
