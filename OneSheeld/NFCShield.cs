namespace OneSheeldClasses
{
    public class NFCShield : ShieldParent
    {
        public static NFCTag nullTag = null;

        bool isErrorAssigned = false;
	    bool isNewTag = false;
	    bool isNewTagSetOnAssigned = false;
	    bool isReadingInProgress = false;
	    bool isTagInterruptsEnabled = false;
        NFCTag tag = null;

        INFCNewTagScanned newTagCallBack = null;
        INFCError errorCallBack = null;

        public NFCShield() :
            base(ShieldIds.NFC_ID)
        {
            nullTag = new NFCTag();
        }

        public NFCTag getLastTag()
        {
	        isNewTag=false;
	        isReadingInProgress=true;
	        if(tag!=null) 
                return tag;
	        return NFCShield.nullTag;
        }

        public bool isNewTagScanned()
        {
	        return isNewTag;
        }

        public void finishedReading()
        {
	        isReadingInProgress=false;
        }

        public void enableTagInterrupts()
        {
	        isTagInterruptsEnabled=true;
        }

        public void disableTagInterrupts()
        {
	        isTagInterruptsEnabled=false;
        }

        public void setOnNewTagScanned(INFCNewTagScanned userCallback)
        {
	        isNewTagSetOnAssigned = true;
	        newTagCallBack = userCallback;
        }

        public void setOnError(INFCError userCallback)
        {
	        isErrorAssigned = true;
	        errorCallBack = userCallback;
        }

        public override void processData()
        {
	        byte functionId = getOneSheeldInstance().getFunctionId();

	        if(functionId == NFC_GET_BASIC_INFO && (!isTagInterruptsEnabled || (!isReadingInProgress && isTagInterruptsEnabled)))
	        {
		        isNewTag = true;

		        int tagIdLength = getOneSheeldInstance().getArgumentLength(0);
		        byte[] tagId = new byte[tagIdLength];

		        if(tagIdLength!=0)
		        {
			        for (int i = 0; i < tagIdLength; i++)
			        {
				        tagId[i] = getOneSheeldInstance().getArgumentData(0)[i];
			        }
		        }
		
		        int tagMaxSize = getOneSheeldInstance().getArgumentData(1)[0]|((getOneSheeldInstance().getArgumentData(1)[1])<<8);

		        byte recordsNumber = getOneSheeldInstance().getArgumentData(2)[0];

		        int tagSize = getOneSheeldInstance().getArgumentData(3)[0]|((getOneSheeldInstance().getArgumentData(3)[1])<<8);

		        if(tag!=null)
                    tag=null;
		        tag = new NFCTag(tagId,(byte)tagIdLength,tagSize,tagMaxSize,recordsNumber);

		        tag.tagIdLength = (byte)tagIdLength;
		        if(tag.recordsNumber>0)
		        {
			        byte argumentNo = getOneSheeldInstance().getArgumentNo();
			        for(int i=4 ;i<argumentNo; i++)
			        {
				        tag.getRecord(i-4).recordType = getOneSheeldInstance().getArgumentData((byte)i)[0];
				        tag.getRecord(i-4).recordTypeLength = getOneSheeldInstance().getArgumentData((byte)i)[1]|(getOneSheeldInstance().getArgumentData((byte)i)[2]<<8);
				        tag.getRecord(i-4).recordDataLength = getOneSheeldInstance().getArgumentData((byte)i)[3]|(getOneSheeldInstance().getArgumentData((byte)i)[4]<<8);
			        }		
		        }

		        if(isNewTagSetOnAssigned && !isInACallback())
		        {
			        enteringACallback();
                    newTagCallBack.OnNewTagScanned(tag);
			        exitingACallback();
		        }		
	        }
	        else if(functionId == NFC_ON_ERROR)
	        {
		        if(isErrorAssigned && !isInACallback())
		        {
			        byte errorNumber = getOneSheeldInstance().getArgumentData(0)[0];
			        enteringACallback();
			        errorCallBack.OnError(errorNumber);
			        exitingACallback();
		        }
	        }
	        else if(functionId == NFC_TAG_ON_PARSED )
	        {
		        if(tag != null && tag.isParsedDataCallBackAssigned && !isInACallback())
		        {
			        byte recordNumber = getOneSheeldInstance().getArgumentData(0)[0];

			        byte parsedDataLength = getOneSheeldInstance().getArgumentLength(1);
			
			        byte[] incomingData = new byte[parsedDataLength+1];

			        for (int i = 0; i < parsedDataLength; i++)
			        {
				        incomingData[i] = getOneSheeldInstance().getArgumentData(1)[i];
			        }
			        incomingData[parsedDataLength]= 0x00;

			        enteringACallback();
			        tag.recordParsedCallBack.OnRecordParsed(recordNumber,incomingData);
			        exitingACallback();
		        }
	        }
	        else if(functionId == NFC_TAG_ON_TYPE)
	        {
		        if(tag != null && tag.isTypeCallBackAssigned && !isInACallback())
		        {
			        byte recordNumber = getOneSheeldInstance().getArgumentData(0)[0];

			        byte typeDataLength = getOneSheeldInstance().getArgumentLength(1);
			        byte[] incomingType = new byte[typeDataLength];

			        for (int i = 0; i < typeDataLength; i++)
			        {
				        incomingType[i] = getOneSheeldInstance().getArgumentData(1)[i];
			        }

			        enteringACallback();
			        tag.recordTypeCallBack.OnRecordType(recordNumber,incomingType,typeDataLength);
			        exitingACallback();
		        }
    	        }
		    else if(functionId == NFC_TAG_ON_DATA)
	        {
		        if(tag != null && tag.isDataCallBackAssigned && !isInACallback())
		        {

			        byte recordNumber = getOneSheeldInstance().getArgumentData(0)[0];

			        byte dataLength = getOneSheeldInstance().getArgumentLength(1);
			        byte[] incomingData = new byte[dataLength];

			        for (int i = 0; i < dataLength; i++)
			        {
				        incomingData[i] = getOneSheeldInstance().getArgumentData(1)[i];
			        }

			        enteringACallback();
			        tag.recordDataCallBack.OnRecordData(recordNumber,incomingData,dataLength);
			        exitingACallback();
		        }
	        }
        }

        // Input Function ID 
        const byte NFC_GET_BASIC_INFO = 0x01;
        const byte NFC_ON_ERROR	= 0x02;
        const byte NFC_TAG_ON_TYPE = 0x03;
        const byte NFC_TAG_ON_PARSED = 0x04;
        const byte NFC_TAG_ON_DATA = 0x05;

        //Error Literals 
        public const byte INDEX_OUT_OF_BOUNDS = 0x00;
        public const byte RECORD_CAN_NOT_BE_PARSED	= 0x01;
        public const byte TAG_NOT_SUPPORTED = 0x02;
        public const byte NO_ENOUGH_BYTES = 0x03;
        public const byte TAG_READING_ERROR = 0x04;
        public const byte RECORD_NOT_FOUND = 0x05;
    }
}
