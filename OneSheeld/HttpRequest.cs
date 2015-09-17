using System.Collections;

namespace OneSheeldClasses
{
    public class HttpRequest
    {
        public IHttpRequestSuccessCallback successCallback = null;
        public IHttpRequestFailureCallback failureCallback = null;
        public IHttpRequestStartCallback startCallback = null;
        public IHttpRequestFinishCallback finishCallback = null;

        string url = null;
        bool isInitFrameSent = false;
    	public byte[] localRequestId = new byte[2];
        public byte callbacksRequested = 0;
        public HttpResponse response = new HttpResponse();
	    static int totalRequests = 0;

        public HttpRequest(string _url)
        {
            int reqId = ++HttpRequest.totalRequests;
            localRequestId[0] = (byte)(reqId & 0xFF);
	        localRequestId[1] = (byte)((reqId >> 8) & 0xFF);
	        callbacksRequested = 0;
	        url = null;
	        response.requestId = getId();
	        if(_url.Length == 0) return;
	        if(!OneSheeldClass.isInitialized)
	        {
                url = _url;
		        OneSheeldClass.addToUnSentRequestsArray(this);
		        isInitFrameSent=false;
	        }
	        else
	        {
		        sendInitFrame(_url);
	        }
        }

	    ~HttpRequest()
        {
            for (int i = 0; i < MAX_NO_OF_REQUESTS; i++)
            {
                if (OneSheeldMain._INTERNET.requestsArray[i] == this)
                {
                    OneSheeldMain._INTERNET.requestsArray[i] = null;
                    return;
                }
            }
        }

        public void sendInitFrame()
        {
            if (!isInitFrameSent)
            {
                sendInitFrame(url);
                url = null;
            }
        }

        void sendInitFrame(string _url)
        {
            //Check length of string 
            if (_url.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(_url.Length, System.Text.Encoding.UTF8.GetBytes(_url));
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_REQUEST_URL, 2, args);

            isInitFrameSent = true;
        }

        public void setUrl(string urlName)
        {
            if (urlName.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(urlName.Length, System.Text.Encoding.UTF8.GetBytes(urlName));
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_SET_URL, 2, args);
        }
        
        public void addHeader(string headername, string data)
        {
            if (headername.Length == 0 || data.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(headername.Length, System.Text.Encoding.UTF8.GetBytes(headername));
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(data.Length, System.Text.Encoding.UTF8.GetBytes(data));
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_ADD_HEADER, 3, args);
        }

        public void addParameter(string paramname, string data)
        {
            if (paramname.Length == 0 || data.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(paramname.Length, System.Text.Encoding.UTF8.GetBytes(paramname));
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(data.Length, System.Text.Encoding.UTF8.GetBytes(data));
            args.Add(arg3);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_ADD_PARAMETER, 3, args);
        }

        public void addRawData(string data)
        {
            if (data.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(data.Length, System.Text.Encoding.UTF8.GetBytes(data));
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_ADD_RAW_DATA, 2, args);
        }

        public int getId()
        {
            return (localRequestId[1] << 8) | localRequestId[0];
        }

        public void deleteHeaders()
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_DELETE_HEADER, 1, args);
        }

        public void deleteParameters()
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_DELETE_PARAMETER, 1, args);
        }

        public void deleteCallBacks()
        {
            callbacksRequested = 0;
        }

        public void setContentType(string contenttype)
        {
            if (contenttype.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(contenttype.Length, System.Text.Encoding.UTF8.GetBytes(contenttype));
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_SET_CONTENT_TYPE, 2, args);
        }

        public void setParametersContentEncoding(string contentEncoding)
        {
            if (contentEncoding.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(contentEncoding.Length, System.Text.Encoding.UTF8.GetBytes(contentEncoding));
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_SET_CONTENT_ENCODING, 2, args);
        }

        public void ignoreResponse()
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, localRequestId);
            args.Add(arg1);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, HTTP_IGNORE_REQUEST, 1, args);
        }

        public void setOnSuccess(IHttpRequestSuccessCallback callBack)
        {
            callbacksRequested |= SUCCESS_CALLBACK_BIT;
            successCallback = callBack;
        }

        public void setOnFailure(IHttpRequestFailureCallback callBack)
        {
            callbacksRequested |= FAILURE_CALLBACK_BIT;
            failureCallback = callBack;
        }

        public void setOnStart(IHttpRequestStartCallback callBack)
        {
            callbacksRequested |= START_CALLBACK_BIT;
            startCallback = callBack;
        }

        public void setOnFinish(IHttpRequestFinishCallback callBack)
        {
            callbacksRequested |= FINISH_CALLBACK_BIT;
            finishCallback = callBack;
        }

        public HttpResponse getResponse()
        {
            return response;
        }

        const byte HTTP_REQUEST_URL = 0x01;
        const byte HTTP_SET_URL = 0x02;
        const byte HTTP_ADD_HEADER = 0x03;
        const byte HTTP_ADD_PARAMETER = 0x04;
        const byte HTTP_ADD_RAW_DATA = 0x15;
        const byte HTTP_DELETE_HEADER = 0x05;
        const byte HTTP_DELETE_PARAMETER = 0x06;
        const byte HTTP_SET_CONTENT_TYPE = 0x07;
        const byte HTTP_IGNORE_REQUEST = 0x08;
        const byte HTTP_SET_CONTENT_ENCODING = 0x16;

        const byte SUCCESS_CALLBACK_BIT = 0x01;
        const byte FAILURE_CALLBACK_BIT	= 0x02;
        const byte START_CALLBACK_BIT = 0x04;
        const byte FINISH_CALLBACK_BIT = 0x08;

        const int MAX_NO_OF_REQUESTS = 20;
    }
}
