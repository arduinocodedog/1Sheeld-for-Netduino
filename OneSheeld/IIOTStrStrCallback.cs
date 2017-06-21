namespace OneSheeldClasses
{
    public interface IIOTStrStrCallback
    {
        void OnsetNewMessageStrStr(string topic, string payload, byte qos, bool retain);
    }
}
