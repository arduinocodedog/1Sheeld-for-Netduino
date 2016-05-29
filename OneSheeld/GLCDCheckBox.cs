namespace OneSheeldClasses
{
    public class GLCDCheckBox : InteractiveShapeClass
    {
        public bool checkboxValue = false;
        string dataString = null;

        public GLCDCheckBox(int x, int y, string _dataString)
            : base(GLCD_CHECK_BOX_TYPE, x, y)
        {

        }

        ~GLCDCheckBox()
        {
            dataString = null;
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

            FunctionArg arg5 = new FunctionArg(dataString);
            args.Add(arg5);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 5, args);
        }

        public void setText(string dataString)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_CHECK_BOX_SET_TEXT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 3, args);
        }

        public void setSize(byte size)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_CHECK_BOX_SET_SIZE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(size);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 3, args);
        }

        public bool isSelected()
        {
            return checkboxValue;
        }

        public void select()
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_CHECK_BOX_SELECT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 2, args);
        }

        public void deselect()
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_CHECK_BOX_UNSELECT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 2, args);
        }

        const byte GLCD_CHECK_BOX_TYPE = 0x0a;
        const byte GLCD_CHECK_BOX_SET_TEXT = 0x03;
        const byte GLCD_CHECK_BOX_SET_SIZE = 0x04;
        const byte GLCD_CHECK_BOX_SELECT = 0x05;
        const byte GLCD_CHECK_BOX_UNSELECT = 0x06;

        const byte SHAPE_DRAW = 0x00;
    }
}
