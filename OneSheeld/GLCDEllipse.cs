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
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(SHAPE_DRAW);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xposition);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(yposition);
            args.Add(arg4);

            FunctionArg arg5 = new FunctionArg(radius1);
            args.Add(arg5);

            FunctionArg arg6 = new FunctionArg(radius2);
            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ELLIPSE_TYPE, 6, args);

        }

        public void setRadius(int r1, int r2)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_ELLIPSE_RADIUS);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(radius1);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(radius2);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ELLIPSE_TYPE, 4, args);

        }

        public void setFill(bool f)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_ELLIPSE_FILL);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(f);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_ELLIPSE_TYPE, 3, args);
        }

        const byte GLCD_ELLIPSE_TYPE = 0x04;
        const byte GLCD_ELLIPSE_RADIUS = 0x03;
        const byte GLCD_ELLIPSE_FILL = 0x04;

        const byte SHAPE_DRAW = 0x00;
    }
}
