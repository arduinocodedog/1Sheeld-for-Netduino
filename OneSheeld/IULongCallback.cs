using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IULongCallback
    {
        void OnChange(ulong val);
    }
}
