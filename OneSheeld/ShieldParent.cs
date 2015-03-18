using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ShieldParent
    {
        ShieldIds ShieldID = 0x00;
        static OneSheeldClass oneSheeldInstance = null;
        static bool oneSheeldInstanceAvailable = false;
        bool isCallBackSet = false;
        ISelectedCallback callBack = null;

        public ShieldParent(ShieldIds shieldid)
        {
            ShieldID = shieldid;
            OneSheeldClass.addToShieldsArray(this);
        }

        public void select()
        {
            OneSheeldMain.OneSheeld.sendPacket(ShieldID, 0x00, SELECT_SHIELD, 0x00, null);
        }

        public void deselect()
        {
            OneSheeldMain.OneSheeld.sendPacket(ShieldID, 0x00, DESELECT_SHIELD, 0x00, null);
        }

        public void setOnSelected(ISelectedCallback userCallback)
        {
            OneSheeldMain.OneSheeld.sendPacket(ShieldID, 0x00, QUERY_SELECTED, 0x00, null);
            isCallBackSet = true;
            callBack = userCallback;
        }

        public byte getShieldID()
        {
            return (byte)ShieldID;
        }

        protected void enteringACallback()
        {
            OneSheeldMain.OneSheeld.enteringACallback();
        }

        protected void exitingACallback()
        {
            OneSheeldMain.OneSheeld.exitingACallback();
        }

        protected bool isInACallback()
        {
            return OneSheeldMain.OneSheeld.isInACallback();
        }

        public static void setOneSheeldInstance(OneSheeldClass instance)
        {
            ShieldParent.oneSheeldInstance = instance;
            ShieldParent.oneSheeldInstanceAvailable = true;
        }

        public static void unSetOneSheeldInstance()
        {
            ShieldParent.oneSheeldInstance = null;
            ShieldParent.oneSheeldInstanceAvailable = false;
        }

        public static OneSheeldClass getOneSheeldInstance()
        {
            if (ShieldParent.oneSheeldInstanceAvailable)
                return ShieldParent.oneSheeldInstance;
            else
                return OneSheeldMain.OneSheeld;
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
