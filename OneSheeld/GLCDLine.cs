using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GLCDLine : ShapeClass
    {
        int x2position = 0;
        int y2position = 0;

        public GLCDLine(int x, int y, int x2, int y2)
            :base (GLCD_LINE_TYPE, x, y)
        {
            x2position = x2;
            y2position = y2;
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

            byte[] x2PositionArray = new byte[2];
            x2PositionArray[1] = (byte)((x2position >> 8) & 0xFF);
            x2PositionArray[0] = (byte)(x2position & 0xFF);
     
            FunctionArg arg5 = new FunctionArg(2, x2PositionArray);

            args.Add(arg5);

            byte[] y2PositionArray = new byte[2];
            y2PositionArray[1] = (byte)((y2position >> 8) & 0xFF);
            y2PositionArray[0] = (byte)(y2position & 0xFF);

            FunctionArg arg6 = new FunctionArg(2, y2PositionArray);

            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_LINE_TYPE, 6, args);

        }

        public void setCoordinates(int xNew, int yNew, int x2New, int y2New)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_LINE_COORDINATES;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] xPositionArray = new byte[2];
            xPositionArray[1] = (byte)((xNew >> 8) & 0xFF);
            xPositionArray[0] = (byte)(xNew & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, xPositionArray);

            args.Add(arg3);

            byte[] yPositionArray = new byte[2];
            yPositionArray[1] = (byte)((yNew >> 8) & 0xFF);
            yPositionArray[0] = (byte)(yNew & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, yPositionArray);

            args.Add(arg4);

            byte[] x2PositionArray = new byte[2];
            x2PositionArray[1] = (byte)((x2New >> 8) & 0xFF);
            x2PositionArray[0] = (byte)(x2New & 0xFF);

            FunctionArg arg5 = new FunctionArg(2, x2PositionArray);

            args.Add(arg5);

            byte[] y2PositionArray = new byte[2];
            y2PositionArray[1] = (byte)((y2New >> 8) & 0xFF);
            y2PositionArray[0] = (byte)(y2New & 0xFF);

            FunctionArg arg6 = new FunctionArg(2, y2PositionArray);

            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_LINE_TYPE, 6, args);
        }

        const byte GLCD_LINE_TYPE = 0x03;
        const byte GLCD_LINE_COORDINATES = 0x03;

        const byte SHAPE_DRAW = 0x00;
    }
}
