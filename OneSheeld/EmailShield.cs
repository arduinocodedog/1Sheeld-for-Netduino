using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class EmailShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        public EmailShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.EMAIL_ID)
        {
            Sheeld = onesheeld;
        }

        //Email Sender
        public void send(string email, string subject, string message)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(email.Length, System.Text.Encoding.UTF8.GetBytes(email));

            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(subject.Length, System.Text.Encoding.UTF8.GetBytes(subject));

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(message.Length, System.Text.Encoding.UTF8.GetBytes(message));

            args.Add(arg3);

	        Sheeld.sendPacket(ShieldIds.EMAIL_ID, 0, EMAIL_SEND, 3, args);
        }

        //Attaching picture
        public void attachLastPicture(string email, string subject, string message, byte imageSource)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(email.Length, System.Text.Encoding.UTF8.GetBytes(email));

            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(subject.Length, System.Text.Encoding.UTF8.GetBytes(subject));

            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(message.Length, System.Text.Encoding.UTF8.GetBytes(message));

            args.Add(arg3);

            byte[] imgsrc = new byte[1];
            imgsrc[0] = imageSource;

            FunctionArg arg4 = new FunctionArg(1, imgsrc);
            args.Add(arg4);

            Sheeld.sendPacket(ShieldIds.EMAIL_ID, 0, EMAIL_ATTACH_PICTURE, 4, args);
        }

        const byte EMAIL_SEND = 0x01;
        const byte EMAIL_ATTACH_PICTURE = 0x02;
    }
}
