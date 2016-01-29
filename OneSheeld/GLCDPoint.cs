using System.Collections;

namespace OneSheeldClasses
{
    public class GLCDPoint : ShapeClass
    {
        public GLCDPoint(int x, int y) :
            base(GLCD_POINT_TYPE, x, y)
        {

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

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_POINT_TYPE, 4, args);
        }

        const byte GLCD_POINT_TYPE = 0x01;
        const byte SHAPE_DRAW = 0x00;
    }
}
