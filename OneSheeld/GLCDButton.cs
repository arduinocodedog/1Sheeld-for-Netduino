namespace OneSheeldClasses
{
    public class GLCDButton : InteractiveShapeClass
    {
        public bool buttonValue = false;
        public bool buttonHasName = false;

        int width = 0;
        int height = 0;
        string dataString = null;

        public GLCDButton(int x, int y, int w, int h, string _dataString = null)
            : base(GLCD_BUTTON_TYPE, x, y)
        {
            buttonHasName = (_dataString != null) ? true : false;
            width = w;
            height = h;
            dataString = _dataString;
        }

        ~GLCDButton()
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

            FunctionArg arg5 = new FunctionArg(width);
            args.Add(arg5);

            FunctionArg arg6 = new FunctionArg(height);
            args.Add(arg6);

            if (buttonHasName)
            {
                FunctionArg arg7 = new FunctionArg(dataString);
                args.Add(arg7);
            }

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, args.Count, args);
        }

        public bool isPressed()
        {
            return buttonValue;
        }

        public void setText(string dataString)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_BUTTON_TEXT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 3, args);
        }

        public void setStyle(byte style)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_BUTTON_STYLE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(style);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 3, args);
        }

        public void setDimensions(int xdimension, int ydimension)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_BUTTON_DIMENSIONS);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(xdimension);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(ydimension);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 4, args);
        }

        const byte GLCD_BUTTON_TYPE = 0x08;
        const byte GLCD_BUTTON_TEXT = 0x03;
        const byte GLCD_BUTTON_DIMENSIONS = 0x04;
        const byte GLCD_BUTTON_STYLE = 0x05;

        const byte GLCD_BUTTON_VALUE = 0x01;

        public const byte STYLE_2D = 0x00;
        public const byte STYLE_3D = 0x01;

        const byte SHAPE_DRAW = 0x00;
    }
}
