using System.Collections;

namespace OneSheeldClasses
{
    public class InternetShield : ShieldParent 
    {
        bool isSetOnErrorCallBackAssigned = false;
        IInternetErrorCallback internetErrorCallBack = null;

        public HttpRequest[] requestsArray = new HttpRequest[MAX_NO_OF_REQUESTS];

        public InternetShield()
            :base(ShieldIds.INTERNET_ID)
        {
            for(int i = 0; i < MAX_NO_OF_REQUESTS; i++)
		        requestsArray[i] = null;
        }

        public bool performGet(HttpRequest request)
        {
            bool isAdded = addToRequestsArray(request);
            if (isAdded)
            {
                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_GET, 2, GetRequestArgs(request));
            }

            return isAdded;
        }

    	public bool performPost(HttpRequest request)
        {
            bool isAdded = addToRequestsArray(request);
            if (isAdded)
            {
                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_POST, 2, GetRequestArgs(request));
            }

            return isAdded;
        }

	    public bool performPut(HttpRequest request)
        {
            bool isAdded = addToRequestsArray(request);
            if (isAdded)
            {
                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_PUT, 2, GetRequestArgs(request));
            }

            return isAdded;
        }

	    public bool performDelete(HttpRequest request)
        {
            bool isAdded = addToRequestsArray(request);
            if (isAdded)
            {
                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_DELETE, 2, GetRequestArgs(request));
            }

            return isAdded;
        }

	    public void cancelAllRequests()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_CANCEL_ALL_REQUESTS);
        }

	    public void ignoreResponse(HttpRequest request)
        {
            request.ignoreResponse();
        }

        public void setBasicAuthentication(string userName, string password)
        {
            if (userName.Length == 0 || password.Length == 0)
                return;

            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(userName.Length, System.Text.Encoding.UTF8.GetBytes(userName));
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(password.Length, System.Text.Encoding.UTF8.GetBytes(password));
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_SET_AUTHENTICATION, 2, args);
        }

	    public void clearBasicAuthentication()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_CLEAR_AUTHENTICATION);
        }

	    public void setIntialResponseMaxBytesCount(int size)
        {
            ArrayList args = new ArrayList();

            byte[] sizeArray = new byte[2];
  	        sizeArray[1] = (byte)((size >> 8) & 0xFF);
  	        sizeArray[0] = (byte)(size & 0xFF);

            FunctionArg arg = new FunctionArg(2, sizeArray);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, INTERNET_SET_DEFAULT_MAX_RESPONSE, 1, args);
        }

        public override void processData()
        {
            byte functionId = getOneSheeldInstance().getFunctionId();

            if (functionId == HTTP_GET_SUCCESS || functionId == HTTP_GET_FAILURE || functionId == HTTP_GET_STARTED ||
                functionId == HTTP_GET_ON_FINISH || functionId == RESPONSE_GET_NEXT_RESPONSE || functionId == RESPONSE_GET_ERROR ||
                functionId == RESPONSE_INPUT_GET_HEADER || functionId == RESPONSE_GET_JSON || functionId == RESPONSE_GET_JSON_ARRAY_LENGTH)
            {
                int requestId = getOneSheeldInstance().getArgumentData(0)[0] | (getOneSheeldInstance().getArgumentData(0)[1] << 8);
                ulong totalBytesCount = 0;
                int statusCode = 0;
                int error = 0;

                if (functionId == HTTP_GET_SUCCESS || functionId == HTTP_GET_FAILURE)
                {
                    statusCode = getOneSheeldInstance().getArgumentData(1)[0] | (getOneSheeldInstance().getArgumentData(1)[1] << 8);
                    totalBytesCount = (ulong)(getOneSheeldInstance().getArgumentData(2)[0] |
                                              (getOneSheeldInstance().getArgumentData(2)[1] << 8) |
                                              (getOneSheeldInstance().getArgumentData(2)[2] << 16) |
                                              (getOneSheeldInstance().getArgumentData(2)[3] << 24));
                }
                else if (functionId == RESPONSE_GET_ERROR)
                    error = getOneSheeldInstance().getArgumentData(1)[0];

                byte[] data = null;
                int dataLength = 0;
                int i = 0;
                for (i = 0; i < MAX_NO_OF_REQUESTS; i++)
                {
                    if (requestsArray[i] != null && requestsArray[i].getId() == requestId)
                    {
                        if (((requestsArray[i].callbacksRequested & SUCCESS_CALLBACK_BIT) != 0 && functionId == HTTP_GET_SUCCESS) ||
                            ((requestsArray[i].callbacksRequested & FAILURE_CALLBACK_BIT) != 0 && functionId == HTTP_GET_FAILURE) ||
                            ((requestsArray[i].callbacksRequested & RESPONSE_GET_NEXT_RESPONSE_BIT) != 0 && functionId == RESPONSE_GET_NEXT_RESPONSE))
                        {
                            byte dataArgumentNumber = 0;
                            if (functionId == RESPONSE_GET_NEXT_RESPONSE)
                            {
                                dataArgumentNumber = 1;
                                if (requestsArray[i].response.bytesCount != 0 && requestsArray[i].response.isInit && requestsArray[i].response.bytes != null)
                                {
                                    requestsArray[i].response.bytes = null;
                                }
                            }
                            else
                            {
                                dataArgumentNumber = 3;
                                requestsArray[i].response.dispose(false);
                            }

                            if ((functionId == RESPONSE_GET_NEXT_RESPONSE && requestsArray[i].response.isInit) ||
                                ((functionId == HTTP_GET_SUCCESS || functionId == HTTP_GET_FAILURE) && totalBytesCount != 0))
                            {
                                dataLength = getOneSheeldInstance().getArgumentLength(dataArgumentNumber);
                                if (dataLength != 0)
                                {
                                    data = new byte[dataLength + 1];
                                    for (int j = 0; j < dataLength; j++)
                                        data[j] = getOneSheeldInstance().getArgumentData(dataArgumentNumber)[j];
                                    data[dataLength] = 0x00;
                                    requestsArray[i].response.bytesCount = dataLength;
                                    requestsArray[i].response.index += (ulong)dataLength;
                                    requestsArray[i].response.bytes = data;
                                }
                                else
                                {
                                    requestsArray[i].response.bytesCount = 0;
                                    requestsArray[i].response.bytes = null;
                                }
                            }

                            if (functionId != RESPONSE_GET_NEXT_RESPONSE)
                            {
                                requestsArray[i].response.isInit = true;
                                requestsArray[i].response.isDisposedTriggered = false;
                                requestsArray[i].response.statusCode = statusCode;
                                requestsArray[i].response.totalBytesCount = totalBytesCount;
                            }

                            if (!isInACallback())
                            {
                                if ((requestsArray[i].callbacksRequested & SUCCESS_CALLBACK_BIT) != 0 && functionId == HTTP_GET_SUCCESS)
                                {
                                    enteringACallback();
                                    requestsArray[i].successCallback.OnSuccess(requestsArray[i].response);
                                    exitingACallback();
                                }
                                else if ((requestsArray[i].callbacksRequested & FAILURE_CALLBACK_BIT) != 0 && functionId == HTTP_GET_FAILURE)
                                {
                                    enteringACallback();
                                    requestsArray[i].failureCallback.OnFailure(requestsArray[i].response);
                                    exitingACallback();
                                }
                                else if ((requestsArray[i].callbacksRequested & RESPONSE_GET_NEXT_RESPONSE_BIT) != 0)
                                {
                                    enteringACallback();
                                    requestsArray[i].response.getNextCallBack.OnNextResponseBytesUpdate(requestsArray[i].response);
                                    exitingACallback();
                                }
                            }
                        }
                        else if (!isInACallback())
                        {
                            if ((requestsArray[i].callbacksRequested & START_CALLBACK_BIT) != 0 && functionId == HTTP_GET_STARTED)
                            {
                                enteringACallback();
                                requestsArray[i].startCallback.OnStart();
                                exitingACallback();
                            }
                            else if ((requestsArray[i].callbacksRequested & FINISH_CALLBACK_BIT) != 0 && functionId == HTTP_GET_ON_FINISH)
                            {
                                enteringACallback();
                                requestsArray[i].finishCallback.OnFinish();
                                exitingACallback();
                            }
                            else if ((requestsArray[i].callbacksRequested & RESPONSE_GET_ERROR_BIT) != 0 && functionId == RESPONSE_GET_ERROR)
                            {
                                enteringACallback();
                                requestsArray[i].response.getErrorCallBack.OnError(error);
                                exitingACallback();
                            }
                            else if ((requestsArray[i].callbacksRequested & RESPONSE_INPUT_GET_HEADER_BIT) != 0 && functionId == RESPONSE_INPUT_GET_HEADER)
                            {
                                byte headerNameLength = getOneSheeldInstance().getArgumentLength(1);
                                byte[] headerName = new byte[headerNameLength + 1];
                                for (int k = 0; k < headerNameLength; k++)
                                    headerName[k] = getOneSheeldInstance().getArgumentData(1)[k];
                                headerName[headerNameLength] = 0x00;

                                byte headerValueLength = getOneSheeldInstance().getArgumentLength(2);
                                byte[] headerValue = new byte[headerValueLength + 1];
                                for (int j = 0; j < headerValueLength; j++)
                                    headerValue[j] = getOneSheeldInstance().getArgumentData(2)[j];
                                headerValue[headerValueLength] = 0x00;

                                enteringACallback();
                                requestsArray[i].response.getHeaderCallBack.OnGetHeader(headerName, headerValue);
                                exitingACallback();
                            }
                            else if (functionId == RESPONSE_GET_JSON || functionId == RESPONSE_GET_JSON_ARRAY_LENGTH)
                            {
                                int keyChainTypes = getOneSheeldInstance().getArgumentData(2)[0] | (getOneSheeldInstance().getArgumentData(2)[1] << 8);

                                byte argumentNo = getOneSheeldInstance().getArgumentNo();
                                if (argumentNo - 3 <= MAX_JSON_KEY_DEPTH)
                                {
                                    JsonKeyChain responseJsonChain = new JsonKeyChain();
                                    for (byte j = 3; j < argumentNo; j++)
                                    {
                                        if ((keyChainTypes & (1 << (j - 3))) != 0)
                                        {
                                            byte jsonKeyValueLength = getOneSheeldInstance().getArgumentLength(j);
                                            string jsonKeyValue = "";
                                            for (int k = 0; k < jsonKeyValueLength; k++)
                                                jsonKeyValue += getOneSheeldInstance().getArgumentData(j)[k];
                                            responseJsonChain.AddKeytoChain(jsonKeyValue);
                                        }
                                        else
                                        {
                                            int jsonKeyValue = getOneSheeldInstance().getArgumentData(j)[0] | (getOneSheeldInstance().getArgumentData(j)[1] << 8);
                                            responseJsonChain.AddKeytoChain(jsonKeyValue);
                                        }
                                    }

                                    if ((requestsArray[i].response.callbacksRequested & RESPONSE_GET_JSON_BIT) != 0 && functionId == RESPONSE_GET_JSON)
                                    {
                                        byte jsonResponseLength = getOneSheeldInstance().getArgumentLength(1);

                                        byte[] jsonResponseValue = new byte[jsonResponseLength + 1];
                                        for (int k = 0; k < jsonResponseLength; k++)
                                        {
                                            jsonResponseValue[k] = getOneSheeldInstance().getArgumentData(1)[k];
                                        }
                                        jsonResponseValue[jsonResponseLength] = 0x00;

                                        enteringACallback();
                                        requestsArray[i].response.getJsonCallBack.OnJsonResponse(responseJsonChain, jsonResponseValue);
                                        exitingACallback();
                                    }
                                    else if ((requestsArray[i].response.callbacksRequested & RESPONSE_GET_JSON_ARRAY_LENGTH_BIT) != 0 && functionId == RESPONSE_GET_JSON_ARRAY_LENGTH)
                                    {
                                        ulong arrayLength = (ulong)(getOneSheeldInstance().getArgumentData(1)[0] | (getOneSheeldInstance().getArgumentData(1)[1] << 8) |
                                                                    (getOneSheeldInstance().getArgumentData(1)[2] << 16) | (getOneSheeldInstance().getArgumentData(1)[3] << 24));

                                        enteringACallback();
                                        requestsArray[i].response.getJsonArrayLengthCallBack.OnJsonArrayLengthResponse(responseJsonChain, arrayLength);
                                        exitingACallback();
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else if (functionId == INTERNET_GET_ERROR && isSetOnErrorCallBackAssigned)
            {
                int reqid = getOneSheeldInstance().getArgumentData(0)[0] | (getOneSheeldInstance().getArgumentData(0)[1] << 8);
                int errorNumber = getOneSheeldInstance().getArgumentData(1)[0];

                if (!isInACallback())
                {
                    enteringACallback();
                    internetErrorCallBack.OnError(reqid, errorNumber);
                    exitingACallback();
                }
            }
        }

        public void setOnError(IInternetErrorCallback userCallback)
        {
	        isSetOnErrorCallBackAssigned = true;
	        internetErrorCallBack = userCallback;
        }

        bool addToRequestsArray(HttpRequest request)
        {
            int i;
            if (request.callbacksRequested != 0)
            {
                for (i = 0; i < MAX_NO_OF_REQUESTS; i++)
                {
                    if (requestsArray[i] == request) return true;
                }

                for (i = 0; i < MAX_NO_OF_REQUESTS; i++)
                {
                    if (requestsArray[i] == null) break;
                }

                if (i >= MAX_NO_OF_REQUESTS) return false;
                else
                {
                    requestsArray[i] = request;
                    return true;
                }
            }
            else
            {
                for (i = 0; i < MAX_NO_OF_REQUESTS; i++)
                {
                    if (requestsArray[i] == request) requestsArray[i] = null;
                }
                return true;
            }

        }

        // Netduino Specific
        ArrayList GetRequestArgs(HttpRequest request)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(2, request.localRequestId);
            args.Add(arg1);

            byte[] callbacksRequested = new byte[1];
            callbacksRequested[0] = request.callbacksRequested;
            FunctionArg arg2 = new FunctionArg(1, callbacksRequested);
            args.Add(arg2);

            return args;
        }

        const int MAX_NO_OF_REQUESTS = 20;
        const int MAX_JSON_KEY_DEPTH = 8;

        const byte SUCCESS_CALLBACK_BIT = 0x01;
        const byte FAILURE_CALLBACK_BIT = 0x02;
        const byte START_CALLBACK_BIT =	0x04;
        const byte FINISH_CALLBACK_BIT = 0x08;


        const byte RESPONSE_INPUT_GET_HEADER_BIT = 0x01;
        const byte RESPONSE_GET_ERROR_BIT = 0x02;
        const byte RESPONSE_GET_NEXT_RESPONSE_BIT = 0x04;

        const byte RESPONSE_GET_JSON_BIT = 0x08;
        const byte RESPONSE_GET_JSON_ARRAY_LENGTH_BIT = 0x10;

        // Internet Shield Function Ids;
        const byte INTERNET_GET = 0x09;
        const byte INTERNET_POST = 0x0A;
        const byte INTERNET_PUT	= 0x0B;
        const byte INTERNET_DELETE = 0x0C;
        const byte INTERNET_CANCEL_ALL_REQUESTS = 0x0D;
        const byte INTERNET_SET_AUTHENTICATION = 0x0E;
        const byte INTERNET_CLEAR_AUTHENTICATION = 0x0F;
        const byte INTERNET_SET_DEFAULT_MAX_RESPONSE = 0x10;
        const byte INTERNET_QUERY_JSON = 0x14;
        const byte INTERNET_QUERY_JSON_ARRAY_LENGTH = 0x17;

        // Input Function ID's for Internet Class
        const byte INTERNET_GET_ERROR = 0x06;

        // HTTP Request Function Ids
        const byte HTTP_GET_SUCCESS = 0x01;
        const byte HTTP_GET_FAILURE	= 0x02;
        const byte HTTP_GET_STARTED	= 0x03;
        const byte HTTP_GET_ON_FINISH = 0x05;

        // HTTP Response Function Ids
        const byte RESPONSE_INPUT_GET_HEADER = 0x07;
        const byte RESPONSE_GET_ERROR = 0x08;
        const byte RESPONSE_GET_NEXT_RESPONSE = 0x09;
        const byte RESPONSE_GET_JSON = 0x0A;
        const byte RESPONSE_GET_JSON_ARRAY_LENGTH = 0x0B;
    }
}
