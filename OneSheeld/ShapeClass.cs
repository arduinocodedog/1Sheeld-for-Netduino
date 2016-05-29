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
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(SHAPE_VISIBILITY);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(shapeID);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(_vType);
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, shapeType, 3, args);
        }

        public virtual void setPosition(int _xPos, int _yPos)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(SHAPE_POSITION);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(_xPos);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(_yPos);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(shapeID);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, shapeType, 4, args);
        }

        public virtual void draw() { }

        const byte SHAPE_DRAW = 0x00;
        const byte SHAPE_POSITION = 0x01;
        const byte SHAPE_VISIBILITY = 0x02;
    }

}
