using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class TTSShield
    {
        OneSheeld Sheeld = null;

        public TTSShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
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
