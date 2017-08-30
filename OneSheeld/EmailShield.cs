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
            if (email.Length == 0 || subject.Length == 0 || message.Length == 0) return;

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
            if (email.Length == 0 || subject.Length == 0 || message.Length == 0) return;

            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(email);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(subject);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(message);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(imageSource);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.EMAIL_ID, 0, EMAIL_ATTACH_FILE, 4, args);
        }

        public void attachFile(int fileType)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(fileType);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.EMAIL_ID, 0, EMAIL_ATTACH_FILE, 1, args);
        }

        void attachFile(string filePath)
        {
            if (filePath.Length == 0) return;
                
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(filePath);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.EMAIL_ID, 0, EMAIL_ATTACH_FILE_PATH, 1, args);
        }

        const byte EMAIL_SEND = 0x01;
        const byte EMAIL_ATTACH_FILE = 0x02;
        const byte EMAIL_ATTACH_FILE_PATH = 0x03;

        /* LITERALS */
        const byte LAST_CAMERA_PIC_ONESHEELD_FOLDER = 0x00;
        const byte LAST_CAMERA_PIC_CAMERA_FOLDER = 0x01;
        const byte LAST_CAMERA_VIDEO = 0x02;
        const byte LAST_CHART_CSV = 0x04;
        const byte LAST_DATA_LOGGER_CSV = 0x05;
        const byte LAST_MIC_RECORD = 0x06;
        const byte LAST_MUSIC_TRACK = 0x07;
    }
}
