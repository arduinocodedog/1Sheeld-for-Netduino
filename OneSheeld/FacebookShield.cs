using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class FacebookShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        public FacebookShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.FACEBOOK_ID)
        {
            Sheeld = onesheeld;
        }

        public void post(string status)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(status.Length, System.Text.Encoding.UTF8.GetBytes(status));

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.FACEBOOK_ID, 0, FACEBOOK_UPDATE_STATUS, 1, args);
        }

        public void postLastPicture(string pictureText, byte imageSource)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(pictureText.Length, System.Text.Encoding.UTF8.GetBytes(pictureText));

            args.Add(arg1);

            byte[] imgsrc = new byte[1];
            imgsrc[0] = (byte)imageSource;

            FunctionArg arg2 = new FunctionArg(1, imgsrc);

            args.Add(arg2);

            Sheeld.sendPacket(ShieldIds.FACEBOOK_ID, 0, FACEBOOK_POST_LAST_PIC, 2, args);
        }

        const byte FACEBOOK_UPDATE_STATUS = 0x01;
        const byte FACEBOOK_POST_LAST_PIC = 0x02;
    }
}
