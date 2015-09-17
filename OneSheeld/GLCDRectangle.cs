using System.Collections;

namespace OneSheeldClasses
{
    public class GLCDRectangle : ShapeClass
    {
        int width = 0;
        int height  = 0;
        int radius = 0;

        public GLCDRectangle(int x, int y, int w, int h, int r = 0)
            : base(GLCD_RECTANGLE_TYPE, x, y)
        {
            width = w;
            height = h;
            radius = r;
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

            byte[] radiusArray = new byte[2];
            radiusArray[1] = (byte)((radius >> 8) & 0xFF);
            radiusArray[0] = (byte)(radius & 0xFF);

            FunctionArg arg7 = new FunctionArg(2, radiusArray);

            args.Add(arg7);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 7, args);

        }

        public void setRadius(int r)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RECTANGLE_RADIUS;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] radiusArray = new byte[2];
            radiusArray[1] = (byte)((radius >> 8) & 0xFF);
            radiusArray[0] = (byte)(radius & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, radiusArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 3, args);
        }

        public void setFill(bool f)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RECTANGLE_FILL;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] fillArray = new byte[1];
            fillArray[0] = (f) ? (byte) 0x01 : (byte) 0x00;

            FunctionArg arg3 = new FunctionArg(1, fillArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 3, args);
        }

        public void setDimensions(int xdimension, int ydimension)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RECTANGLE_DIMENSIONS;

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

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 4, args);
        }

        const byte GLCD_RECTANGLE_TYPE = 0x02;
        const byte GLCD_RECTANGLE_RADIUS = 0x03;
        const byte GLCD_RECTANGLE_FILL = 0x04;
        const byte GLCD_RECTANGLE_DIMENSIONS = 0x05;

        const byte SHAPE_DRAW = 0x00;
    }
}
