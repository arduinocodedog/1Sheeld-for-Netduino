using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface INFCNewTagScanned
    {
        void OnNewTagScanned(NFCTag tag);
    }
}
