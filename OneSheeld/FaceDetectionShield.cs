namespace OneSheeldClasses
{
    public class FaceDetectionShield : ShieldParent
    {
        private bool isCallBackAssigned = false;
        private bool isdeletedAssigned = false;
        private byte counter = 0;
        private Face[] facesArray = null;
        INewFaceCallback onNewFaceCallback = null;
        IFaceNotVisibleCallback onDeletedFaceCallback = null;

        public FaceDetectionShield()
            :base(ShieldIds.FACE_DETECTOR_ID)
        {
            facesArray = new Face[MAX_FACES];
        }

        public Face getVisibleFace(byte number)
        {
            if (number >= 0 && number < MAX_FACES && facesArray[number].faceID >= 0)
            {
                return facesArray[number];
            }
            else
            {
                Face nullFace = new Face();
                return nullFace;
            }
        }

        public Face getFace(int _faceID)
        {
            for (int i = 0; i < counter; i++)
            {
                if (facesArray[i].faceID == _faceID && _faceID != -1)
                {
                    return facesArray[i];
                }
            }

            Face nullFace = new Face();
            return nullFace;
        }

        public byte getVisibleFacesCount()
        {
            return counter;
        }

        public bool isFaceVisible(int _faceID)
        {
            for (int i = 0; i < counter; i++)
            {
                if (facesArray[i].faceID == _faceID && _faceID != -1)
                {
                    return true;
                }
            }
            return false;
        }

        public void setOnNewFace(INewFaceCallback userCallback)
        {
            onNewFaceCallback = userCallback;
            isCallBackAssigned = true;
        }
                
        public void setOnNotVisible(IFaceNotVisibleCallback userCallback)
        {
            onDeletedFaceCallback = userCallback;
            isdeletedAssigned = true;
        }

        public override void processData()
        {
            byte functionId = getOneSheeldInstance().getFunctionId();

            if (functionId == FACE_GET)
            {
                int currentFaceID = getOneSheeldInstance().getArgumentData(0)[0] | ((getOneSheeldInstance().getArgumentData(0)[1]) << 8);
                int currentX = getOneSheeldInstance().getArgumentData(1)[0] | ((getOneSheeldInstance().getArgumentData(1)[1]) << 8);
                int currentY = getOneSheeldInstance().getArgumentData(1)[2] | ((getOneSheeldInstance().getArgumentData(1)[3]) << 8);
                uint currentHeight = (uint)(getOneSheeldInstance().getArgumentData(2)[0] | ((getOneSheeldInstance().getArgumentData(2)[1]) << 8));
                uint currentWidth = (uint)(getOneSheeldInstance().getArgumentData(2)[2] | ((getOneSheeldInstance().getArgumentData(2)[3]) << 8));
                int i;
                byte currentRightEye = getOneSheeldInstance().getArgumentData(3)[0];
                byte currentLeftEye = getOneSheeldInstance().getArgumentData(3)[1];
                byte currentSmile = getOneSheeldInstance().getArgumentData(3)[2];

                for (i = 0; i < MAX_FACES; i++)
                {
                    if (facesArray[i].faceID == currentFaceID)
                    {
                        facesArray[i].leftEyeOpened = currentLeftEye;
                        facesArray[i].rightEyeOpened = currentRightEye;
                        facesArray[i].smiling = currentSmile;
                        facesArray[i].xCoordinate = currentX;
                        facesArray[i].yCoordinate = currentY;
                        facesArray[i].faceWidth = currentWidth;
                        facesArray[i].faceHeight = currentHeight;
                        break;
                    }
                }

                if (i == MAX_FACES)
                {
                    i = 0;
                    while (facesArray[i].faceID >= 0 && i < MAX_FACES)
                    {
                        i++;
                    }
                    facesArray[i].faceID = currentFaceID;
                    facesArray[i].leftEyeOpened = currentLeftEye;
                    facesArray[i].rightEyeOpened = currentRightEye;
                    facesArray[i].smiling = currentSmile;
                    facesArray[i].xCoordinate = currentX;
                    facesArray[i].yCoordinate = currentY;
                    facesArray[i].faceWidth = currentWidth;
                    facesArray[i].faceHeight = currentHeight;
                    counter++;
                }

                if (!isInACallback())
                {
                    if (isCallBackAssigned)
                    {
                        enteringACallback();
                        onNewFaceCallback.OnNewFace(facesArray[i]);
                        exitingACallback();
                    }
                }
            }
            else if (functionId == FACE_OUT_OF_BOUNDS)
            {
                int currentFaceID = getOneSheeldInstance().getArgumentData(0)[0] | ((getOneSheeldInstance().getArgumentData(0)[1]) << 8);
                for (int i = 0; i < MAX_FACES; i++)
                {
                    if (facesArray[i].faceID == currentFaceID)
                    {
                        eraseFaceData((byte)i);
                        if (counter != 0) { counter--; }
                        break;
                    }
                }
                rearrangeFaces();
                if (!isInACallback())
                {
                    if (isdeletedAssigned)
                    {
                        enteringACallback();
                        onDeletedFaceCallback.OnNotVisible(currentFaceID);
                        exitingACallback();
                    }
                }
            }
        }

        private void eraseFaceData(byte i)
        {
            facesArray[i].faceID = -1;
            facesArray[i].leftEyeOpened = -1;
            facesArray[i].rightEyeOpened = -1;
            facesArray[i].smiling = -1;
            facesArray[i].xCoordinate = 0;
            facesArray[i].yCoordinate = 0;
            facesArray[i].faceWidth = 0;
            facesArray[i].faceHeight = 0;
        }

        private void rearrangeFaces()
        {
            for (int i = 0; i < MAX_FACES; i++)
            {
                if (facesArray[i].faceID < 0)
                {
                    int j = i + 1;
                    while (facesArray[j].faceID < 0 && j < MAX_FACES) { j++; }
                    if (j == MAX_FACES)
                    {
                        break;
                    }
                    else
                    {
                        facesArray[i] = facesArray[j];
                        eraseFaceData((byte)j);
                    }
                }
            }
        }

        //Input Function ID
        const byte FACE_GET = 0x01;
        const byte FACE_OUT_OF_BOUNDS = 0x02;

        //Literals
        const int MAX_FACES = 20;
    }
}
