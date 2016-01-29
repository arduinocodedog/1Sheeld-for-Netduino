using System.Collections;

namespace OneSheeldClasses
{
    public class NotificationShield : ShieldParent
    {
        public NotificationShield()
            : base(ShieldIds.NOTIFICATION_ID)
        {
        }

        public void notifyPhone(string data)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.NOTIFICATION_ID,0,NOTIFICATION_NOTIFY_PHONE,1,args);
        }

        const byte NOTIFICATION_NOTIFY_PHONE = 0x01;
    }
}
