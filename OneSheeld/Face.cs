using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class Face
    {
        private int _faceID = -1;
        public int faceID
        {
            get { return _faceID; }
            set { _faceID = value; }
        }

        private int _leftEyeOpened = -1;
        public int leftEyeOpened
        {
            get { return _leftEyeOpened; }
            set { _leftEyeOpened = value; }
        }

        private int _rightEyeOpened = -1;
        public int rightEyeOpened
        {
            get { return _rightEyeOpened;  }
            set { _rightEyeOpened = value; }
        }

        private int _smiling = -1;
        public int smiling
        {
            get { return _smiling; }
            set { _smiling = value;  }
        }

        private int _xCoordinate = 0;
        public int xCoordinate
        {
            get { return _xCoordinate; }
            set { _xCoordinate = value; }
        }

        private int _yCoordinate = 0;
        public int yCoordinate
        {
            get { return _yCoordinate; }
            set { _yCoordinate = value; }
        }

        private uint _faceWidth = 0;
        public uint faceWidth
        {
            get { return _faceWidth; }
            set { _faceWidth = value; }
        }

        private uint _faceHeight = 0;
        public uint faceHeight
        {
            get { return _faceHeight; }
            set { _faceHeight = value; }
        }
    }
}
