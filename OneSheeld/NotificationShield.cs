using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class NotificationShield
    {
        OneSheeld Sheeld = null;

        public NotificationShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
        }

        public void notifyPhone(string data)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(data.Length, System.Text.Encoding.UTF8.GetBytes(data));

            args.Add(arg);

	        Sheeld.sendPacket(ShieldIds.NOTIFICATION_ID,0,NOTIFICATION_NOTIFY_PHONE,1,args);
        }

        const byte NOTIFICATION_NOTIFY_PHONE = 0x01;
    }
}
