namespace OneSheeldClasses
{
    public interface IHttpJsonResponseCallback
    {
        void OnJsonResponse(JsonKeyChain chain, byte[] data);
    }
}
