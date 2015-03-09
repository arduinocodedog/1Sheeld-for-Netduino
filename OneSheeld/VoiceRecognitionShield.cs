using System;
using System.Text;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class VoiceRecognitionShield : ShieldParent, IShieldChild
    {
        OneSheeld Sheeld = null;

        string voice = null;
        bool isCallbackAssigned = false;
        bool newCommand = false;
        byte errorNumber = 0x00;
        IVoiceRecognitionCallback userCallback = null;

        public VoiceRecognitionShield(OneSheeld onesheeld)
            : base(onesheeld, (byte) ShieldIds.VOICE_RECOGNITION_ID)
        {
            Sheeld = onesheeld;

            SetChild(this);
        }

        public void start()
        {
            Sheeld.sendPacket(ShieldIds.VOICE_RECOGNITION_ID, 0, VOICE_START_LISTENING, 0, null);
        }

        public string getLastCommand()
        {
            newCommand = false;
            return voice;
        }

        public int getLastCommandLength()
        {
            if (voice == null)
                return -1;

            return voice.Length;
        }

        public bool isNewCommandReceived()
        {
            return newCommand;
        }

        void IShieldChild.processData()
        {
            byte functionID = Sheeld.getFunctionId();

            if (functionID == VOICE_GET)
            {
                if (voice != null)
                    voice = "";

                int voicetextLength = Sheeld.getArgumentLength(0);

                if (voicetextLength > 0)
                {
                    for (int j = 0; j < voicetextLength; j++)
                        voice += Convert.ToChar(Sheeld.getArgumentData(0)[j]);

                    newCommand = true;
                }

                if (isCallbackAssigned)
                    userCallback.OnNewCommand(voice);
            }
            else if (functionID == VOICE_GET_ERROR)
            {
                errorNumber = Sheeld.getArgumentData(0)[0];

                if (isCallbackAssigned)
                    userCallback.OnError(errorNumber);
            }

        }

        public void setVoiceRecognitionCallback(IVoiceRecognitionCallback callback)
        {
            userCallback = callback;
            isCallbackAssigned = true;
        }

        //Input Function ID's 
        const byte VOICE_GET = 0x01;
        const byte VOICE_START_LISTENING = 0x01;
        const byte VOICE_GET_ERROR = 0x02;

        //Errors messages 
        const byte NETWORK_TIMEOUT_ERROR = 0x01;
        const byte NETWORK_ERROR = 0x02;
        const byte AUDIO_ERROR = 0x03;
        const byte SERVER_ERROR = 0x04;
        const byte SPEECH_TIMEOUT_ERROR = 0x06;
        const byte NO_MATCH_ERROR = 0x07;
        const byte RECOGNIZER_BUSY_ERROR = 0x08;
    }
}
