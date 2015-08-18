using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ShapeClass
    {
        public int shapeID = 0;
        public int xposition = 0;
        public int yposition = 0;
        public byte shapeType = 0x00;
        static int currentShapeId = 0;

        public ShapeClass(byte _Type, int _xPos, int _yPos)
        {
            shapeType = _Type;
            xposition = _xPos;
            yposition = _yPos;
            shapeID = currentShapeId++;
        }

        private bool _isInteractiveShape = false;
        public bool isInteractiveShape
        {
            get { return _isInteractiveShape; }
            set { _isInteractiveShape = value;  }
        }

        public virtual void setVisibility(byte _vType)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = SHAPE_VISIBILITY;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, shapeIdArray);

            args.Add(arg2);

            byte[] visibilityType = new byte[1];
            visibilityType[0] = _vType;

            FunctionArg arg3 = new FunctionArg(1, visibilityType);

            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, shapeType, 3, args);
        }

        public virtual void setPosition(int _xPos, int _yPos)
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = SHAPE_POSITION;

            FunctionArg arg1 = new FunctionArg(1, functionid);

            args.Add(arg1);

            byte[] xPositionArray = new byte[2];
            xPositionArray[1] = (byte)((_xPos >> 8) & 0xFF);
            xPositionArray[0] = (byte)(_xPos & 0xFF);

            FunctionArg arg2 = new FunctionArg(2, xPositionArray);

            args.Add(arg2);

            byte[] yPositionArray = new byte[2];
            yPositionArray[1] = (byte)((_yPos >> 8) & 0xFF);
            yPositionArray[0] = (byte)(_yPos & 0xFF);

            FunctionArg arg3 = new FunctionArg(2, yPositionArray);

            args.Add(arg3);

            byte[] shapeIdArray = new byte[2];
            shapeIdArray[1] = (byte)((shapeID >> 8) & 0xFF);
            shapeIdArray[0] = (byte)(shapeID & 0xFF);

            FunctionArg arg4 = new FunctionArg(2, shapeIdArray);

            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.GLCD_ID, 0, shapeType, 4, args);
        }

        public virtual void draw() { }

        const byte SHAPE_DRAW = 0x00;
        const byte SHAPE_POSITION = 0x01;
        const byte SHAPE_VISIBILITY = 0x02;
    }

}
