using OneSheeldClasses;

namespace SimpleBARCODESCANNER
{
    class BarcodeScanner : OneSheeldUser, IOneSheeldSketch, IBarcodeErrorCallback
    {
        public void Setup()
        {
            OneSheeld.begin();

            BARCODESCANNER.setOnError(this);
        }

        public void Loop()
        {
            /* Check if a new barcode is scanned. */
            if (BARCODESCANNER.isNewBarcodeScanned())
            {
                /* Print max data length saved in barcode. */
                TERMINAL.println(BARCODESCANNER.getMaxDataLength());

                /* Print first 128 bytes of barcode data. */
                TERMINAL.println(BARCODESCANNER.getData());

                /* If the barcode has more that 128 bytes, query its next data bytes. */
                if (BARCODESCANNER.getMaxDataLength() > 128)
                {
                    /* Query the next 128 (default) bytes. */
                    BARCODESCANNER.queryNextDataBytes();
                }
            }
            /* Check if the data is the next bytes of same barcode. */
            if (BARCODESCANNER.isNextDataBytesReceived())
            {
                /* Check if the barcode data was fully sent. */
                if (!BARCODESCANNER.isFullySent())
                {
                    /* Query the next 128 (default) bytes. */
                    BARCODESCANNER.queryNextDataBytes();
                }
                else
                {
                    /* Print data is fully sent from barcode shield. */
                    TERMINAL.println("Fully sent");
                }
                /* Print the next data bytes. */
                TERMINAL.println(BARCODESCANNER.getData());
            }
        }

        //Errors messages 
        const byte BARCODE_NOT_SUPPORTED = 0x01;
        const byte BARCODE_SCANNING_ERROR = 0x02;
        const byte WRONG_PARAMETER = 0x03;
        const byte NOT_ENOUGH_BYTES = 0x04;
        const byte CATEGORY_NOT_SUPPORTED = 0x05;

        public void OnError(byte errno)
        {
            /* Switch on error and print it on the terminal. */
            switch (errno)
            {
                case BARCODE_NOT_SUPPORTED: TERMINAL.println("Barcode is not supported"); break;
                case BARCODE_SCANNING_ERROR: TERMINAL.println("Barcode scanning error"); break;
                case WRONG_PARAMETER: TERMINAL.println("Wrong parameter"); break;
                case NOT_ENOUGH_BYTES: TERMINAL.println("No enough bytes"); break;
                case CATEGORY_NOT_SUPPORTED: TERMINAL.println("Category not supported"); break;
            }
        }
    }
}
