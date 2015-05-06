using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class NFCTag
    {
        public bool isTypeCallBackAssigned = false;
        public bool isDataCallBackAssigned = false;
        public bool isParsedDataCallBackAssigned = false;
        public IRecordTypeCallback recordTypeCallBack = null;
        public IRecordDataCallback recordDataCallBack = null;
        public IRecordParsedCallback recordParsedCallBack = null;

        byte[] tagId = null;
        int tagSize = 0;
        int tagMaxSize = 0;
        public byte recordsNumber = 0;
        public byte tagIdLength = 0;
        NFCRecord[] recordsArray = null;

        static NFCRecord nullRecord = null;

        public NFCTag(byte[] _tagId, byte tagIdLength, int _tagSize, int _tagMaxSize, byte _recordsNumber)
        {
            nullRecord = new NFCRecord();

            if (tagIdLength != 0)
            {
                tagId = new byte[tagIdLength];
                for (int i = 0; i < tagIdLength; i++)
                    tagId[i] = _tagId[i];
            }

            tagSize = _tagSize;
            tagMaxSize = _tagMaxSize;
            recordsNumber = _recordsNumber;
            if (recordsNumber > 0)
            {
                recordsArray = new NFCRecord[recordsNumber];
                for (int i = 0; i < recordsNumber; i++)
                {
                    recordsArray[i] = new NFCRecord((byte)i);
                }
            }
        }

        public NFCTag()
        {
            nullRecord = new NFCRecord();

            tagSize = 0;
            tagMaxSize = 0;
            recordsNumber = 0;
            recordsArray = null;
        }

        ~NFCTag()
        {
            if (recordsNumber > 0 && recordsArray != null)
            {
                for (int i = 0; i < recordsNumber; i++)
                    recordsArray[i] = null;
                recordsArray = null;
            }

            if (tagId != null)
                tagId = null;
        }

        public NFCRecord getRecord(int index)
        {
            if (index >= recordsNumber)
                return nullRecord;
            return recordsArray[index];
        }

        public int getSize()
        {
            return tagSize;
        }

        public int getMaxSize()
        {
            return tagMaxSize;
        }

        public byte getNumberOfRecords()
        {
            return recordsNumber;
        }

        public bool isEmpty()
        {
            return (recordsNumber == 0);
        }

        public byte getIdLength()
        {
            return tagIdLength;
        }

        public byte[] getId()
        {
            return tagId;
        }

        public void setOnRecordTypeResponse(IRecordTypeCallback userCallback)
        {
            isTypeCallBackAssigned = true;
            recordTypeCallBack = userCallback;
        }

        public void setOnRecordParsedDataResponse(IRecordParsedCallback userCallback)
        {
            isParsedDataCallBackAssigned = true;
            recordParsedCallBack = userCallback;
        }

        public void setOnRecordDataResponse(IRecordDataCallback userCallback)
        {
            isDataCallBackAssigned = true;
            recordDataCallBack = userCallback;
        }

        public bool isNull()
        {
            return tagMaxSize == 0;
        }

        //Input Function ID
        const byte NFC_TAG_ON_TYPE = 0x03;
        const byte NFC_TAG_ON_PARSED = 0x04;
        const byte NFC_TAG_ON_DATA = 0x05;
    }
}
 