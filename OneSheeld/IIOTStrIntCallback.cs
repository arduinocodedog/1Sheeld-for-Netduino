namespace OneSheeldClasses
{
    public interface IIOTStrIntCallback
    {
        void OnsetNewMessageStrInt(string topic, int payload, byte qos, bool retain);
    }
}
