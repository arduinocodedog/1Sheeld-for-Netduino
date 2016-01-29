using System.Collections;

namespace OneSheeldClasses
{
    public class FacebookShield : ShieldParent
    {
        public FacebookShield()
            : base(ShieldIds.FACEBOOK_ID)
        {
        }

        public void post(string status)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(status);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.FACEBOOK_ID, 0, FACEBOOK_UPDATE_STATUS, 1, args);
        }

        public void postLastPicture(string pictureText, byte imageSource)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(pictureText);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(imageSource);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.FACEBOOK_ID, 0, FACEBOOK_POST_LAST_PIC, 2, args);
        }

        const byte FACEBOOK_UPDATE_STATUS = 0x01;
        const byte FACEBOOK_POST_LAST_PIC = 0x02;
    }
}
