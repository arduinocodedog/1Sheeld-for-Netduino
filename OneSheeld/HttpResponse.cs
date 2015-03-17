using System;
<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> origin/master
using Microsoft.SPOT;

namespace OneSheeldClasses
{
<<<<<<< HEAD
    public class HttpResponse
    {
        OneSheeld Sheeld = null;
        public int requestId = 0;
        public bool isDisposedTriggered = false;
        public int statusCode = 0;
        public ulong totalBytesCount = 0;
        public byte[] bytes = null;
        public int bytesCount = 0;
        public bool isInit = false;
        public ulong index = 0;
        public int callbacksRequested = 0;

        public IHttpResponseNextResponseBytesCallback getNextCallBack = null;
        public IHttpResponseErrorCallback getErrorCallBack = null;
        public IHttpResponseJsonResponseCallback getJsonCallBack = null;
        public IHttpJsonArrayLengthResponseCallback getJsonArrayLengthCallBack = null;
        public IHttpResponseGetHeaderCallback getHeaderCallBack = null;

        public HttpResponse()
        {
            isInit = false;
        }

        public void SetOneSheeld(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
        }

        ~HttpResponse()
        {
            if (isInit && bytesCount != 0)
                bytes = null;
        }

        public int getStatusCode()
        {
	        return statusCode;
        }

        public int getBytesCount()
        {
	        return bytesCount;
        }

        public byte[] getBytes()
        {
	        return bytes;
        }

        public ulong getTotalBytesCount()
        {
	        return totalBytesCount;
        }

        public ulong getCurrentIndex()
        {
	        return index;
        }

        public void getTheseBytes(ulong start,int size)
        {
	        if(isInit)
	        {
		        index=start;

                ArrayList args = new ArrayList();

                byte[] reqId = new byte[2];
                reqId[1] = (byte)((requestId >> 8) & 0xFF);
                reqId[0] = (byte)(requestId & 0xFF);

                FunctionArg arg1 = new FunctionArg(2, reqId);
                args.Add(arg1);

                byte[] startArray = new byte[4];
	  	        startArray[0] = (byte)(start & 0xFF);
	  	        startArray[1] = (byte)((start >> 8) & 0xFF);
	  	        startArray[2] = (byte)((start >> 16) & 0xFF);
	  	        startArray[3] = (byte)((start >> 24) & 0xFF);

                FunctionArg arg2 = new FunctionArg(4, startArray);
                args.Add(arg2);

                byte[] sizeArray = new byte[2];
	  	        sizeArray[1] = (byte)((size >> 8) & 0xFF);
	  	        sizeArray[0] = (byte)(size & 0xFF);

                FunctionArg arg3 = new FunctionArg(2, sizeArray);
                args.Add(arg3);

                Sheeld.sendPacket(ShieldIds.INTERNET_ID, 0, RESPONSE_GET_NEXT_BYTES, 3, args);
    	    }
        }

        public void getNextBytes(int size = 64)
        {
	        getTheseBytes(index,size);
        }

        public void setOnNextResponseBytesUpdate(IHttpResponseNextResponseBytesCallback userCallback)
        {
	        callbacksRequested |= RESPONSE_GET_NEXT_RESPONSE_BIT;
	        getNextCallBack = userCallback;
        }

        public void setOnError(IHttpResponseErrorCallback userCallback)
        {
	        callbacksRequested |= RESPONSE_GET_ERROR_BIT;
	        getErrorCallBack = userCallback;
        }

        public void setOnJsonResponse(IHttpResponseJsonResponseCallback userCallback)
        {
	        callbacksRequested |= RESPONSE_GET_JSON_BIT;
	        getJsonCallBack = userCallback;
        }

        public void setOnJsonArrayLengthResponse(IHttpJsonArrayLengthResponseCallback userCallback)
        {
	        callbacksRequested |= RESPONSE_GET_JSON_ARRAY_LENGTH_BIT;
	        getJsonArrayLengthCallBack = userCallback;
        }

        public bool isSentFully()
        {
	        return (bool)(index>=totalBytesCount);
        }

        public void dispose(bool sendFrame)
        {
	        isDisposedTriggered = true;
	        if(isInit && bytesCount!=0 && bytes!=null)
	        {
		        bytes =  null;
	        }
	        isInit = false;
	        bytes = null;
	        index = 0;
	        bytesCount = 0;
	        statusCode = 0;
	        totalBytesCount = 0;

            ArrayList args = new ArrayList();

            byte[] reqId = new byte[2];
            reqId[1] = (byte)((requestId >> 8) & 0xFF);
            reqId[0] = (byte)(requestId & 0xFF);

            FunctionArg arg1 = new FunctionArg(2, reqId);
            args.Add(arg1);
	
	        if(sendFrame)
	        {
		        Sheeld.sendPacket(ShieldIds.INTERNET_ID,0,RESPONSE_DISPOSE,1,args);
		        callbacksRequested=0;
	        }
        }

        public bool isDisposed()
        {
	        return isDisposedTriggered;
        }

        public void resetIndex(ulong x)
        {
	        index = x;
        }

        public void getHeader(string headerName, IHttpResponseGetHeaderCallback userCallback)
        {
	        if(isInit)
	        {
		        //Check length of string 
		        if(headerName.Length == 0) 
                    return;
		        callbacksRequested |= RESPONSE_INPUT_GET_HEADER_BIT;
		        getHeaderCallBack = userCallback;

                ArrayList args = new ArrayList();

                FunctionArg arg1 = new FunctionArg(headerName.Length, System.Text.Encoding.UTF8.GetBytes(headerName));
                args.Add(arg1);

		        Sheeld.sendPacket(ShieldIds.INTERNET_ID,0,RESPONSE_INPUT_GET_HEADER,1,args);	
	        }
	
        }

        public JsonKeyChain AddKeytoChain(int key)
        {
            JsonKeyChain chain = new JsonKeyChain(Sheeld, requestId);
            return chain.AddKeytoChain(key);
        }

        public JsonKeyChain AddKeytoChain(string key)
        {
            if (key == null)
                return null;

            JsonKeyChain chain = new JsonKeyChain(Sheeld, requestId);
            return chain.AddKeytoChain(key);
        }

        const byte RESPONSE_INPUT_GET_HEADER = 0x07;
        const byte RESPONSE_DISPOSE = 0x11;
        const byte RESPONSE_GET_NEXT_BYTES = 0x12;

        const byte RESPONSE_INPUT_GET_HEADER_BIT = 0x01;
        const byte RESPONSE_GET_ERROR_BIT = 0x02;
        const byte RESPONSE_GET_NEXT_RESPONSE_BIT =	0x04;
        const byte RESPONSE_GET_JSON_BIT = 0x08;
        const byte RESPONSE_GET_JSON_ARRAY_LENGTH_BIT = 0x10;
=======
    class HttpResponse
    {
>>>>>>> origin/master
    }
}
