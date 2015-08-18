using System;
using System.Collections;
using Microsoft.SPOT;

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

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 5, args);
        }

        public void setFont(byte fonttype)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_TEXTBOX_SET_FONT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] fontArray = new byte[1];
            fontArray[0] = fonttype;

            FunctionArg arg3 = new FunctionArg(1, fontArray);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 3, args);
        }

        public void setSize(byte size)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_TEXTBOX_SET_SIZE;

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

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 3, args);
        }

        public void setText(string _dataString)
        {
            dataString = _dataString;

            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_TEXTBOX_TEXT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString.Length, System.Text.Encoding.UTF8.GetBytes(dataString));

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_TEXTBOX_TYPE, 3, args);

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
