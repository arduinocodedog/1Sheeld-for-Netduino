using System;
using Microsoft.SPOT;

namespace SimpleMUSICPLAYER
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            MusicPlayer musicplayer = new MusicPlayer();
            musicplayer.Setup();
            while (true)
                musicplayer.Loop();
        }

    }
}
