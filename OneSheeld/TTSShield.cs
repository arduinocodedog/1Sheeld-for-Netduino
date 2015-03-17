using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class TTSShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        public TTSShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.TTS_ID)
        {
            Sheeld = onesheeld;
        }

        public void say(byte[] text)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(text.Length, text);

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.TTS_ID, 0, TTS_SAY, 1, args);
        }

        public void say(string text)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(text.Length, System.Text.Encoding.UTF8.GetBytes(text));

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.TTS_ID, 0, TTS_SAY, 1, args);
        }

        //Output Function ID
        const byte TTS_SAY = 0x01;
    }
}
