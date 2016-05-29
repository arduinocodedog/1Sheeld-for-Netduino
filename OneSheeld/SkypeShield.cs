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
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(username);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.SKYPE_ID, 0, SKYPE_CALL, 1, args);
        }

        public void videoCall(string username)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(username);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.SKYPE_ID, 0, SKYPE_VIDEO_CALL, 1, args);
        }

        //Output Function ID's
        const byte SKYPE_CALL = 0x01;
        const byte SKYPE_VIDEO_CALL = 0x02;
    }
}
