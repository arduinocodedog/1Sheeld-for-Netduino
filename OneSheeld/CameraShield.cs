namespace OneSheeldClasses
{
    public class CameraShield : ShieldParent
    {
        public CameraShield()
            : base(ShieldIds.CAMERA_ID)
        {
        }

        //Reset Parameters
        public void resetParameters()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_RESET);
        }

        //Rear Capture 
        public void rearCapture()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_REAR_CAPTURE);
        }

        //Camera Flash 
        public void setFlash(byte x)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(x);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_SET_FLASH, 1, args);
        }

        //Front Capture 
        public void frontCapture()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_FRONT_CAPTURE);
        }

        //Quality Set
        public void setQuality(byte x)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(x);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_SET_QUALITY, 1, args);
        }

        public void record(ulong seconds, byte cameraPosition)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(seconds);
            args.Add(arg);

            FunctionArg arg2 = new FunctionArg(cameraPosition);
            args.Add(arg2);
            
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_RECORD_VIDEO, 2, args);
        }

        public void zoom(byte zoomValue, bool smoothZoom)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(zoomValue);
            args.Add(arg);

            FunctionArg arg2 = new FunctionArg(smoothZoom);
            args.Add(arg2);

            if (zoomValue > 100) { zoomValue = 100; }
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CAMERA_ID, 0, CAMERA_ZOOM, 2, args);
        }

        //Output Functions ID's
        const byte CAMERA_RESET = 0x00;
        const byte CAMERA_REAR_CAPTURE = 0x01;
        const byte CAMERA_SET_FLASH = 0x02;
        const byte CAMERA_FRONT_CAPTURE = 0x03;
        const byte CAMERA_SET_QUALITY = 0x04;
        const byte CAMERA_RECORD_VIDEO = 0x05;
        const byte CAMERA_ZOOM = 0x06;

        //Setting Flash (Literals)
        public byte OFF
        {
            get { return 0x02; }
            set { }
        }

        public byte ON
        {
            get { return 0x01; }
            set { }
        }

        public byte AUTO
        {
            get { return 0x02; }
            set { }
        }

        //Literals used by users
        public byte LOW_QUALITY
        {
            get { return 0x01; }
            set { }
        }

        public byte MID_QUALITY
        {
            get { return 0x02; }
            set { }
        }

        public byte HIGH_QUALITY
        {
            get { return 0x03; }
            set { }
        }
    }
}
