using System.Threading;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleIOT
{
    public class IOT : OneSheeldUser, IOneSheeldSketch, IIOTStrStrCallback, IIOTconnectionStatusCallback, IIOTerrorCallback
    {
        /* Set your host name */
        const string HOST_NAME = "test.mosquitto.org";

        /* LED Pin */
        OutputPort ledPin = null;
        /* Subscribe to topic 1Sheeld/MyArduino/led . */
        const string myTopic = "1Sheeld/MyArduino/led";

        public void Setup()
        {
            /* Start communication. */
            OneSheeld.begin();
            /* Setup LED Pin on 13 */
            ledPin = new OutputPort(Pins.GPIO_PIN_D13, false);
            /* Disconnect from broker. */
            IOT.disconnect();
            /* Reset all connection variables to default */
            IOT.resetConnectionParametersToDefaults();
            /* Connect to mosquitto's public broker. */
            IOT.connect(HOST_NAME);
            /* Subscribe to new messages. */
            IOT.setNewMessageStrStr(this);
            /* Subscribe to connnection status callback. */
            IOT.setConnectionStatus(this);
            /* Subscribe to error callback. */
            IOT.setOnError(this);
            /* Some time for app to connect. */
            Thread.Sleep(3000);
            /* Subscribe to led topic. */
            IOT.subscribe(myTopic);
        }

        public void Loop()
        { }

        public void OnsetNewMessageStrStr(string incomingTopic, string payload, byte qos, bool retained)
        {
            /* Check on incomingTopic. */
            if (myTopic.Equals(incomingTopic))
            {
                /* If payload states ON. */
                if (payload.Equals("ON"))
                {
                    /* Turn on the led. */
                    ledPin.Write(true);
                }
                /* If payload states OFF. */
                else if (payload.Equals("OFF"))
                {
                    /* Turn off the led. */
                    ledPin.Write(false);
                }
            }
        }

        public void OnConnectionStatus(byte statusCode)
        {
            /* Check connection code and display. */
            switch (statusCode)
            {
                case CONNECTION_SUCCESSFUL: TERMINAL.println("CONNECTION_SUCCESSFUL"); break;
                case CONNECTION_FAILED: TERMINAL.println("CONNECTION_FAILED"); break;
                case CONNECTION_LOST: TERMINAL.println("CONNECTION_LOST"); break;
                case CONNECTION_LOST_RECONNECTING: TERMINAL.println("CONNECTION_LOST_RECONNECTING"); break;
                case NOT_CONNECTED_YET: TERMINAL.println("NOT_CONNECTED_YET"); break;
                case MISSING_HOST: TERMINAL.println("MISSING_HOST"); break;
            }
        }

        public void OnError(byte errorCode)
        {
            /* Check error code and display. */
            switch (errorCode)
            {
                case CONNECTION_REFUSED: TERMINAL.println("CONNECTION_REFUSED"); break;
                case ILLEGAL_MESSAGE_RECEIVED: TERMINAL.println("ILLEGAL_MESSAGE_RECEIVED"); break;
                case DROPPING_OUT_GOING_MESSAGE: TERMINAL.println("DROPPING_OUT_GOING_MESSAGE"); break;
                case ENCODER_NOT_READY: TERMINAL.println("ENCODER_NOT_READY"); break;
                case INVALID_CONNACK_RECEIVED: TERMINAL.println("INVALID_CONNACK_RECEIVED"); break;
                case NO_CONNACK_RECEIVED: TERMINAL.println("NO_CONNACK_RECEIVED"); break;
                case CONNACK_UNACCEPTABLEP_ROTOCOLVERSION: TERMINAL.println("CONNACK_UNACCEPTABLEP_ROTOCOLVERSION"); break;
                case CONNACK_IDENTIFIER_REJECTED: TERMINAL.println("CONNACK_IDENTIFIER_REJECTED"); break;
                case CONNACK_SERVER_UNAVAILABLE: TERMINAL.println("CONNACK_SERVER_UNAVAILABLE"); break;
                case CONNACK_AUTHENTICATION_FAILED: TERMINAL.println("CONNACK_AUTHENTICATION_FAILED"); break;
                case CONNACK_NOT_AUTHORIZED: TERMINAL.println("CONNACK_NOT_AUTHORIZED"); break;
                case CONNACK_RESERVED: TERMINAL.println("CONNACK_RESERVED"); break;
            }
        }

        //Connection codes literals
        const byte CONNECTION_SUCCESSFUL = 0x01;
        const byte CONNECTION_FAILED = 0x02;
        const byte CONNECTION_LOST = 0x03;
        const byte CONNECTION_LOST_RECONNECTING = 0x04;
        const byte NOT_CONNECTED_YET = 0x05;
        const byte MISSING_HOST = 0x06;

        // Error literals
        const byte CONNECTION_REFUSED = 0x00;
        const byte ILLEGAL_MESSAGE_RECEIVED = 0x01;
        const byte DROPPING_OUT_GOING_MESSAGE = 0x02;
        const byte ENCODER_NOT_READY = 0x03;
        const byte INVALID_CONNACK_RECEIVED = 0x04;
        const byte NO_CONNACK_RECEIVED = 0x05;
        const byte CONNACK_UNACCEPTABLEP_ROTOCOLVERSION = 0x06;
        const byte CONNACK_IDENTIFIER_REJECTED = 0x07;
        const byte CONNACK_SERVER_UNAVAILABLE = 0x08;
        const byte CONNACK_AUTHENTICATION_FAILED = 0x09;
        const byte CONNACK_NOT_AUTHORIZED = 0x0A;
        const byte CONNACK_RESERVED = 0x0B;
    }
}
