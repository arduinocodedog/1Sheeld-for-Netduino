namespace OneSheeldClasses
{
    public interface IIOTStrFloatCallback
    {
        void OnnewMessageStrFloatCallback(string topic, float payload, byte qos, bool retain);
    }
}
