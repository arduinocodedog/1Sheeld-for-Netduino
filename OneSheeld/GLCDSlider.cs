using System.Collections;

namespace OneSheeldClasses
{
    public class GLCDSlider : InteractiveShapeClass
    {
        public int sliderValue = 0;
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

            FunctionArg arg1 = new FunctionArg(SHAPE_DRAW);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xposition);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(yposition);
            args.Add(arg4);

            FunctionArg arg5 = new FunctionArg(width);
            args.Add(arg5);

            FunctionArg arg6 = new FunctionArg(height);
            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 6, args);

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

            FunctionArg arg1 = new FunctionArg(GLCD_SLIDER_RANGE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(start);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(end);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 4, args);
        }

        public void setValue(int v)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_SLIDER_VALUE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(v);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 3, args);
        }

        public void setDimensions(int xdimension, int ydimension)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_SLIDER_DIMENSIONS);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xdimension);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(ydimension);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_SLIDER_TYPE, 4, args);
        }

        public int getValue()
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
