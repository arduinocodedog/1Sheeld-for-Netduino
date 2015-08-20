using System.Collections;

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
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_STOP);
        }

        //Play Setter
        public void play()
        {
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PLAY);
        }

        //Pause Setter
        public void pause()
        {
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PAUSE);
        }

        //Previous Setter
        public void previous()
        {
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PREVIOUS);
        }

        //Next Setter
        public void next()
        {
	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_NEXT);
        }

        //SeekForward Setter
        public void seekForward(byte x)
        {
            ArrayList args = new ArrayList();

            byte[] parm = new byte[1];
            parm[0] = x;

            FunctionArg arg = new FunctionArg(1, parm);

            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_SEEK_FORWARD,1,args);
        }

        //SeekBackward Setter
        public void seekBackward(byte x)
        {
            ArrayList args = new ArrayList();

            byte[] parm = new byte[1];
            parm[0] = x;

            FunctionArg arg = new FunctionArg(1, parm);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_SEEK_BACKWARD, 1, args);
        }

        //Volume Setter
        public void setVolume(byte x)
        {
	        if (x > 10) 
                x=10;
	        else if (x < 0) 
                x=0;

            ArrayList args = new ArrayList();

            byte[] parm = new byte[1];
            parm[0] = x;

            FunctionArg arg = new FunctionArg(1, parm);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_VOLUME, 1, args);
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
    }
}
