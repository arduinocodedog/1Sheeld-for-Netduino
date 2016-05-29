namespace OneSheeldClasses 
{
    class GLCDTextBox : ShapeClass
    {
        string dataString = null;

        GLCDTextBox(int x, int y, string _dataString)
            : base(GLCD_TEXTBOX_TYPE, x, y)
        {
            dataString = _dataString;
        }

        ~GLCDTextBox()
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

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 5, args);
        }

        public void setFont(byte fonttype)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_TEXTBOX_SET_FONT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(fonttype);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 3, args);
        }

        public void setSize(byte size)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_TEXTBOX_SET_SIZE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(size);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 3, args);
        }

        public void setText(string _dataString)
        {
            dataString = _dataString;

            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_TEXTBOX_TEXT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 3, args);

        }

        const byte GLCD_TEXTBOX_TYPE = 0x05;
        const byte GLCD_TEXTBOX_SET_FONT = 0x03;
        const byte GLCD_TEXTBOX_SET_SIZE = 0x04;
        const byte GLCD_TEXTBOX_TEXT = 0x05;

        /* Fonts Literals. */
        public const byte ARIAL = 0x00;
        public const byte ARIAL_BOLD = 0x01;
        public const byte ARIAL_ITALIC = 0x02;
        public const byte COMIC_SANS = 0x03;
        public const byte SERIF = 0x04;

        /* Size Literals. */
        public const byte SMALL = 0x00;
        public const byte MEDIUM = 0x01;
        public const byte LARGE = 0x02;

        const byte SHAPE_DRAW = 0x00;
    }
}
