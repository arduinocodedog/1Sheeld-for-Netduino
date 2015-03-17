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
        ShieldIds ShieldID = 0x00;

        protected float valueX = 0.0f, valueY = 0.0f, valueZ = 0.0f;

        public XYZSensorShield(OneSheeld onesheeld, byte funcid, ShieldIds shieldid)
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
            byte functionId = getOneSheeldInstance().getFunctionId();

            if (functionId == ShieldFunctionID)
            {
                //Process X-Axis Value
                valueX = Sheeld.convertBytesToFloat(getOneSheeldInstance().getArgumentData(0));
                //Process Y-Axis Value
                valueY = Sheeld.convertBytesToFloat(getOneSheeldInstance().getArgumentData(1));
                //Process Z-Axis Value
                valueZ = Sheeld.convertBytesToFloat(getOneSheeldInstance().getArgumentData(2));
                
                //User Function Invoked
                if (isCallBackAssigned & !isInACallback())
                {
                    enteringACallback();
                    changeCallBack.OnChange(valueX, valueY, valueZ);
                    exitingACallback();
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
