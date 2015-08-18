using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GLCDEllipse : ShapeClass
    {
        int radius1 = 0;
        int radius2 = 0;

        public GLCDEllipse(int x, int y, int r1, int r2)
            : base(GLCD_ELLIPSE_TYPE, x, y)
        {
            radius1 = r1;
            radius2 = r2;
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

            byte[] r1Array = new byte[2];
            r1Array[1] = (byte)((radius1 >> 8) & 0xFF);
            r1Array[0] = (byte)(radius1 & 0xFF);

            FunctionArg arg5 = new FunctionArg(2, r1Array);

            args.Add(arg5);

            byte[] r2Array = new byte[2];
            r2Array[1] = (byte)((radius2 >> 8) & 0xFF);
            r2Array[0] = (byte)(radius2 & 0xFF);

            FunctionArg arg6 = new FunctionArg(2, r2Array);

            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_ELLIPSE_TYPE, 6, args);

        }

        public void setRadius(int r1, int r2)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_ELLIPSE_RADIUS;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] r1Array = new byte[2];
            r1Array[1] = (byte)((radius1 >> 8) & 0xFF);
            r1Array[0] = (byte)(radius1 & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, r1Array);

            args.Add(arg3);

            byte[] r2Array = new byte[2];
            r2Array[1] = (byte)((radius2 >> 8) & 0xFF);
            r2Array[0] = (byte)(radius2 & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, r2Array);

            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_ELLIPSE_TYPE, 4, args);

        }

        public void setFill(bool f)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_ELLIPSE_FILL;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] fillArray = new byte[1];
            fillArray[0] = (f) ? (byte)0x01 : (byte)0x00;

            FunctionArg arg3 = new FunctionArg(1, fillArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_ELLIPSE_TYPE, 3, args);
        }

        const byte GLCD_ELLIPSE_TYPE = 0x04;
        const byte GLCD_ELLIPSE_RADIUS = 0x03;
        const byte GLCD_ELLIPSE_FILL = 0x04;

        const byte SHAPE_DRAW = 0x00;
    }
}
