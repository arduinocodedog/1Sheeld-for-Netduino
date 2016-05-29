namespace OneSheeldClasses
{
    public class EmailShield : ShieldParent
    {
        public EmailShield()
            : base(ShieldIds.EMAIL_ID)
        {
        }

        //Email Sender
        public void send(string email, string subject, string message)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(email);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(subject);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(message);
            args.Add(arg3);

	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.EMAIL_ID, 0, EMAIL_SEND, 3, args);
        }

        //Attaching picture
        public void attachLastPicture(string email, string subject, string message, byte imageSource)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(email);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(subject);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(message);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(imageSource);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.EMAIL_ID, 0, EMAIL_ATTACH_PICTURE, 4, args);
        }

        const byte EMAIL_SEND = 0x01;
        const byte EMAIL_ATTACH_PICTURE = 0x02;
    }
}
