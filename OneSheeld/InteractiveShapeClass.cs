using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class InteractiveShapeClass : ShapeClass
    {
        public IInteractiveShapeChangeCallback userCallback = null;
        public bool isCallbackAssigned = false;

        public InteractiveShapeClass(byte shapeType, int xPosition, int yPosition)
            : base(shapeType, xPosition, yPosition)
        {
            isInteractiveShape = true;
        }

        public void SetOnChange(IInteractiveShapeChangeCallback callback)
        {
            userCallback = callback;
            isCallbackAssigned = true;
        }
    }
}
