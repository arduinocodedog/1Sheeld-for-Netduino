namespace OneSheeldClasses
{
    public class IOTShield : ShieldParent
    {
        int callbacksAssignments = 0;
        bool isBrokerConnected = false;
        bool isConnStatCallbackAssigned = false;
        bool isErrorCallbackAssigned = false;
        IIOTconnectionStatusCallback connectionStatusCallback = null;
        IIOTStrStrCallback newMessageStrStrCallback = null;
        IIOTStrIntCallback newMessageStrIntCallback = null;
        IIOTStrUnIntCallback newMessageStrUnIntCallback = null;
        IIOTStrFloatCallback newMessageStrFloatCallback = null;
        IIOTStrRawCallback newMessageStrRawCallback = null;
        IIOTerrorCallback errorCallback = null;

        public IOTShield()
             : base(ShieldIds.IOT_ID)
        {
        }

        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();

            if (functionID == IOT_GET_DATA)
            {
                byte topicLength = getOneSheeldInstance().getArgumentLength(0);
                string topic = "";
                for (int i = 0; i < topicLength; i++)
                {
                    topic += (char) getOneSheeldInstance().getArgumentData(0)[i];
                }
                byte qos = getOneSheeldInstance().getArgumentData(2)[0];
                bool retain = (getOneSheeldInstance().getArgumentData(3)[0] == (byte) 0 ? false : true);
                //Invoke User Function
                if (!isInACallback())
                {
                    if (callbacksAssignments != 0)
                    {
                        enteringACallback();
                    }

                    if ((callbacksAssignments & (1 << STRING_STRING)) != 0)
                    {
                        byte payloadLength = getOneSheeldInstance().getArgumentLength(1);
                        string payload = "";
                        for (int i = 0; i < payloadLength; i++)
                        {
                            payload += (char) getOneSheeldInstance().getArgumentData(1)[i];
                        }
                        newMessageStrStrCallback.OnsetNewMessageStrStr(topic, payload, qos, retain);
                    }
                    if ((callbacksAssignments & (1 << STRING_INT)) != 0)
                    {
                        int payload = getOneSheeldInstance().getArgumentData(1)[0] | ((getOneSheeldInstance().getArgumentData(1)[1]) << 8);
                        newMessageStrIntCallback.OnsetNewMessageStrInt(topic, payload, qos, retain);
                    }
                    if ((callbacksAssignments & (1 << STRING_UNINT)) != 0)
                    {
                        uint payload = (uint) (getOneSheeldInstance().getArgumentData(1)[0] | ((getOneSheeldInstance().getArgumentData(1)[1]) << 8));
                        newMessageStrUnIntCallback.OnnewMessageStrUnIntCallback(topic, payload, qos, retain);
                    }
                    if ((callbacksAssignments & (1 << STRING_FLOAT)) != 0)
                    {
                        float payload = OneSheeldMain.OneSheeld.convertBytesToFloat(getOneSheeldInstance().getArgumentData(1));
                        newMessageStrFloatCallback.OnnewMessageStrFloatCallback(topic, payload, qos, retain);
                    }
                    if ((callbacksAssignments & (1 << STRING_RAW)) != 0)
                    {
                        byte[] payload = getOneSheeldInstance().getArgumentData(1);
                        byte payloadLength = getOneSheeldInstance().getArgumentLength(1);
                        newMessageStrRawCallback.OnnewMessageStrRawCallback(topic, payload, payloadLength, qos, retain);
                    }

                    if (callbacksAssignments != 0)
                    {
                        exitingACallback();
                    }
                }
            }
            else if (functionID == IOT_GET_CONNECTION)
            {
                byte connectionStatus = getOneSheeldInstance().getArgumentData(0)[0];
                if (connectionStatus == CONNECTION_SUCCESSFUL)
                {
                    isBrokerConnected = true;
                }
                else if (connectionStatus == CONNECTION_FAILED || connectionStatus == CONNECTION_LOST || connectionStatus == NOT_CONNECTED_YET)
                {
                    isBrokerConnected = false;
                }
                //Invoke User Function
                if (!isInACallback())
                {
                    if (isConnStatCallbackAssigned)
                    {
                        enteringACallback();
                        connectionStatusCallback.OnConnectionStatus(connectionStatus);
                        exitingACallback();
                    }
                }
            }
            else if (functionID == IOT_GET_ERROR && !isInACallback())
            {
                byte errorNumber = getOneSheeldInstance().getArgumentData(0)[0];
                //Invoke User Function
                if (isErrorCallbackAssigned)
                {
                    enteringACallback();
                    errorCallback.OnError(errorNumber);
                    exitingACallback();
                }
            }
        }

        public bool isConnected()
        {
            return isBrokerConnected;
        }

        public void setHost(string server)
        {
            if (server.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg = new FunctionArg(server);
                args.Add(arg);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_SET_HOST, 1, args);
            }
        }

        public void setPort(uint portNumber)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(portNumber);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_SET_PORT, 1, args);
        }

        public void setClientID(string clientID)
        {
            if (clientID.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg = new FunctionArg(clientID);
                args.Add(arg);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_SET_CLIENT_ID, 1, args);
            }
        }

        public void setLastWillAndTestament(string willTopic, string willMessage, byte willQos, bool willRetained)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(willTopic);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(willMessage);
            args.Add(arg2);

            FunctionArg arg3 = new FunctionArg(willQos);
            args.Add(arg3);

            FunctionArg arg4 = new FunctionArg(willRetained);
            args.Add(arg4);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_LAST_WILL, 4, args);
        }

        public void setCleanSession(bool cleanSession)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(cleanSession);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CLEAN_SESSION, 1, args);
        }

        public void setKeepAlive(uint keepAlive)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(keepAlive);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_KEEP_ALIVE, 1, args);
        }

        public void setCredentials(string userName, string password)
        {
            if (userName.Length > 0 && password.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(userName);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(password);
                args.Add(arg2);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CREDENTIALS, 2, args);
            }
        }

        public void setAutoReconnect(bool autoReconnect)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(autoReconnect);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_AUTO_RECON, 1, args);
        }

        public void setSecureConnection(bool state)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(state);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_SSL_CONNECTION, 1, args);
        }

        public void connect()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CONNECT);
        }

        public void connect(string host)
        {
            if (host.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg = new FunctionArg(host);
                args.Add(arg);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CONNECT, 1, args);
            }
        }

        public void connect(string host, uint portNumber)
        {
            if (host.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(host);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(portNumber);
                args.Add(arg2);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CONNECT, 2, args);
            }
        }

        public void connect(string host, uint portNumber, bool connectSSL)
        {
            if (host.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(host);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(portNumber);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(connectSSL);
                args.Add(arg3);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CONNECT, 3, args);
            }
        }

        public void connect(string host, string userName, string password, uint portNumber)
        {
            if (host.Length > 0 && userName.Length > 0 && password.Length > 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(host);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(userName);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(password);
                args.Add(arg3);

                FunctionArg arg4 = new FunctionArg(portNumber);
                args.Add(arg4);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_CONNECT, 4, args);
            }
        }

        public void disconnect()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_DISCONNECT);
            isBrokerConnected = false;
        }

       
        public void publish(string topic, byte[] payload, byte qos = 0, bool retain = false)
        {
            if (qos > 2)
                qos = 2;

            if (payload.Length == 0)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(topic);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(qos);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(retain);
                args.Add(arg3);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_PUBLISH_EMPTY, 3, args);
            }
            else
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg1 = new FunctionArg(topic);
                args.Add(arg1);

                FunctionArg arg2 = new FunctionArg(payload);
                args.Add(arg2);

                FunctionArg arg3 = new FunctionArg(qos);
                args.Add(arg3);

                FunctionArg arg4 = new FunctionArg(retain);
                args.Add(arg4);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_PUBLISH, 4, args);
            }
        }

        public void publish(string topic, string payload, byte qos = 0, bool retain = false)
        {
            publish(topic, System.Text.Encoding.UTF8.GetBytes(payload), qos, retain);
        }

        public void publish(string topic, int i, byte qos = 0, bool retain = false)
        {
            byte[] payload = new byte[2];
            payload[1] = (byte)((i >> 8) & 0xFF);
            payload[0] = (byte)(i & 0xFF);

            publish(topic, payload, qos, retain);
        }

        public void publish(string topic, uint ui, byte qos = 0, bool retain = false)
        {
            byte[] payload = new byte[2];
            payload[1] = (byte)((ui >> 8) & 0xFF);
            payload[0] = (byte)(ui & 0xFF);

            publish(topic, payload, qos, retain);
        }

        public void publish(string topic, float payload, byte qos = 0, bool retain = false)
        {
            publish(topic, OneSheeldMain.OneSheeld.convertFloatToBytes(payload), qos, retain);
        }

        public void subscribe(string topic, byte qos = 0)
        {
            if (qos > 2)
                qos = 2;

            FunctionArgs args = new FunctionArgs();

            FunctionArg arg1 = new FunctionArg(topic);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(qos);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_SUBSCRIBE, 2, args);
        }

        public void unsubscribe()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_UNSUBSCRIBE);
        }

        public void unsubscribe(string topic)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(topic);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_UNSUBSCRIBE, 1, args);
        }

        public void resetConnectionParametersToDefaults()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.IOT_ID, 0, IOT_RESET_TO_DEFAULT);
        }

        public void setConnectionStatus(IIOTconnectionStatusCallback callback)
        {
            connectionStatusCallback = callback;
            isConnStatCallbackAssigned = true;
        }

        public void setNewMessageStrStr(IIOTStrStrCallback callback)
        {
            newMessageStrStrCallback = callback;
            callbacksAssignments = callbacksAssignments | (1 << STRING_STRING);
        }

        public void setNewMessageStrInt(IIOTStrIntCallback callback)
        {
            newMessageStrIntCallback = callback;
            callbacksAssignments = callbacksAssignments | (1 << STRING_INT);
        }

        public void setNewMessageStrUInt(IIOTStrUnIntCallback callback)
        {
            newMessageStrUnIntCallback = callback;
            callbacksAssignments = callbacksAssignments | (1 << STRING_UNINT);
        }

        public void setNewMessageStrFloat(IIOTStrFloatCallback callback)
        {
            newMessageStrFloatCallback = callback;
            callbacksAssignments = callbacksAssignments | (1 << STRING_FLOAT);
        }

        public void setNewMessageStrRaw(IIOTStrRawCallback callback)
        {
            newMessageStrRawCallback = callback;
            callbacksAssignments = callbacksAssignments | (1 << STRING_RAW);
        }

        public void setOnError(IIOTerrorCallback callback)
        {
            errorCallback = callback;
            isErrorCallbackAssigned = true;
        }

        //Output Function ID
        const byte IOT_RESET_TO_DEFAULT = 0x00;
        const byte IOT_SET_HOST = 0x01;
        const byte IOT_SET_PORT = 0x02;
        const byte IOT_SET_CLIENT_ID = 0x03;
        const byte IOT_LAST_WILL = 0x04;
        const byte IOT_CLEAN_SESSION = 0x05;
        const byte IOT_KEEP_ALIVE = 0x06;
        const byte IOT_CREDENTIALS = 0x07;
        const byte IOT_AUTO_RECON = 0x08;
        const byte IOT_CONNECT = 0x09;
        const byte IOT_DISCONNECT = 0x0A;
        const byte IOT_PUBLISH = 0x0B;
        const byte IOT_SUBSCRIBE = 0x0C;
        const byte IOT_UNSUBSCRIBE = 0x0D;
        const byte IOT_UNSUBSCRIBE_ALL = 0x0E;
        const byte IOT_SSL_CONNECTION = 0x0F;

        // Exception until we support sending empty strings
        const byte IOT_PUBLISH_EMPTY = 0xFE;

        //Input Function ID
        const byte IOT_GET_DATA = 0x01;
        const byte IOT_GET_CONNECTION = 0x02;
        const byte IOT_GET_ERROR = 0x03;

        //Literals
        const byte QOS_0 = 0x00;
        const byte QOS_1 = 0x01;
        const byte QOS_2 = 0x02;
        const byte NOT_RETAINED = 0x00;
        const byte RETAINED = 0x01;

        //Connection codes literals
        const byte CONNECTION_SUCCESSFUL = 0x01;
        const byte CONNECTION_FAILED = 0x02;
        const byte CONNECTION_LOST = 0x03;
        const byte CONNECTION_LOST_RECONNECTING = 0x04;
        const byte NOT_CONNECTED_YET = 0x05;
        const byte MISSING_HOST = 0x06;

        // Callback assginments order in callbacksAssignments variable
        const byte CHAR_CHAR = 0;  // Not supported in Netduino
        const byte CHAR_INT = 1;   // Not supported in Netduino
        const byte CHAR_UNINT = 2; // Not supported in Netduino
        const byte CHAR_FLOAT = 3; // Not supported in Netduino
        const byte CHAR_RAW = 4;   // Not supported in Netduino
        const byte STRING_STRING = 5;
        const byte STRING_INT = 6;
        const byte STRING_UNINT = 7;
        const byte STRING_FLOAT = 8;
        const byte STRING_RAW = 9;

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

