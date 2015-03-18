using System;
using Microsoft.SPOT;

namespace AdvancedTTS
{
    public class Program
    {
        public static void Main()
        {
            OneSheeldClasses.OneSheeldUser.Run(new TextToSpeech());
        }

    }
}
