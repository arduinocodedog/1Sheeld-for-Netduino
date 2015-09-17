using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IShieldFrameCallback
    {
        void OneNewShieldFrame(byte shieldID, byte instanceID, byte functionID, byte argNo, byte[] argumentL, byte[][] arguments);
    }
}
