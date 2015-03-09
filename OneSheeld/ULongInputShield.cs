using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ULongInputShield : ShieldParent, IShieldChild
    {
        OneSheeld Sheeld = null;
        IULongCallback changeCallBack = null;
        bool isCallBackAssigned = false;
        int dataSize = 0;
        ulong value = 0L;
        ulong[] data = null;
        byte ShieldFunctionID = 0x00;
        byte ShieldID = 0x00;

        public ULongInputShield(OneSheeld onesheeld, byte funcid, byte shieldid, int bytesused)
            : base(onesheeld, shieldid)
        {
            Sheeld = onesheeld;
            dataSize = bytesused;
            ShieldFunctionID = funcid;
            ShieldID = shieldid;

            data = new ulong[dataSize];

            SetChild(this);
        }

        public ulong getValue()
        {
            return value;
        }

        void IShieldChild.processData()
        {
            //Check Function-ID
            byte functionId = Sheeld.getFunctionId();

            if (functionId == ShieldFunctionID)
            {
                value = 0;
                data[0] = Sheeld.getArgumentData(0)[0];
                data[1] = Sheeld.getArgumentData(0)[1];
                if (dataSize > 2)
                    data[2] = Sheeld.getArgumentData(0)[2];
                value |= data[0];
                value |= (data[1] << 8);
                if (dataSize > 2)
                    value |= (data[2] << 16);                

                //User Function Invoked
                if (isCallBackAssigned)
                {
                    changeCallBack.OnChange(value);
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
