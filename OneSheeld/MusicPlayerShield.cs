namespace OneSheeldClasses
{
    public class MusicPlayerShield : ShieldParent
    {
        public MusicPlayerShield()
            : base(ShieldIds.MUSIC_PLAYER_ID)
        {
        }

        //Stop Setter
        public void stop()
        {
	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_STOP);
        }

        //Play Setter
        public void play()
        {
	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PLAY);
        }

        //Pause Setter
        public void pause()
        {
	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PAUSE);
        }

        public void play(string fileName)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(fileName);
            args.Add(arg);
            
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_PLAY_FILE_NAME, 1, args);
        }

        public void play(int fileIndex)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(fileIndex);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_PLAYD_FILE_INDEX, 1, args);
        }

        //Previous Setter
        public void previous()
        {
	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PREVIOUS);
        }

        //Next Setter
        public void next()
        {
	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_NEXT);
        }

        //SeekForward Setter
        public void seekForward(byte x)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(x);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_SEEK_FORWARD,1,args);
        }

        //SeekBackward Setter
        public void seekBackward(byte x)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(x);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_SEEK_BACKWARD, 1, args);
        }

        //Volume Setter
        public void setVolume(byte x)
        {
	        if (x > 10) 
                x=10;

            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(x);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_VOLUME, 1, args);
        }

        //Output Functions ID's
        const byte MUSIC_STOP = 0x01;
        const byte MUSIC_PLAY = 0x02;
        const byte MUSIC_PAUSE = 0x03;
        const byte MUSIC_PREVIOUS = 0x04;
        const byte MUSIC_NEXT = 0x05;
        const byte MUSIC_SEEK_FORWARD = 0x06;
        const byte MUSIC_SEEK_BACKWARD = 0x07;
        const byte MUSIC_VOLUME = 0x08;
        const byte MUSIC_PLAY_FILE_NAME = 0x09;
        const byte MUSIC_PLAYD_FILE_INDEX = 0x0A;
    }
}
