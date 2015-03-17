using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedINTERNET
{
    public class Internet :
        IHttpRequestSuccessCallback,
        IHttpResponseJsonResponseCallback,
        IHttpResponseErrorCallback,
        IInternetErrorCallback
    {
        static OneSheeld sheeld = new OneSheeld();

        HttpRequest oneSheeldRequest = null;

        OutputPort red = null;
        OutputPort green = null;
        OutputPort blue = null;

        public void Setup()
        {
            red = new OutputPort(Pins.GPIO_PIN_D8, false);
            green = new OutputPort(Pins.GPIO_PIN_D9, false);
            blue = new OutputPort(Pins.GPIO_PIN_D10, false);

            sheeld.begin();
            sheeld.waitForAppConnection();

            Thread.Sleep(5000);

            oneSheeldRequest = new HttpRequest(sheeld, "http://api.openweathermap.org/data/2.5/weather");
            oneSheeldRequest.setOnSuccess(this);
            oneSheeldRequest.response.setOnJsonResponse(this);
            oneSheeldRequest.response.setOnError(this);

            OneSheeld.INTERNET.setOnError(this);
        }

        public void Loop() 
        { 
            if (OneSheeld.VOICERECOGNITION.isNewCommandReceived())
            {
                oneSheeldRequest.addParameter("q", OneSheeld.VOICERECOGNITION.getLastCommand());
                OneSheeld.INTERNET.performGet(oneSheeldRequest);
                //sheeld.delay(5000);
            }
        }

        public void OnSuccess(HttpResponse response)
        {
            response.AddKeytoChain("weather").AddKeytoChain(0).AddKeytoChain("main").query();
        }

        public void OnJsonResponse(JsonKeyChain chain, byte[] data)
        {
            OneSheeld.TTS.say(data);
            if (data.Equals("Clouds"))
                blueRGB();
            if (data.Equals("Sand"))
                redRGB();
            if (data.Equals("Snow"))
                whiteRGB();
            if (data.Equals("Clear"))
                yellowRGB();

        }

        void yellowRGB()
        {
            red.Write(true);
            green.Write(true);
            blue.Write(false);
        }

        void blueRGB()
        {
            red.Write(false);
            green.Write(false);
            blue.Write(true);
        }

        void whiteRGB()
        {
            red.Write(true);
            green.Write(true);
            blue.Write(true);
        }

        void redRGB()
        {
            red.Write(true);
            green.Write(false);
            blue.Write(false);
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
