using System.Collections;

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
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(text.Length, text);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TTS_ID, 0, TTS_SAY, 1, args);
        }

        public void say(string text)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(text.Length, System.Text.Encoding.UTF8.GetBytes(text));

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.TTS_ID, 0, TTS_SAY, 1, args);
        }

        //Output Function ID
        const byte TTS_SAY = 0x01;
    }
}
