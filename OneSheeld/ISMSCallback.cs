namespace OneSheeldClasses
{
    public interface ISMSCallback
    {
        void OnSMSReceive(string number, string text);
    }
}
