namespace OneSheeldClasses
{
    public interface IInternetErrorCallback
    {
        void OnError(int requestid, int errorNumber);
    }
}
