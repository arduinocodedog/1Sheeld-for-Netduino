using System.Collections;

namespace OneSheeldClasses
{
    class GLCDAnalogGauge : ShapeClass
    {
        int radius = 0;

        public GLCDAnalogGauge(int x, int y, int r)
            : base(GLCD_ANALOG_GAUGE_TYPE, x, y)
        {
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

            byte[] radiusArray = new byte[2];
            radiusArray[1] = (byte)((radius >> 8) & 0xFF);
            radiusArray[0] = (byte)(radius & 0xFF);

            FunctionArg arg5 = new FunctionArg(2, radiusArray);

            args.Add(arg5);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ANALOG_GAUGE_TYPE, 5, args);

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
            functionid[0] = GLCD_ANALOG_GAUGE_RANGE;

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


            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ANALOG_GAUGE_TYPE, 4, args);
        }

        public void setValue(int v)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_ANALOG_GAUGE_VALUE;

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

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ANALOG_GAUGE_TYPE, 3, args);

        }

        public void setRadius(int r)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_ANALOG_GAUGE_RADIUS;

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

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ANALOG_GAUGE_TYPE, 3, args);

        }

        const byte GLCD_ANALOG_GAUGE_TYPE = 0x07;
        const byte GLCD_ANALOG_GAUGE_RANGE = 0x03;
        const byte GLCD_ANALOG_GAUGE_VALUE = 0x04;
        const byte GLCD_ANALOG_GAUGE_RADIUS = 0x05;

        const byte SHAPE_DRAW = 0x00;
    }
}
