using System;
using OneSheeldClasses;

namespace AdvancedBARCODESCANNER
{
    class BarcodeScanner : OneSheeldUser, IOneSheeldSketch, IBarcodeErrorCallback, IBarcodeParameterValueCallback, IBarcodeScannedCallback
    {
        /* Declare some strings to receive data from 2D code. */
        string sendTo = "";
        string subject = "";
        string body = "";

        public void Setup()
        {
            OneSheeld.begin();

            BARCODESCANNER.setOnNewBarcodeScanned(this);
            BARCODESCANNER.setOnParameterValueResponse(this);
            BARCODESCANNER.setOnError(this);
        }

        public void Loop()
        {
            /* Leave the loop function empty. */
        }

        //Barcode_Formats
        const byte UPC_A = 0x01;
        const byte UPC_E = 0x02;
        const byte EAN8 = 0x03;
        const byte EAN13 = 0x04;
        const byte CODE39 = 0x05;
        const byte CODE93 = 0x06;
        const byte CODE128 = 0x07;
        const byte PDF417 = 0x08;
        const byte QRCODE = 0x09;
        const byte AZTEC = 0x0A;
        const byte ITF = 0x0B;
        const byte ITF14 = 0x0C;
        const byte DATA_MATRIX = 0x0D;

        //2D Barcode Categories
        const byte NO_CATEGORY = 0x00;
        const byte CODE_URL = 0x01;
        const byte CODE_TXT = 0x02;
        const byte CODE_EMAIL = 0x03;
        const byte CODE_VCARD = 0x04;
        const byte CODE_SMS = 0x05;
        const byte CODE_CALL = 0x06;
        const byte CODE_GEO = 0x07;
        const byte CODE_WIFI = 0x08;
        const byte CODE_EVENT = 0x09;

        public void OnBarcodeScanned(byte barcodeFormat, byte barcodeCategory, int barcodeMaxLength, string barcodeData)
        {
            /* Check if the code scanned is a QR code or a data matrix code. */
            if (barcodeFormat == QRCODE || barcodeFormat == DATA_MATRIX)
            {
                /* Check if the code scanned with email category type. */
                if (barcodeCategory == CODE_EMAIL)
                {
                    /* Terminal line for debugging. */
                    TERMINAL.println("Email scanned from barcode.");

                    /* Query parameters from barcode scanner. */
                    BARCODESCANNER.queryParameterValue("mailto");
                    BARCODESCANNER.queryParameterValue("subject");
                    BARCODESCANNER.queryParameterValue("body");
                }

            }
        }

        public void OnParameterValue(string parameter, string value)
        {
            /* Save incoming parameter data.*/
            if (parameter.Equals("mailto"))
            {
                sendTo = value;
            }
            else if (parameter.Equals("subject"))
            {
                subject = value;
            }
            else if (parameter.Equals("body"))
            {
                body = value;
            }

            /* Check if the strings are not empty send the email. */
            if (sendTo.Length != 0 && subject.Length != 0 && body.Length != 0)
            {
                /* Send email. */
                EMAIL.send(sendTo, subject, body);

                sendTo = "";
                subject = "";
                body = "";
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
};