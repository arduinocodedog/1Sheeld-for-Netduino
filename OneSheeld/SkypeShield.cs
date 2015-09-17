using System.Collections;

namespace OneSheeldClasses
{
    public class SkypeShield : ShieldParent
    {
        public SkypeShield()
            : base(ShieldIds.SKYPE_ID)
        {
        }

        public void call(string username)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(username.Length, System.Text.Encoding.UTF8.GetBytes(username));

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.SKYPE_ID, 0, SKYPE_CALL, 1, args);
        }

        public void videoCall(string username)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(username.Length, System.Text.Encoding.UTF8.GetBytes(username));

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.SKYPE_ID, 0, SKYPE_VIDEO_CALL, 1, args);
        }

        //Output Function ID's
        const byte SKYPE_CALL = 0x01;
        const byte SKYPE_VIDEO_CALL = 0x02;
    }
}
