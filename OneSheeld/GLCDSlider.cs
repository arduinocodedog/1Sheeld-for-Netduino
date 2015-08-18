using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GLCDSlider : InteractiveShapeClass
    {
        public byte sliderValue = 0x00;
        int width = 0;
        int height = 0;

        public GLCDSlider(int x, int y, int w, int h)
            : base(GLCD_SLIDER_TYPE, x, y)
        {
            width = w;
            height = h;
        }

        public override void draw()
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = SHAPE_DRAW;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] xPositionArray = new byte[2];
            xPositionArray[1] = (byte)((xposition >> 8) & 0xFF);
            xPositionArray[0] = (byte)(xposition & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, xPositionArray);

            args.Add(arg3);

            byte[] yPositionArray = new byte[2];
            yPositionArray[1] = (byte)((yposition >> 8) & 0xFF);
            yPositionArray[0] = (byte)(yposition & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, yPositionArray);

            args.Add(arg4);

            byte[] widthArray = new byte[2];
            widthArray[1] = (byte)((width >> 8) & 0xFF);
            widthArray[0] = (byte)(width & 0xFF);

            FunctionArg arg5 = new FunctionArg(2, widthArray);

            args.Add(arg5);

            byte[] heightArray = new byte[2];
            heightArray[1] = (byte)((height >> 8) & 0xFF);
            heightArray[0] = (byte)(height & 0xFF);

            FunctionArg arg6 = new FunctionArg(2, heightArray);

            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 6, args);

        }

        public void setRange(int start, int end)
        {
            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            else if ((start == end) || (start < 0) || (end < 0))
            {
                start = 0;
                end = 100;
            }

            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_SLIDER_RANGE;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] startArray = new byte[2];
            startArray[1] = (byte)((start >> 8) & 0xFF);
            startArray[0] = (byte)(start & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, startArray);

            args.Add(arg3);

            byte[] endArray = new byte[2];
            endArray[1] = (byte)((end >> 8) & 0xFF);
            endArray[0] = (byte)(end & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, endArray);


            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 4, args);
        }

        public void setValue(int v)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_SLIDER_VALUE;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] valueArray = new byte[2];
            valueArray[1] = (byte)((v >> 8) & 0xFF);
            valueArray[0] = (byte)(v & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, valueArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 3, args);
        }

        public void setDimensions(int xdimension, int ydimension)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_SLIDER_DIMENSIONS;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] xDimensionArray = new byte[2];
            xDimensionArray[1] = (byte)((xdimension >> 8) & 0xFF);
            xDimensionArray[0] = (byte)(xdimension & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, xDimensionArray);

            args.Add(arg3);

            byte[] yDimensionArray = new byte[2];
            yDimensionArray[1] = (byte)((ydimension >> 8) & 0xFF);
            yDimensionArray[0] = (byte)(ydimension & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, yDimensionArray);

            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 4, args);
        }

        public byte getValue()
        {
            return sliderValue;
        }

        public const byte GLCD_SLIDER_TYPE = 0x0b;
        const byte GLCD_SLIDER_RANGE = 0x03;
        const byte GLCD_SLIDER_VALUE = 0x04;
        const byte GLCD_SLIDER_DIMENSIONS = 0x05;

        const byte SHAPE_DRAW = 0x00;
    }
}
