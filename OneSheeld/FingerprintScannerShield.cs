namespace OneSheeldClasses
{
    public class FingerprintScannerShield : ShieldParent
    {
        bool isFingerVerified = false;
        bool isNewFingerprintScanned = false;
        bool isErrorCallbackAssigned = false;
        bool isFingerprintCallbackAssigned = false;
        IFingerprintErrorCallback errorCallback = null;
        INewFingerScannedCallback fingerprintCallback = null;

        public FingerprintScannerShield()
            : base(ShieldIds.FINGERPRINT_ID) 
        { }

        public bool isVerified()
        {
            isNewFingerprintScanned = false;
            return isFingerVerified;
        }

        public bool isNewFingerScanned()
        {
            return isNewFingerprintScanned;
        }

        public void scan()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.FINGERPRINT_ID, 0, FINGERPRINT_SCAN, 0);
        }

        public void setOnError(IFingerprintErrorCallback userCallback)
        {
            isErrorCallbackAssigned = true;
            errorCallback = userCallback;
        }

	    public void setOnNewFingerScanned(INewFingerScannedCallback userCallback)
        {
            isFingerprintCallbackAssigned = true;
            fingerprintCallback = userCallback;
        }

        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();

            if (functionID == FINGERPRINT_GET)
            {
                isNewFingerprintScanned = true;
                isFingerVerified = (getOneSheeldInstance().getArgumentData(0)[0] == 0x00) ? false : true;
                //Invoke User Function
                if (!isInACallback())
                {
                    if (isFingerprintCallbackAssigned)
                    {
                        enteringACallback();
                        fingerprintCallback.OnFingerprintScanned(isFingerVerified);
                        exitingACallback();
                    }
                }
            }
            else if (functionID == FINGERPRINT_GET_ERROR && !isInACallback())
            {
                byte errorNumber = getOneSheeldInstance().getArgumentData(0)[0];
                //Invoke User Function
                if (isErrorCallbackAssigned)
                {
                    enteringACallback();
                    errorCallback.OnError(errorNumber);
                    exitingACallback();
                }
            }
        }

         //Output  function ID's
        const byte FINGERPRINT_SCAN = 0x01;

        //Input Function ID's 
        const byte FINGERPRINT_GET = 0x01;
        const byte FINGERPRINT_GET_ERROR = 0x02;

        //Errors messages 
        const byte AUTHENTICATION_FAILED = 0x01;
        const byte USER_CANCEL = 0x02;
        const byte USER_FALLBACK = 0x03;
        const byte SYSTEM_CANCEL = 0x04;
        const byte PASSCODE_NOT_SET = 0x05;
        const byte TOUCHID_NOT_AVAILABLE = 0x06;
        const byte TOUCHID_NOT_ENROLLED = 0x07;
        const byte TOUCHID_LOCKOUT = 0x08;
        const byte APP_CANCEL = 0x09;
        const byte INVALID_CONTEXT = 0x0A;
    }
}
