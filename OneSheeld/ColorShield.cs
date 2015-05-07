using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ColorShield : ShieldParent
    {
        bool isNewColor = false;
	    IColorCallback colorCallBack = null;
        bool colorCallBackInvoked = false;
        IColorsCallback colorsCallBack = null;
        bool colorsCallBackInvoked = false;
        bool fullOperation = false;
        ColorClass[] colorInstances = null; 

        public ColorShield() :
            base(ShieldIds.COLOR_ID)
        {
            colorInstances = new ColorClass[9];
        }

        public void setPalette(byte range)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = range;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.COLOR_ID,0,COLOR_PALETTE_ID,1,args);
        }

        public void enableFullOperation()
        {
	        isNewColor=false;
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.COLOR_ID,0,COLOR_FULL_OPERATION_ID,0);
        }

        public void enableNormalOperation()
        {
	        isNewColor=false;
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.COLOR_ID,0,COLOR_NORMAL_OPERATION_ID,0);
        }

        public void setCalculationMode(byte mode)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = mode;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.COLOR_ID, 0, COLOR_CALCULATION_MODE_ID, 1, args);
        }

        public void setPatchSize(byte mode)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = mode;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.COLOR_ID, 0, COLOR_SET_PATH_SIZE_ID, 1, args);
        }

        public ColorClass getLastColor(byte whichColor = COLOR_CENTER_MIDDLE)
        {
	        if(whichColor>=9||!fullOperation)
	        {
		        whichColor=COLOR_CENTER_MIDDLE;
		        isNewColor =false;
	        }
	        return colorInstances[whichColor];
        }

        public bool isNewColorReceived()
        {
	        return isNewColor;
        }

        public bool isFullOperation()
        {
	        return fullOperation;
        }

        public void finishedReading()
        {
	        isNewColor=false;
        }

        public void setOnNewColor(IColorCallback userCallback)
        {
	        colorCallBack = userCallback;
	        colorCallBackInvoked = true;
        }


        public void setOnNewColor(IColorsCallback userCallback)
        {
	        colorsCallBack = userCallback;
	        colorsCallBackInvoked = true;
        }


        public static ulong convertRgbToHsb(ulong rgb)
        {
            double rd = (double)((rgb&0xFF0000)>>16)/255;
            double gd = (double)((rgb&0x00FF00)>>8)/255;
            double bd = (double)((rgb&0x0000FF))/255;
            double maximum = System.Math.Max(rd, System.Math.Max(gd, bd)), minimum = System.Math.Min(rd, System.Math.Min(gd, bd));
            double h = 0, s = 0, b = maximum;

            double d = maximum - minimum;
            s = maximum == 0 ? 0 : d / maximum;

            if (maximum == minimum) { 
                h = 0; // achromatic
            } else {
                if (maximum == rd) {
                    h = (gd - bd) / d + (gd < bd ? 6 : 0);
                } else if (maximum == gd) {
                    h = (bd - rd) / d + 2;
                } else if (maximum == bd) {
                    h = (rd - gd) / d + 4;
                }
                h /= 6;
            }
            uint hue=(h=System.Math.Round((double)(h*360)))>360?(uint)360:(uint)h;
            byte saturation=((s=System.Math.Round((double)(s*100)))>100)?(byte)100:(byte)s;
            byte brightness=(b=System.Math.Round((double)(b*100)))>100?(byte)100:(byte)b;
            return ((ulong)hue<<16)|((ulong)saturation<<8)|((ulong)brightness);
        }

        public override void processData()
        {
	        byte functionId = getOneSheeldInstance().getFunctionId();

	        if(functionId == COLOR_VALUE)
	        {
		        isNewColor = true;
		        fullOperation=false;

		        colorInstances[COLOR_CENTER_MIDDLE] = new ColorClass((ulong)(((ulong)getOneSheeldInstance().getArgumentData(0)[0])|
											                    ((ulong)getOneSheeldInstance().getArgumentData(0)[1])<<8|
											                    ((ulong)getOneSheeldInstance().getArgumentData(0)[2])<<16));
		
		        if(colorCallBackInvoked && !isInACallback())
		        {
			        enteringACallback();
                    colorCallBack.OnColorReceived(colorInstances[COLOR_CENTER_MIDDLE]);
			        exitingACallback();
		        }
	        }
	        else if(functionId == ALL_COLORS_VALUE && getOneSheeldInstance().getArgumentNo()==9)
	        {
		        isNewColor = true;
		        fullOperation = true;

		        for(int i=0;i < 9;i++)
		        {
			        colorInstances[i] = new ColorClass((ulong)(((ulong)getOneSheeldInstance().getArgumentData((byte)i)[0])|
											              ((ulong)getOneSheeldInstance().getArgumentData((byte)i)[1])<<8|
											              ((ulong)getOneSheeldInstance().getArgumentData((byte)i)[2])<<16));
		        }

		
		        if(colorsCallBackInvoked && !isInACallback())
		        {	
			        enteringACallback();
			        colorsCallBack.OnColorsReceived(colorInstances[COLOR_UPPER_LEFT],colorInstances[COLOR_UPPER_MIDDLE],colorInstances[COLOR_UPPER_RIGHT],
				        colorInstances[COLOR_CENTER_LEFT],colorInstances[COLOR_CENTER_MIDDLE],colorInstances[COLOR_CENTER_RIGHT],
				        colorInstances[COLOR_LOWER_LEFT],colorInstances[COLOR_LOWER_MIDDLE],colorInstances[COLOR_LOWER_RIGHT]);
			        exitingACallback();
		        }
	        }
        }

        //Output Function ID
        const byte COLOR_PALETTE_ID	= 0x01;
        const byte COLOR_FULL_OPERATION_ID = 0x02;
        const byte COLOR_NORMAL_OPERATION_ID = 0x03;
        const byte COLOR_CALCULATION_MODE_ID = 0x04;
        const byte COLOR_SET_PATH_SIZE_ID = 0x05;

        //Input Function ID
        const byte COLOR_VALUE = 0x01;
        const byte ALL_COLORS_VALUE = 0x02;

        //Literals
        public const int COLOR_UPPER_LEFT = 0;
        public const int COLOR_UPPER_MIDDLE = 1;
        public const int COLOR_UPPER_RIGHT = 2;
        public const int COLOR_CENTER_LEFT = 3;
        public const int COLOR_CENTER_MIDDLE = 4;
        public const int COLOR_CENTER_RIGHT = 5;
        public const int COLOR_LOWER_LEFT = 6;
        public const int COLOR_LOWER_MIDDLE = 7;
        public const int COLOR_LOWER_RIGHT = 8;

        public const byte MOST_DOMINANT_COLOR = 0x01;
        public const byte AVERAGE_COLOR = 0x02;

        public const byte SMALL_SIZE = 0x01;
        public const byte MEDIUM_SIZE = 0x02;
        public const byte LARGE_SIZE = 0x03;

        public const int _1_BIT_GRAYSCALE_PALETTE = 1;
        public const int _2_BIT_GRAYSCALE_PALETTE = 2;
        public const int _4_BIT_GRAYSCALE_PALETTE = 3;
        public const int _8_BIT_GRAYSCALE_PALETTE = 4;
        public const int _3_BIT_RGB_PALETTE = 5;
        public const int _6_BIT_RGB_PALETTE = 6;
        public const int _9_BIT_RGB_PALETTE = 7;
        public const int _12_BIT_RGB_PALETTE = 8;
        public const int _15_BIT_RGB_PALETTE = 9;
        public const int _18_BIT_RGB_PALETTE = 10;
        public const int _24_BIT_RGB_PALETTE = 11;
    }
}
