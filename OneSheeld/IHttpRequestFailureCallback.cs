namespace OneSheeldClasses
{
    public interface IHttpRequestFailureCallback
    {
        void OnFailure(HttpResponse response);
    }
}
