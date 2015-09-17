using System.Collections;

namespace OneSheeldClasses
{
    public class GLCDShield : ShieldParent
    {
        ShapeClass[] interactiveShapesArray = null;

        public GLCDShield() 
            : base(ShieldIds.GLCD_ID)
        {
            interactiveShapesArray = new ShapeClass[MAX_NO_OF_SHAPE_USED];
            for (int i = 0; i < MAX_NO_OF_SHAPE_USED; i++)
                interactiveShapesArray[i] = null;
        }

        public void clear()
        {
            ArrayList args = new ArrayList();

            byte[] functionid = new byte[1];
            functionid[0] = GLCD_CLEAR;

            FunctionArg arg = new FunctionArg(1, functionid);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.GLCD_ID, 0, GLCD_TYPE, 1, args);
        }

        public void draw(ShapeClass usersShape)
        {
            byte currentShapeType = usersShape.shapeType;

            if (usersShape.isInteractiveShape)
            {
                bool shapeIsAdded = addToShapeArray(usersShape);
                if (shapeIsAdded)
                    usersShape.draw();
            }
            else
                usersShape.draw();
        }

        protected bool addToShapeArray(ShapeClass shape)
        {
            int i = 0;
            for (i = 0; i < MAX_NO_OF_SHAPE_USED; i++)
            {
                if (interactiveShapesArray[i] == null)
                    break;
            }

            if (i >= MAX_NO_OF_SHAPE_USED)
                return false;

            interactiveShapesArray[i] = shape;
            return true;
        }

        public override void processData()
        {
            byte functionid = getOneSheeldInstance().getArgumentData(0)[0];

            if (functionid == GLCD_GET_DATA_FROM_SHAPE)
            {
                int shapeId = (getOneSheeldInstance().getArgumentData(1)[0] | (getOneSheeldInstance().getArgumentData(1)[1] << 8));

                byte incomingShapeType = getOneSheeldInstance().getFunctionId();

                for (int i = 0; i < MAX_NO_OF_SHAPE_USED; i++)
                {
                    if (interactiveShapesArray[i] != null && interactiveShapesArray[i].shapeID == shapeId && interactiveShapesArray[i].shapeType == incomingShapeType)
                    {
                        switch (incomingShapeType)
                        {
                            case GLCD_BUTTON_TYPE:
                                bool incomingButtonValue = (getOneSheeldInstance().getArgumentData(2)[0] != 0);
                                GLCDButton buttonPointer = ((GLCDButton)(interactiveShapesArray[i]));
                                buttonPointer.buttonValue = incomingButtonValue;
                                if (buttonPointer.isCallbackAssigned && !isInACallback())
                                {
                                    enteringACallback();
                                    buttonPointer.userCallback.OnChange(incomingButtonValue);
                                    exitingACallback();
                                }
                                break;

                            case GLCD_RADIO_BUTTON_TYPE:
                                bool incomingRadioButtonValue = (getOneSheeldInstance().getArgumentData(2)[0] != 0);
                                GLCDRadioButton radioButtonPointer = ((GLCDRadioButton)(interactiveShapesArray[i]));
                                radioButtonPointer.radiobuttonValue = incomingRadioButtonValue;
                                if (radioButtonPointer.isCallbackAssigned && !isInACallback())
                                {
                                    enteringACallback();
                                    radioButtonPointer.userCallback.OnChange(incomingRadioButtonValue);
                                    exitingACallback();
                                }
                                break;

                            case GLCD_CHECK_BOX_TYPE:
                                bool incomingCheckBoxValue = (getOneSheeldInstance().getArgumentData(2)[0] != 0);
                                GLCDCheckBox checkBoxPointer = ((GLCDCheckBox)(interactiveShapesArray[i]));
                                checkBoxPointer.checkboxValue = incomingCheckBoxValue;
                                if (checkBoxPointer.isCallbackAssigned && !isInACallback())
                                {
                                    enteringACallback();
                                    checkBoxPointer.userCallback.OnChange(incomingCheckBoxValue);
                                    exitingACallback();
                                }
                                break;

                            case GLCD_SLIDER_TYPE:
                                int incomingSliderValue = (int)(getOneSheeldInstance().getArgumentData(2)[0] | (getOneSheeldInstance().getArgumentData(2)[1] << 8));
                                GLCDSlider sliderPointer = ((GLCDSlider)(interactiveShapesArray[i]));
                                sliderPointer.sliderValue = incomingSliderValue;
                                if (sliderPointer.isCallbackAssigned && !isInACallback())
                                {
                                    enteringACallback();
                                    sliderPointer.userCallback.OnChange(incomingSliderValue);
                                    exitingACallback();
                                }
                                break;
                        }
                        break;
                    }
                }
            }

        }

        const byte GLCD_TYPE = 0x00;
        const byte GLCD_CLEAR = 0x00;

        const byte GLCD_BUTTON_TYPE = 0x08;
        const byte GLCD_RADIO_BUTTON_TYPE = 0x09;
        const byte GLCD_CHECK_BOX_TYPE = 0x0a;
        const byte GLCD_SLIDER_TYPE = 0x0b;
        
        const byte GLCD_GET_DATA_FROM_SHAPE = 0x01;

        const int MAX_NO_OF_SHAPE_USED = 32;
    }
}
