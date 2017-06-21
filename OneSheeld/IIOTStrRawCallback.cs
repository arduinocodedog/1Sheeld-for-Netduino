namespace OneSheeldClasses
{
    public interface IIOTStrRawCallback
    {
        void OnnewMessageStrRawCallback(string topic, byte[] payload, int payloadlength, byte qos, bool retain);
    }
}
