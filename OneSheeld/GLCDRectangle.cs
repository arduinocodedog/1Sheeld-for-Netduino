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

            FunctionArg arg7 = new FunctionArg(radius);
            args.Add(arg7);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 7, args);

        }

        public void setRadius(int r)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_RECTANGLE_RADIUS);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(radius);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 3, args);
        }

        public void setFill(bool f)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_RECTANGLE_FILL);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(f);
           args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RECTANGLE_TYPE, 3, args);
        }

        public void setDimensions(int xdimension, int ydimension)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(GLCD_RECTANGLE_DIMENSIONS);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xdimension);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(ydimension);
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
