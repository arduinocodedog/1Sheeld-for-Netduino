using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ShieldParent
    {
        byte ShieldID = 0x00;
        IShieldChild Child = null;
        OneSheeld Sheeld = null;

        public ShieldParent(OneSheeld sheeld, byte shieldid)
        {
            Sheeld = sheeld;
            ShieldID = shieldid;
        }

        public void SetChild(IShieldChild child)
        {
            Child = child;
        }

        public void processFrame()
        {
            if (Sheeld.getShieldId() != ShieldID)
                return;

            byte functionID = Sheeld.getFunctionId();
            if (functionID != CHECK_SELECTED)
                Child.processData();
        }

        // Process Frame Handling
        const byte CHECK_SELECTED = 0xff;
    }
}
