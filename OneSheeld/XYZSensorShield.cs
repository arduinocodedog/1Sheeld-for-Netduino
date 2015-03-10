using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class XYZSensorShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        IXYZFloatCallback changeCallBack = null;
        bool isCallBackAssigned = false;
        byte ShieldFunctionID = 0x00;
        byte ShieldID = 0x00;

        protected float valueX = 0.0f, valueY = 0.0f, valueZ = 0.0f;

        public XYZSensorShield(OneSheeld onesheeld, byte funcid, byte shieldid)
            : base(onesheeld, shieldid)
        {
            Sheeld = onesheeld;
            ShieldFunctionID = funcid;
            ShieldID = shieldid;
        }

        public float getX()
        {
            return valueX;
        }

        public float getY()
        {
            return valueY;
        }

        public float getZ()
        {
            return valueZ;
        }

        public override void processData()
        {
            //Check Function-ID
            byte functionId = Sheeld.getFunctionId();

            if (functionId == ShieldFunctionID)
            {
                //Process X-Axis Value
                valueX = Sheeld.convertBytesToFloat(Sheeld.getArgumentData(0));
                //Process Y-Axis Value
                valueY = Sheeld.convertBytesToFloat(Sheeld.getArgumentData(1));
                //Process Z-Axis Value
                valueZ = Sheeld.convertBytesToFloat(Sheeld.getArgumentData(2));
                
                //User Function Invoked
                if (isCallBackAssigned)
                {
                    changeCallBack.OnChange(valueX, valueY, valueZ);
                }
            }	
        }

        public void setOnValueChange(IXYZFloatCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }

    }
}
