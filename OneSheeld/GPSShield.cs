using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GPSShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        bool isCallBackAssigned = false;
        IGPSCallback changeCallBack = null;

        float LatValue = 0.0f;
        float LonValue = 0.0f;
        bool isInit = false;

        byte[] getfloat = null;

        public GPSShield(OneSheeld onesheeld)
            : base(onesheeld, (byte)ShieldIds.GPS_ID)
        {
            Sheeld = onesheeld;
            getfloat = new byte[4];
        }
        
        public override void processData()
        {
            //Checking Function-ID
            byte functionId = Sheeld.getFunctionId();
            if (functionId == GPS_VALUE)
            {
                //Process Lattitude Value
                getfloat[0] = Sheeld.getArgumentData(0)[0];
                getfloat[1] = Sheeld.getArgumentData(0)[1];
                getfloat[2] = Sheeld.getArgumentData(0)[2];
                getfloat[3] = Sheeld.getArgumentData(0)[3];
                LatValue = Sheeld.convertBytesToFloat(getfloat);

                //Process Longitude Value
                getfloat[0] = Sheeld.getArgumentData(1)[0];
                getfloat[1] = Sheeld.getArgumentData(1)[1];
                getfloat[2] = Sheeld.getArgumentData(1)[2];
                getfloat[3] = Sheeld.getArgumentData(1)[3];
                LonValue = Sheeld.convertBytesToFloat(getfloat);

                isInit = true;  								
            }

            //Users Function Invoked
            if (isCallBackAssigned)
            {
                changeCallBack.OnChange(LatValue, LonValue);
            }
        }

        public float getLatitude()
        {
            return LatValue;
        }

        public float getLongitude()
        {
            return LonValue;
        }

        public bool isInRange(float usersValue1, float usersValue2, float range)
        {
            if (!isInit) 
                return false;

            float x = getDistance(usersValue1, usersValue2);
            
            if (x >= 0 && x < range)
                return true;

            return false;
        }

        public float getDistance(float x , float y)			//x and y is the lattitude and the longitude inserted by the user 
        {
	        if (!isInit)
                return 0;
	        float dLat = radian(x-LatValue);			//difference betwwen the two lattitude point  
	        float dLon = radian(y-LonValue);			//difference betwwen the two longitude point  

            float chordProcess = (float)(System.Math.Sin(dLat / 2) * System.Math.Sin(dLat / 2) + System.Math.Sin(dLon / 2) * System.Math.Sin(dLon / 2) * System.Math.Cos(radian(LatValue)) * System.Math.Cos(radian(x)));
            float angularDistance = (float)(2 * System.Math.Atan2(System.Math.Sqrt(chordProcess), System.Math.Sqrt(1 - chordProcess)));
	        float actualDsitance  = (RADUIS_OF_EARTH*angularDistance)*1000;			//getting the actuall distance in meters

	        return actualDsitance;											
        }

        public float radian(float value)
        {
	        float radianValue = value*(PI/180);
	        return radianValue;
        }

        public void setOneValueChange(IGPSCallback userCallback)
        {
            changeCallBack = userCallback;
            isCallBackAssigned = true;
        }

        // Literal Constants
        const int RADUIS_OF_EARTH = 6371;
        const float PI = 3.1415926535897932384626433832795f;

        //Input Function ID 
        const byte GPS_VALUE = 0x01;
    }
}
