using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class CameraShield : ShieldParent
    {
        public CameraShield()
            : base(ShieldIds.CAMERA_ID)
        {
        }

        //Rear Capture 
        public void rearCapture()
        {
            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.CAMERA_ID, 0, CAMERA_REAR_CAPTURE, 0, null);
        }

        //Camera Flash 
        public void setFlash(byte x)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = x;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.CAMERA_ID, 0, CAMERA_SET_FLASH, 1, args);
        }

        //Front Capture 
        public void frontCapture()
        {
            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.CAMERA_ID, 0, CAMERA_FRONT_CAPTURE, 0, null);
        }

        //Quality Set
        public void setQuality(byte x)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = x;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.CAMERA_ID, 0, CAMERA_SET_QUALITY, 1, args);
        }

        //Output Functions ID's
        const byte CAMERA_REAR_CAPTURE = 0x01;
        const byte CAMERA_SET_FLASH = 0x02;
        const byte CAMERA_FRONT_CAPTURE = 0x03;
        const byte CAMERA_SET_QUALITY = 0x04;

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
