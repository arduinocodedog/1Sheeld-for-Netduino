namespace OneSheeldClasses
{
    public interface IVoiceRecognitionCallback
    {
        void OnNewCommand(string voice);
        void OnError(byte error);
    }
}
