using System;
using Microsoft.SPOT;

namespace AdvancedTTS
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            TextToSpeech tts = new TextToSpeech();
            tts.Setup();
            while (true)
                tts.Loop();
        }

    }
}
