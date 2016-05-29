namespace OneSheeldClasses
{
    public class TTSShield : ShieldParent
    {
        public TTSShield()
            : base(ShieldIds.TTS_ID)
        {
        }

        public void say(byte[] text)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(text);
           args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.TTS_ID, 0, TTS_SAY, 1, args);
        }

        public void say(string text)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(text);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.TTS_ID, 0, TTS_SAY, 1, args);
        }

        //Output Function ID
        const byte TTS_SAY = 0x01;
    }
}
