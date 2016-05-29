namespace OneSheeldClasses
{
    public class GLCDRadioButton : InteractiveShapeClass
    {
        public bool radiobuttonValue = false;

        bool sendAsGroup = false;
        string dataString = null;

        public GLCDRadioButton(int x, int y, string _dataString, int _gn = -1)
            : base(GLCD_RADIO_BUTTON_TYPE, x, y)
        {
            if (_groupNumber != -1)
            {
                sendAsGroup = true;
                groupNumber = _gn;
            }
        }

        ~GLCDRadioButton()
        {
            dataString = null;
        }

        private int _groupNumber = -1;
        public int groupNumber
        {
            get { return _groupNumber; }
            set { _groupNumber = value; }
        }

        public bool isSelected()
        {
            return radiobuttonValue;
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

            if (sendAsGroup)
            {
                FunctionArg arg6 = new FunctionArg(groupNumber);
                args.Add(arg6);

                sendAsGroup = false;
            }

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, args.Count, args);
        }

        public void setText(string dataString)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_RADIO_BUTTON_SET_TEXT);

            args.Add(arg1);
            FunctionArg arg2 = new FunctionArg(shapeID);

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 3, args);

        }

        public void setSize(byte size)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_RADIO_BUTTON_SET_SIZE);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(size);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 3, args);
        }

        public void setGroup(int number)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_RADIO_BUTTON_SET_GROUP);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(number);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 3, args);
        }

        public void select()
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(GLCD_RADIO_BUTTON_SELECT);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 2, args);
        }

        const byte GLCD_RADIO_BUTTON_TYPE = 0x09;
        const byte GLCD_RADIO_BUTTON_SET_TEXT = 0x03;
        const byte GLCD_RADIO_BUTTON_SET_SIZE = 0x04;
        const byte GLCD_RADIO_BUTTON_SET_GROUP = 0x05;
        const byte GLCD_RADIO_BUTTON_SELECT = 0x06;

        const byte SHAPE_DRAW = 0x00;
    }
}
