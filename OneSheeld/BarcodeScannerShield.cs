namespace OneSheeldClasses
{
    public class BarcodeScannerShield : ShieldParent
    {
        bool isNewBarcode = false;
        bool isCallbackAssigned = false;
        bool isNextDataResponseCallbackAssigned = false;
        bool isParameterCallbackAssigned = false;
        bool isErrorCallbackAssigned = false;
        bool isNext = false;
        byte barcodeDataLength = 0x00;
        byte barcodeFormat = 0x00;
        byte barcodeCategory = 0x00;
        byte errorNumber = 0x00;
        int barcodeMaxLength = 0;
        int index = 0;
        string barcodeData ="";

        IBarcodeScannedCallback barcodeCallback = null;
        IBarcodeNextDataResponseCallback nextDataResponseCallback = null;
        IBarcodeParameterValueCallback parameterValueCallback = null;
        IBarcodeErrorCallback errorCallback = null;

        public BarcodeScannerShield()
            :base(ShieldIds.BARCODE_ID)
        {
        }

        public void queryNextDataBytes(byte length = 128)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(length);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.BARCODE_ID, 0, BARCODE_QUERY_NEXT_BYTES, 1, args);
        }

        public void queryParameterValue(string parameter)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(parameter);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.BARCODE_ID, 0, BARCODE_QUERY_PARAMETER, 1, args);
        }

        public string getData()
        {
            isNewBarcode = false;
            isNext = false;
            return barcodeData;
        }

        public int getMaxDataLength()
        {
            return barcodeMaxLength;
        }

        public bool isNewBarcodeScanned()
        {
            return isNewBarcode;
        }

        public bool isFullySent()
        {
            return (index >= barcodeMaxLength);
        }

        public bool isNextDataBytesReceived()
        {
            return isNext;
        }

        public byte getCategory()
        {
            isNewBarcode = false;
            return barcodeCategory;
        }

        public byte getFormat()
        {
            isNewBarcode = false;
            return barcodeFormat;
        }

        public byte getDataLength()
        {
            return barcodeDataLength;
        }

        //Process Input Data
        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();

            if (functionID == BARCODE_GET_1D)
            {
                isNewBarcode = true;
                barcodeFormat = getOneSheeldInstance().getArgumentData(0)[0];
                barcodeMaxLength = getOneSheeldInstance().getArgumentData(1)[0] | ((getOneSheeldInstance().getArgumentData(1)[1]) << 8);

                if (barcodeData != "")
                {
                    barcodeData = "";
                }

                barcodeDataLength = getOneSheeldInstance().getArgumentLength(2);
                index = barcodeDataLength;
                for (int j = 0; j < barcodeDataLength; j++)
                {
                    barcodeData += getOneSheeldInstance().getArgumentData(2)[j];
                }

                //Invoke Users function
                if (!isInACallback())
                {
                    if (isCallbackAssigned)
                    {
                        enteringACallback();
                        barcodeCallback.OnBarcodeScanned(barcodeFormat, NO_CATEGORY, barcodeMaxLength, barcodeData);
                        exitingACallback();
                    }
                }

            }
            else if (functionID == BARCODE_GET_2D)
            {
                isNewBarcode = true;
                barcodeFormat = getOneSheeldInstance().getArgumentData(0)[0];
                barcodeCategory = getOneSheeldInstance().getArgumentData(1)[0];
                barcodeMaxLength = getOneSheeldInstance().getArgumentData(2)[0] | ((getOneSheeldInstance().getArgumentData(2)[1]) << 8);

                if (barcodeData != "")
                {
                    barcodeData = "";
                }

                barcodeDataLength = getOneSheeldInstance().getArgumentLength(3);
                index = barcodeDataLength;
                for (int j = 0; j < barcodeDataLength; j++)
                {
                    barcodeData += getOneSheeldInstance().getArgumentData(3)[j];
                }

                //Invoke Users function
                if (!isInACallback())
                {
                    if (isCallbackAssigned)
                    {
                        enteringACallback();
                        barcodeCallback.OnBarcodeScanned(barcodeFormat, barcodeCategory, barcodeMaxLength, barcodeData);
                        exitingACallback();
                    }
                }
            }
            else if (functionID == BARCODE_GET_NEXT && !isInACallback())
            {
                isNext = true;
                if (barcodeData != "")
                {
                    barcodeData = "";
                }

                barcodeDataLength = getOneSheeldInstance().getArgumentLength(0);
                index += barcodeDataLength;
                for (int j = 0; j < barcodeDataLength; j++)
                {
                    barcodeData += getOneSheeldInstance().getArgumentData(0)[j];
                }

                //Invoke User Function
                if (isNextDataResponseCallbackAssigned)
                {
                    enteringACallback();
                    nextDataResponseCallback.OnNextDataResponse(barcodeDataLength, barcodeData);
                    exitingACallback();
                }
            }
            else if (functionID == BARCODE_GET_PARAMETER && !isInACallback())
            {
                byte parameterLength = getOneSheeldInstance().getArgumentLength(0);
                byte valueLength = getOneSheeldInstance().getArgumentLength(1);
                string parameter = "";
                string value = "";

                for (int j = 0; j < parameterLength; j++)
                {
                    parameter += getOneSheeldInstance().getArgumentData(0)[j];
                }

                for (int j = 0; j < valueLength; j++)
                {
                    value += getOneSheeldInstance().getArgumentData(1)[j];
                }

                //Invoke User Function
                if (isParameterCallbackAssigned)
                {
                    enteringACallback();
                    parameterValueCallback.OnParameterValue(parameter, value);
                    exitingACallback();
                }
            }
            else if (functionID == BARCODE_GET_ERROR && !isInACallback())
            {
                errorNumber = getOneSheeldInstance().getArgumentData(0)[0];

                //Invoke User Function
                if (isErrorCallbackAssigned)
                {
                    enteringACallback();
                    errorCallback.OnError(errorNumber);
                    exitingACallback();
                }
            }
        }

        //Users Function Setter 
        public void setOnNewBarcodeScanned(IBarcodeScannedCallback userFunction)
        {
	        barcodeCallback=userFunction;
	        isCallbackAssigned=true;
        }

        public void setOnNextDataResponse(IBarcodeNextDataResponseCallback userFunction)
        {
	        nextDataResponseCallback=userFunction;
	        isNextDataResponseCallbackAssigned=true;
        }

        public void setOnParameterValueResponse(IBarcodeParameterValueCallback userFunction)
        {
	        parameterValueCallback=userFunction;
	        isParameterCallbackAssigned=true;
        }

        public void setOnError(IBarcodeErrorCallback userFunction)
        {
	        errorCallback=userFunction;
	        isErrorCallbackAssigned=true;
        }

        //Output  function ID's
        const byte BARCODE_QUERY_PARAMETER = 0x01;
        const byte BARCODE_QUERY_NEXT_BYTES = 0x02;

        //Input Function ID's 
        const byte BARCODE_GET_1D = 0x01;
        const byte BARCODE_GET_2D = 0x02;
        const byte BARCODE_GET_NEXT = 0x03;
        const byte BARCODE_GET_PARAMETER = 0x04;
        const byte BARCODE_GET_ERROR = 0x05;

        //2D Barcode Categories
        const byte NO_CATEGORY = 0x00;
    }
}
