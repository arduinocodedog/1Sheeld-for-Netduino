using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ULongInputShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        IULongCallback changeCallBack = null;
        bool isCallBackAssigned = false;
        int dataSize = 0;
        ulong value = 0L;
        ulong[] data = null;
        byte ShieldFunctionID = 0x00;
        ShieldIds ShieldID = 0x00;

        public ULongInputShield(OneSheeld onesheeld, byte funcid, ShieldIds shieldid, int bytesused)
            : base(onesheeld, shieldid)
        {
            Sheeld = onesheeld;
            dataSize = bytesused;
            ShieldFunctionID = funcid;
            ShieldID = shieldid;

            data = new ulong[dataSize];
        }

        public ulong getValue()
        {
            return value;
        }

        public override void processData()
        {
            //Check Function-ID
            byte functionId = getOneSheeldInstance().getFunctionId();

            if (functionId == ShieldFunctionID)
            {
                value = 0;
                data[0] = getOneSheeldInstance().getArgumentData(0)[0];
                data[1] = getOneSheeldInstance().getArgumentData(0)[1];
                if (dataSize > 2)
                    data[2] = getOneSheeldInstance().getArgumentData(0)[2];
                value |= data[0];
                value |= (data[1] << 8);
                if (dataSize > 2)
                    value |= (data[2] << 16);                

                //User Function Invoked
                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    changeCallBack.OnChange(value);
                    exitingACallback();
                }
            }	
        }

        public void setOnValueChange(IULongCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }
    }
}
