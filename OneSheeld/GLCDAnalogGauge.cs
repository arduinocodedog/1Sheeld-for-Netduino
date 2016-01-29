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

            FunctionArg arg1 = new FunctionArg(SHAPE_DRAW);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xposition);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(yposition);
            args.Add(arg4);

            FunctionArg arg5 = new FunctionArg(radius);
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

            FunctionArg arg1 = new FunctionArg(GLCD_ANALOG_GAUGE_RANGE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(start);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(end);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ANALOG_GAUGE_TYPE, 4, args);
        }

        public void setValue(int v)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_ANALOG_GAUGE_VALUE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(v);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ANALOG_GAUGE_TYPE, 3, args);
        }

        public void setRadius(int r)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_ANALOG_GAUGE_RADIUS);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(radius);
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
