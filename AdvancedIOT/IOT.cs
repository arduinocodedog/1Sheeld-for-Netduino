using Microsoft.SPOT.Hardware;
using OneSheeldClasses;

namespace AdvancedIOT
{
    public class IOT: OneSheeldUser, IOneSheeldSketch, IIOTStrFloatCallback, IIOTconnectionStatusCallback, IIOTerrorCallback
    {
        /* Set your host name. */
        const string HOST_NAME = "hostname";
        /* Set keep alive interval. */
        const byte KEEP_ALIVE_INTERVAL = 10;
        /* Set port. */
        const int PORT = 8883;
        /* Set last will topic. */
        const string LAST_WILL_TOPIC = "1Sheeld/MyArduino/Status";
        /* Set last will payload. */
        const string LAST_WILL_PAYLOAD = "Disconnected";
        /* Set username. */
        const string USER_NAME = "username";
        /* Set password. */
        const string PASSWORD = "password";

        /* Flag to check if already subscribed. */
        bool subscribed = false;
        /* Flag to check if logger not started. */
        bool startedLogging = false;
        /* Sensor Pin as Input */
        AnalogInput sensorPin = null;
        /* Counter for incoming sensor values. */
        int counter;
        /* Subscribe to topic "1Sheeld/MyArduino/RemoteSensor" to get remote sensor values . */
        const string subscribeTopic = "1Sheeld/MyArduino/RemoteSensor";
        /* Pulish sesnor values to topic "1Sheeld/MyArduino/AnalogSensor" . */
        const string publishTopic = "1Sheeld/MyArduino/AnalogSensor";

        public void Setup()
        {
            /* Start communication. */
            OneSheeld.begin();
            /* Set sensorPin on A0. */
            sensorPin = new AnalogInput(Cpu.AnalogChannel.ANALOG_0);
            /* Disconnect from broker. */
            IOT.disconnect();
            /* Reset all connection parameters to default */
            IOT.resetConnectionParametersToDefaults();
            /* Set host name. */
            IOT.setHost(HOST_NAME);
            /* Set port no .*/
            IOT.setPort(PORT);
            /* Set the connection as SSL/TLS. */
            IOT.setSecureConnection(true);
            /* Set keep alive interval. */
            IOT.setKeepAlive(KEEP_ALIVE_INTERVAL);
            /* Set persistent session. */
            IOT.setCleanSession(false);
            /* Set username & password. */
            IOT.setCredentials(USER_NAME, PASSWORD);
            /* Set last will with QOS_0 and as a retained message. */
            IOT.setLastWillAndTestament(LAST_WILL_TOPIC, LAST_WILL_PAYLOAD, QOS_0, true);
            /* Connect to broker. */
            IOT.connect();
            /* Subscribe to new messages. */
            IOT.setNewMessageStrFloat(this);
            /* Subscribe to connnection status callback. */
            IOT.setConnectionStatus(this);
            /* Subscribe to error callback. */
            IOT.setOnError(this);
        }

        public void Loop()
        {
            /* Check if connected to broker. */
            bool connectedToBroker = IOT.isConnected();
            if (connectedToBroker)
            {
                /* Check if already subscribed to topic
                 * and don't subscribe again.
                 */
                if (!subscribed)
                {
                    /* Subscribe to led topic. */
                    IOT.subscribe(subscribeTopic);
                    subscribed = true;
                }

                int sensorValue = sensorPin.ReadRaw();
                /* Publish to mic topic. */
                IOT.publish(publishTopic, sensorValue, QOS_1, true);
            }
        }

        public void OnnewMessageStrFloatCallback(string topic, float payload, byte qos, bool retain)
        {
            /* Check if 1000 messages were logged stop logging
             * and reset startLogging and counter variables.
            */
            if (counter == 1000)
            {
                DATALOGGER.stop();
                startedLogging = false;
                counter = 0;
            }
            /* Check if data logging not started, then start 
             * with a new file.
            */
            if (!startedLogging)
            {
                DATALOGGER.start("SensorValues");
                startedLogging = true;
            }
            /* Log data to file. */
            if (startedLogging)
            {
                DATALOGGER.add("Value", payload);
                DATALOGGER.log();
                counter++;
            }

        }

        public void OnConnectionStatus(byte statusCode)
        {
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

        //Literals
        const byte QOS_0 = 0x00;
        const byte QOS_1 = 0x01;
        const byte QOS_2 = 0x02;
        const byte NOT_RETAINED = 0x00;
        const byte RETAINED = 0x01;
    }
}
