using System.Collections;

namespace OneSheeldClasses
{
    public class GLCDButton : InteractiveShapeClass
    {
        public bool buttonValue = false;

        int width = 0;
        int height = 0;
        string dataString = null;

        public GLCDButton(int x, int y, int w, int h, string _dataString)
            : base(GLCD_BUTTON_TYPE, x, y)
        {
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

            byte[] widthArray = new byte[2];
            widthArray[1] = (byte)((width >> 8) & 0xFF);
            widthArray[0] = (byte)(width & 0xFF);

            FunctionArg arg5 = new FunctionArg(2, widthArray);

            args.Add(arg5);

            byte[] heightArray = new byte[2];
            heightArray[1] = (byte)((height >> 8) & 0xFF);
            heightArray[0] = (byte)(height & 0xFF);

            FunctionArg arg6 = new FunctionArg(2, heightArray);

            args.Add(arg6);

            FunctionArg arg7 = new FunctionArg(dataString.Length, System.Text.Encoding.UTF8.GetBytes(dataString));

            args.Add(arg7);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 7, args);
        }

        public bool isPressed()
        {
            return buttonValue;
        }

        public void setText(string _dataString)
        {
            dataString = _dataString;

            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_BUTTON_TEXT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString.Length, System.Text.Encoding.UTF8.GetBytes(dataString));

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 3, args);
        }

        public void setStyle(byte style)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_BUTTON_STYLE;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] styleArray = new byte[1];
            styleArray[0] = style;

            FunctionArg arg3 = new FunctionArg(1, styleArray);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 3, args);
        }

        public void setDimensions(int xdimension, int ydimension)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_BUTTON_DIMENSIONS;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] xDimensionArray = new byte[2];
            xDimensionArray[1] = (byte)((xdimension >> 8) & 0xFF);
            xDimensionArray[0] = (byte)(xdimension & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, xDimensionArray);

            args.Add(arg3);

            byte[] yDimensionArray = new byte[2];
            yDimensionArray[1] = (byte)((ydimension >> 8) & 0xFF);
            yDimensionArray[0] = (byte)(ydimension & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, yDimensionArray);

            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_BUTTON_TYPE, 4, args);
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
