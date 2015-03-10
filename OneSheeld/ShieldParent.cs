using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public abstract class ShieldParent
    {
        byte ShieldID = 0x00;
        OneSheeld Sheeld = null;

        public ShieldParent(OneSheeld sheeld, byte shieldid)
        {
            Sheeld = sheeld;
            ShieldID = shieldid;
        }

        public void processFrame()
        {
            if (Sheeld.getShieldId() != ShieldID)
                return;

            byte functionID = Sheeld.getFunctionId();
            if (functionID != CHECK_SELECTED)
                processData();
        }

        public abstract void processData();

        // Process Frame Handling
        const byte CHECK_SELECTED = 0xff;
    }
}
