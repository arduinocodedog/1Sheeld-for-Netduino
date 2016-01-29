using System.Collections;

namespace OneSheeldClasses
{
    public class HttpResponse 
    {
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
        public IHttpJsonResponseCallback getJsonCallBack = null;
        public IHttpJsonArrayLengthResponseCallback getJsonArrayLengthCallBack = null;
        public IHttpResponseGetHeaderCallback getHeaderCallBack = null;

        public HttpResponse()
        {
            isInit = false;
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

                FunctionArg arg1 = new FunctionArg(requestId);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(start);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(size);
                args.Add(arg3);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, RESPONSE_GET_NEXT_BYTES, 3, args);
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

        public void setOnJsonResponse(IHttpJsonResponseCallback userCallback)
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

	        if(sendFrame)
	        {
                ArrayList args = new ArrayList();

                FunctionArg arg1 = new FunctionArg(requestId);
                args.Add(arg1);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID,0,RESPONSE_DISPOSE,1,args);
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

                FunctionArg arg1 = new FunctionArg(headerName);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(requestId);
                args.Add(arg2);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID,0,RESPONSE_GET_HEADER,2,args);	
	        }
	
        }

        public JsonKeyChain this[int key]
        {
            get { return AddKeytoChain(key); }
            set { }
        }

        public JsonKeyChain this[string key]
        {
            get { return AddKeytoChain(key); }
            set { }
        }

        public JsonKeyChain AddKeytoChain(int key)
        {
            JsonKeyChain chain = new JsonKeyChain(requestId);
            return chain.AddKeytoChain(key);
        }

        public JsonKeyChain AddKeytoChain(string key)
        {
            if (key == null)
                return null;

            JsonKeyChain chain = new JsonKeyChain(requestId);
            return chain.AddKeytoChain(key);
        }

        const byte RESPONSE_DISPOSE = 0x11;
        const byte RESPONSE_GET_NEXT_BYTES = 0x12;
        const byte RESPONSE_GET_HEADER = 0x13;

        const byte RESPONSE_INPUT_GET_HEADER_BIT = 0x01;
        const byte RESPONSE_GET_ERROR_BIT = 0x02;
        const byte RESPONSE_GET_NEXT_RESPONSE_BIT =	0x04;
        const byte RESPONSE_GET_JSON_BIT = 0x08;
        const byte RESPONSE_GET_JSON_ARRAY_LENGTH_BIT = 0x10;
    }
}
