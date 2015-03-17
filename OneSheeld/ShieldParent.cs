using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ShieldParent
    {
        ShieldIds ShieldID = 0x00;
        static OneSheeld Sheeld = null;
        static OneSheeld oneSheeldInstance = null;
        static bool oneSheeldInstanceAvailable = false;
        bool isCallBackSet = false;
        ISelectedCallback callBack = null;

        public ShieldParent(OneSheeld sheeld, ShieldIds shieldid)
        {
            Sheeld = sheeld;
            ShieldID = shieldid;
            OneSheeld.addToShieldsArray(this);
        }

        public void select()
        {
            Sheeld.sendPacket(ShieldID, 0x00, SELECT_SHIELD, 0x00, null);
        }

        public void deselect()
        {
            Sheeld.sendPacket(ShieldID, 0x00, DESELECT_SHIELD, 0x00, null);
        }

        public void setOnSelected(ISelectedCallback userCallback)
        {
            Sheeld.sendPacket(ShieldID, 0x00, QUERY_SELECTED, 0x00, null);
            isCallBackSet = true;
            callBack = userCallback;
        }

        public byte getShieldID()
        {
            return (byte)ShieldID;
        }

        protected void enteringACallback()
        {
            Sheeld.enteringACallback();
        }

        protected void exitingACallback()
        {
            Sheeld.exitingACallback();
        }

        protected bool isInACallback()
        {
            return Sheeld.isInACallback();
        }

        static public void setOneSheeldInstance(OneSheeld instance)
        {
            oneSheeldInstance = instance;
            oneSheeldInstanceAvailable = true;
        }

        static public void unSetOneSheeldInstance()
        {
            oneSheeldInstance = null;
            oneSheeldInstanceAvailable = false;
        }

        static public OneSheeld getOneSheeldInstance()
        {
            if (oneSheeldInstanceAvailable)
                return oneSheeldInstance;
            else
                return Sheeld;
        }

        public void processFrame()
        {
            if (getOneSheeldInstance().getShieldId() != (byte)ShieldID)
                return;

            byte functionID = getOneSheeldInstance().getFunctionId();
            if (functionID == CHECK_SELECTED)
            {
                if (isCallBackSet && !isInACallback())
                {
                    enteringACallback();
                    callBack.selected();
                    exitingACallback();
                }
            }
            else
                processData();
        }

        // Netduino specific - I could do abstract ... but it causes more hassles than it's worth
        public virtual void processData() { }

        // Process Frame Handling
        const byte CHECK_SELECTED = 0xFF;

        /* Output functions ID. */
        const byte QUERY_SELECTED = 0xFF;
        const byte SELECT_SHIELD = 0xFE;
        const byte DESELECT_SHIELD = 0xFD;
    }
}
