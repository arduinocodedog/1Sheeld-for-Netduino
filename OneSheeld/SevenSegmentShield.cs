using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class SevenSegmentShield
    {
        OneSheeld Sheeld = null;

        public SevenSegmentShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
        }

        //Number Setter
        public void setNumber(byte x)
        {
	        byte[] data2 = { (byte)0x3F,(byte)0x06,(byte)0x5B,(byte)0x4F,(byte)0x66,
                             (byte)0x6D,(byte)0x7D,(byte)0x07,(byte)0x7F,(byte)0x6F };
	
	        switch (x)
	        {
		        case 0 : setValue(data2[0]);break;
		        case 1 : setValue(data2[1]);break;
		        case 2 : setValue(data2[2]);break;
		        case 3 : setValue(data2[3]);break;
		        case 4 : setValue(data2[4]);break;
		        case 5 : setValue(data2[5]);break;
		        case 6 : setValue(data2[6]);break;
		        case 7 : setValue(data2[7]);break;
		        case 8 : setValue(data2[8]);break;
		        case 9 : setValue(data2[9]);break;
	        }

        }

        //Setter
        public void setValue(byte shape)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = shape;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

	        Sheeld.sendPacket(ShieldIds.SEV_SEG_ID,0,SEVENSEGMENT_SET_VALUE,1,args);
        }

        //Dot Setter
        public void setDot()
        {
	        Sheeld.sendPacket(ShieldIds.SEV_SEG_ID,0,SEVENSEGMENT_SET_DOT,0,null);
        }

        const byte SEVENSEGMENT_SET_VALUE = 0x01;
        const byte SEVENSEGMENT_SET_DOT = 0x02;    
    }
}
