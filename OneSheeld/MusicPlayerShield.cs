using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class MusicPlayerShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        public MusicPlayerShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.MUSIC_PLAYER_ID)
        {
            Sheeld = onesheeld;
        }

        //Stop Setter
        public void stop()
        {
	        Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_STOP,0,null);
        }

        //Play Setter
        public void play()
        {
	        Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PLAY,0,null);
        }

        //Pause Setter
        public void pause()
        {
	        Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PAUSE,0,null);
        }

        //Previous Setter
        public void previous()
        {
	        Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_PREVIOUS,0,null);
        }

        //Next Setter
        public void next()
        {
	        Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_NEXT,0,null);
        }

        //SeekForward Setter
        public void seekForward(byte x)
        {
            ArrayList args = new ArrayList();

            byte[] parm = new byte[1];
            parm[0] = x;

            FunctionArg arg = new FunctionArg(1, parm);

            args.Add(arg);

	        Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID,0,MUSIC_SEEK_FORWARD,1,args);
        }

        //SeekBackward Setter
        public void seekBackward(byte x)
        {
            ArrayList args = new ArrayList();

            byte[] parm = new byte[1];
            parm[0] = x;

            FunctionArg arg = new FunctionArg(1, parm);

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_SEEK_BACKWARD, 1, args);
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

            Sheeld.sendPacket(ShieldIds.MUSIC_PLAYER_ID, 0, MUSIC_VOLUME, 1, args);
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
