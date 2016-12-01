namespace OneSheeldClasses
{
    public interface IBarcodeScannedCallback
    {
        void OnBarcodeScanned(byte barcodeFormat, byte barcodeCategory, int barcodeMaxLength, string barcodeData);
    }
}
