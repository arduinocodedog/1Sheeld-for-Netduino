namespace OneSheeldClasses
{
    public interface IRecordTypeCallback
    {
        void OnRecordType(byte id, byte[] data, byte typeLength);
    }
}
