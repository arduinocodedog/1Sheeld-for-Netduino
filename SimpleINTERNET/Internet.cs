using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleINTERNET
{
    public class Internet : 
        IHttpRequestSuccessCallback, 
        IHttpRequestFailureCallback, 
        IHttpRequestStartCallback, 
        IHttpRequestFinishCallback,
        IHttpResponseNextResponseBytesCallback,
        IHttpResponseErrorCallback,
        IInternetErrorCallback
    {
        static OneSheeld sheeld = new OneSheeld();
        
        HttpRequest oneSheeldRequest = null;

        OutputPort led = null;

        public void Setup()
        {
            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            sheeld.begin();
            sheeld.waitForAppConnection();

            Thread.Sleep(5000); 

            oneSheeldRequest = new HttpRequest(sheeld, "http://www.1sheeld.com/");
            oneSheeldRequest.setOnSuccess(this);
            oneSheeldRequest.setOnFailure(this);
            oneSheeldRequest.setOnStart(this);
            oneSheeldRequest.setOnFinish(this);
            oneSheeldRequest.getResponse().setOnNextResponseBytesUpdate(this);
            oneSheeldRequest.getResponse().setOnError(this);

            OneSheeld.INTERNET.setOnError(this);
            OneSheeld.INTERNET.performGet(oneSheeldRequest);
        }

        public void Loop() { }
    
        public void OnSuccess(HttpResponse response)
        {
            OneSheeld.TERMINAL.println(response.getStatusCode());
            response.getNextBytes();
        }

        public void OnFailure(HttpResponse response)
        {
            OneSheeld.TERMINAL.println(response.getStatusCode());
            OneSheeld.TERMINAL.println(response.getBytes());
        }

        public void OnStart()
        {
            led.Write(true);
        }

        public void OnFinish()
        {
            led.Write(false);
        }

        public void OnNextResponseBytesUpdate(HttpResponse response)
        {
            OneSheeld.TERMINAL.println(response.getBytes());

            if (!response.isSentFully())
            {
                response.getNextBytes();
            }
        }

        const byte INDEX_OUT_OF_BOUNDS = 0x00;
        const byte RESPONSE_CAN_NOT_BE_FOUND = 0x01;
        const byte HEADER_CAN_NOT_BE_FOUND = 0x02;
        const byte NO_ENOUGH_BYTES = 0x03;
        const byte REQUEST_HAS_NO_RESPONSE = 0x04;
        const byte SIZE_OF_REQUEST_CAN_NOT_BE_ZERO = 0x05;
        const byte UNSUPPORTED_HTTP_ENTITY = 0x06;
        const byte JSON_KEYCHAIN_IS_WRONG = 0x07;

        public void OnError(int errorNumber)
        {
            OneSheeld.TERMINAL.print("Error:");
            switch (errorNumber)
            {
                case INDEX_OUT_OF_BOUNDS: OneSheeld.TERMINAL.println("INDEX_OUT_OF_BOUNDS"); break;
                case RESPONSE_CAN_NOT_BE_FOUND: OneSheeld.TERMINAL.println("RESPONSE_CAN_NOT_BE_FOUND"); break;
                case HEADER_CAN_NOT_BE_FOUND: OneSheeld.TERMINAL.println("HEADER_CAN_NOT_BE_FOUND"); break;
                case NO_ENOUGH_BYTES: OneSheeld.TERMINAL.println("NO_ENOUGH_BYTES"); break;
                case REQUEST_HAS_NO_RESPONSE: OneSheeld.TERMINAL.println("REQUEST_HAS_NO_RESPONSE"); break;
                case SIZE_OF_REQUEST_CAN_NOT_BE_ZERO: OneSheeld.TERMINAL.println("SIZE_OF_REQUEST_CAN_NOT_BE_ZERO"); break;
                case UNSUPPORTED_HTTP_ENTITY: OneSheeld.TERMINAL.println("UNSUPPORTED_HTTP_ENTITY"); break;
                case JSON_KEYCHAIN_IS_WRONG: OneSheeld.TERMINAL.println("JSON_KEYCHAIN_IS_WRONG"); break;
            }
        }

        const byte REQUEST_CAN_NOT_BE_FOUND = 0x00;
        const byte NOT_CONNECTED_TO_NETWORK = 0x01;
        const byte URL_IS_NOT_FOUND = 0x02;
        const byte ALREADY_EXECUTING_REQUEST = 0x03;
        const byte URL_IS_WRONG = 0x04;

        public void OnError(int requestId, int errorNumber)
        {
            /* Print out error Number.*/
            OneSheeld.TERMINAL.print("Request id:");
            OneSheeld.TERMINAL.println(requestId);
            OneSheeld.TERMINAL.print("Internet error:");
            switch (errorNumber)
            {
                case REQUEST_CAN_NOT_BE_FOUND: OneSheeld.TERMINAL.println("REQUEST_CAN_NOT_BE_FOUND"); break;
                case NOT_CONNECTED_TO_NETWORK: OneSheeld.TERMINAL.println("NOT_CONNECTED_TO_NETWORK"); break;
                case URL_IS_NOT_FOUND: OneSheeld.TERMINAL.println("URL_IS_NOT_FOUND"); break;
                case ALREADY_EXECUTING_REQUEST: OneSheeld.TERMINAL.println("ALREADY_EXECUTING_REQUEST"); break;
                case URL_IS_WRONG: OneSheeld.TERMINAL.println("URL_IS_WRONG"); break;
            }

        }
    }
}