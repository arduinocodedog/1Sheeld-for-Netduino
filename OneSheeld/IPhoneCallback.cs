namespace OneSheeldClasses
{
    public interface IPhoneCallback
    {
        void OnCallStatusChange(bool isRinging, string phoneNumber);
    }
}
