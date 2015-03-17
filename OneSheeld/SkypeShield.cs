using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class SkypeShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        public SkypeShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.SKYPE_ID)
        {
            Sheeld = onesheeld;
        }

        public void call(string username)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(username.Length, System.Text.Encoding.UTF8.GetBytes(username));

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.SKYPE_ID, 0, SKYPE_CALL, 1, args);
        }

        public void videoCall(string username)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(username.Length, System.Text.Encoding.UTF8.GetBytes(username));

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.SKYPE_ID, 0, SKYPE_VIDEO_CALL, 1, args);
        }

        //Output Function ID's
        const byte SKYPE_CALL = 0x01;
        const byte SKYPE_VIDEO_CALL = 0x02;
    }
}
