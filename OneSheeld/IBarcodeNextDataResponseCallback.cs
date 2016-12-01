namespace OneSheeldClasses
{
    public interface IBarcodeNextDataResponseCallback
    {
        void OnNextDataResponse(byte barcodeDataLength, string barcodeData);
    }
}
