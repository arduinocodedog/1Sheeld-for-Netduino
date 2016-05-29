namespace OneSheeldClasses
{
    public class NFCRecord
    {
        bool isRecordNull = false;
        public byte recordType = 0;
        public int recordTypeLength = 0;
        public int recordDataLength = 0;
        byte recordNumber = 0;

        public NFCRecord(byte _recordNumber)
        {
            recordNumber = _recordNumber;
        }

	    public NFCRecord()
        {
            isRecordNull = true;
        }

        public bool isNull()
        {
	        return isRecordNull;
        }

        public byte getTypeCategory()
        {
	        return recordType;
        }
	
        public int getTypeLength()
        {
	        return recordTypeLength;
        }

        public int getDataLength()
        {
	        return recordDataLength;
        }

        public void queryData(int start, byte size)
        {
            if (!isRecordNull)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(recordNumber);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(start);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(size);
                args.Add(arg3);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.NFC_ID, 0, NFC_RECORD_QUERY_DATA, 3, args);
            }
        }

        public void queryType(int start,byte size)
        {
            if (!isRecordNull)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(recordNumber);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(start);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(size);
                args.Add(arg3);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.NFC_ID, 0, NFC_RECORD_QUERY_TYPE, 3, args);
            }
        }

        public void queryParsedData()
        {
            if (!isRecordNull)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(recordNumber);
                args.Add(arg1);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.NFC_ID, 0, NFC_RECORD_QUERY_PARSED_DATA, 1, args);
            }
        }

        //Output Function ID
        const byte NFC_RECORD_QUERY_DATA = 0x01;
        const byte NFC_RECORD_QUERY_TYPE = 0x02;
        const byte NFC_RECORD_QUERY_PARSED_DATA = 0x03;

        //Literals
        public const byte TNF_UNKNOWN = 0x00;
        public const byte TNF_EMPTY = 0x01;
        public const byte TNF_EXTERNAL_TYPE = 0x02;
        public const byte TNF_MIME_MEDIA = 0x03;
        public const byte TNF_UNCHANGED = 0x04;
        public const byte TNF_ABSOLUTE_URI = 0x05;
        public const byte RTD_TEXT = 0x06;
        public const byte RTD_URI = 0x07;
        public const byte RTD_UNSUPPORTED = 0x08;
    }
}
