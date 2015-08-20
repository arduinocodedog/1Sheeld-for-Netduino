namespace OneSheeldClasses
{
    public interface ISubscribeCallback
    {
        void OnSubscribeOrDigitalChange(byte incomingPinNumber, bool incommingPinValue);
    }
}
