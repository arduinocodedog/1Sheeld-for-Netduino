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
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(SHAPE_DRAW);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xposition);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(yposition);
            args.Add(arg4);

            FunctionArg arg5 = new FunctionArg(x2position);
            args.Add(arg5);

            FunctionArg arg6 = new FunctionArg(y2position);
            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_LINE_TYPE, 6, args);

        }

        public void setCoordinates(int xNew, int yNew, int x2New, int y2New)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_LINE_COORDINATES);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xNew);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(yNew);
            args.Add(arg4);

            FunctionArg arg5 = new FunctionArg(x2New);
            args.Add(arg5);

            FunctionArg arg6 = new FunctionArg(y2New);
            args.Add(arg6);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_LINE_TYPE, 6, args);
        }

        const byte GLCD_LINE_TYPE = 0x03;
        const byte GLCD_LINE_COORDINATES = 0x03;

        const byte SHAPE_DRAW = 0x00;
    }
}
