using System.Collections;

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
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = SHAPE_DRAW;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] xPositionArray = new byte[2];
            xPositionArray[1] = (byte)((xposition >> 8) & 0xFF);
            xPositionArray[0] = (byte)(xposition & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, xPositionArray);

            args.Add(arg3);

            byte[] yPositionArray = new byte[2];
            yPositionArray[1] = (byte)((yposition >> 8) & 0xFF);
            yPositionArray[0] = (byte)(yposition & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, yPositionArray);

            args.Add(arg4);

            FunctionArg arg5 = new FunctionArg(dataString.Length, System.Text.Encoding.UTF8.GetBytes(dataString));

            args.Add(arg5);

            if (sendAsGroup)
            {
                byte[] groupNumberArray = new byte[2];
                groupNumberArray[1] = (byte)((groupNumber >> 8) & 0xFF);
                groupNumberArray[0] = (byte)(groupNumber & 0xFF);

                FunctionArg arg6 = new FunctionArg(2, groupNumberArray);

                args.Add(arg6);

                sendAsGroup = false;
            }

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, args.Count, args);
        }

        public void setText(string dataString)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RADIO_BUTTON_SET_TEXT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString.Length, System.Text.Encoding.UTF8.GetBytes(dataString));

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 3, args);

        }

        public void setSize(byte size)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RADIO_BUTTON_SET_SIZE;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] sizeArray = new byte[1];
            sizeArray[0] = size;

            FunctionArg arg3 = new FunctionArg(1, sizeArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 3, args);
        }

        public void setGroup(int number)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RADIO_BUTTON_SET_GROUP;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] numberArray = new byte[2];
            numberArray[1] = (byte)((number >> 8) & 0xFF);
            numberArray[0] = (byte)(number & 0xFF);

            FunctionArg arg3 = new FunctionArg(1, numberArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 3, args);
        }

        public void select()
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_RADIO_BUTTON_SELECT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_RADIO_BUTTON_TYPE, 2, args);
        }

        const byte GLCD_RADIO_BUTTON_TYPE = 0x09;
        const byte GLCD_RADIO_BUTTON_SET_TEXT = 0x03;
        const byte GLCD_RADIO_BUTTON_SET_SIZE = 0x04;
        const byte GLCD_RADIO_BUTTON_SET_GROUP = 0x05;
        const byte GLCD_RADIO_BUTTON_SELECT = 0x06;

        const byte SHAPE_DRAW = 0x00;
    }
}
