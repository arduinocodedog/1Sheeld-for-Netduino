namespace OneSheeldClasses
{
    public interface IRecordParsedCallback
    {
        void OnRecordParsed(byte id, byte[] data);
    }
}
