using System.Collections;

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

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 5, args);
        }

        public void setText(string dataString)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_CHECK_BOX_SET_TEXT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(dataString.Length, System.Text.Encoding.UTF8.GetBytes(dataString));

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 3, args);
        }

        public void setSize(byte size)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_CHECK_BOX_SET_SIZE;

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

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 3, args);
        }

        public bool isSelected()
        {
            return checkboxValue;
        }

        public void select()
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_CHECK_BOX_SELECT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 2, args);
        }

        public void deselect()
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_CHECK_BOX_UNSELECT;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, GLCD_CHECK_BOX_TYPE, 2, args);
        }

        const byte GLCD_CHECK_BOX_TYPE = 0x0a;
        const byte GLCD_CHECK_BOX_SET_TEXT = 0x03;
        const byte GLCD_CHECK_BOX_SET_SIZE = 0x04;
        const byte GLCD_CHECK_BOX_SELECT = 0x05;
        const byte GLCD_CHECK_BOX_UNSELECT = 0x06;

        const byte SHAPE_DRAW = 0x00;
    }
}
