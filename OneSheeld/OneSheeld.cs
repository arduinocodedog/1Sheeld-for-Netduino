using System;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace OneSheeldClasses
{
    public class OneSheeld
    {
        Stream OneSheeldSerial = null;
        SerialPort Serial1 = null;
        long lastTimeFrameSent = 0L;
        bool isFirstFrame = false;
        bool framestart = false;
        bool isArgumentsNumberAllocated = false;
        bool isArgumentLengthsAllocated = false;
        bool isOneSheeldConnected = false;
        bool isOneSheeldRemoteDataUsed = false;
        byte functions = 0;
        byte shield = 0;
        byte instance = 0;
        byte counter = 0;
        byte argumentcounter = 0;
        byte datalengthcounter = 0;
        byte argumentnumber = 0;
        byte numberOfDataAllocated = 0;
        byte endFrame = 0;
        byte[][] arguments = null;
        byte[] argumentLengths = null;
        int remoteOneSheeldsCounter = 0;
        IRemoteCallback remoteCallBack = null;

        public RemoteOneSheeld[] listOfRemoteOneSheelds = null;

        public AccelerometerSensorShield ACCELEROMETER = null;
        public BuzzerShield BUZZER = null;
        public CameraShield CAMERA = null;
        public ClockShield CLOCK = null;
        public DataLoggerShield DATALOGGER = null;
        public EmailShield EMAIL = null;
        public FacebookShield FACEBOOK = null;
        public FoursquareShield FOURSQUARE = null;
        public GamePadShield GAMEPAD = null;
        public GPSShield GPS = null;
        public GravitySensorShield GRAVITY = null;
        public GyroscopeSensorShield GYROSCOPE = null;
        public InternetShield INTERNET = null;
        public KeyboardShield KEYBOARD = null;
        public KeypadShield KEYPAD = null;
        public LCDShield LCD = null;
        public LedShield LED = null;
        public LightSensorShield LIGHT = null;
        public MagnetometerSensorShield MAGNETOMETER = null;
        public MicShield MIC = null;
        public MusicPlayerShield MUSICPLAYER = null;
        public NotificationShield NOTIFICATION = null;
        public OrientationSensorShield ORIENTATION = null;
        public PatternShield PATTERN = null;
        public PhoneShield PHONE = null;
        public PressureSensorShield PRESSURE = null;
        public ProximitySensorShield PROXIMITY = null;
        public PushButtonShield PUSHBUTTON = null;
        public SevenSegmentShield SEVENSEGMENT = null;
        public SkypeShield SKYPE = null;
        public SliderShield SLIDER = null;
        public SMSShield SMS = null;
        public TemperatureSensorShield TEMPERATURE = null;
        public TerminalShield TERMINAL = null;
        public ToggleButtonShield TOGGLEBUTTON = null;
        public TTSShield TTS = null;
        public TwitterShield TWITTER = null;
        public VoiceRecognitionShield VOICERECOGNITION = null;

        void begin(int baud)
        {
            Serial1 = new SerialPort(SerialPorts.COM1, baud, Parity.None, 8, StopBits.One);
            Serial1.DataReceived += processInput;
            Serial1.Open();
            OneSheeldSerial = Serial1;
        }

        void processInput(Object sender, SerialDataReceivedEventArgs e)
        {
            processInput();
        }

        void freeMemoryAllocated()
        {
            framestart = false;
            if (isArgumentsNumberAllocated)
            {
                for (int i = 0; i < numberOfDataAllocated; i++)
                {
                    arguments[i] = null;
                }
                numberOfDataAllocated = 0;
                arguments = null;
                isArgumentsNumberAllocated = false;
            }
            if (isArgumentLengthsAllocated)
            {
                argumentLengths = null;
                isArgumentLengthsAllocated = false;
            }
        }

        void sendToShields()
        {
            //Checking the Shield-ID    
            byte ShieldNumber = getShieldId();
            switch (ShieldNumber)
            {
                case (byte)ShieldIds.ONESHEELD_ID: processFrame(); break;
                case (byte)ShieldIds.KEYPAD_SHIELD_ID: KEYPAD.processFrame(); break;
                case (byte)ShieldIds.GPS_ID: GPS.processFrame(); break;
                case (byte)ShieldIds.SLIDER_ID: SLIDER.processFrame(); break;
                case (byte)ShieldIds.PUSH_BUTTON_ID: PUSHBUTTON.processFrame(); break;
                case (byte)ShieldIds.TOGGLE_BUTTON_ID: TOGGLEBUTTON.processFrame(); break;
                case (byte)ShieldIds.GAMEPAD_ID: GAMEPAD.processFrame(); break;
                case (byte)ShieldIds.PROXIMITY_ID: PROXIMITY.processFrame(); break;
                case (byte)ShieldIds.MIC_ID: MIC.processFrame(); break;
                case (byte)ShieldIds.TEMPERATURE_ID: TEMPERATURE.processFrame(); break;
                case (byte)ShieldIds.LIGHT_ID: LIGHT.processFrame(); break;
                case (byte)ShieldIds.PRESSURE_ID: PRESSURE.processFrame(); break;
                case (byte)ShieldIds.GRAVITY_ID: GRAVITY.processFrame(); break;
                case (byte)ShieldIds.ACCELEROMETER_ID: ACCELEROMETER.processFrame(); break;
                case (byte)ShieldIds.GYROSCOPE_ID: GYROSCOPE.processFrame(); break;
                case (byte)ShieldIds.ORIENTATION_ID: ORIENTATION.processFrame(); break;
                case (byte)ShieldIds.MAGNETOMETER_ID: MAGNETOMETER.processFrame(); break;
                case (byte)ShieldIds.PHONE_ID: PHONE.processFrame(); break;
                case (byte)ShieldIds.SMS_ID: SMS.processFrame(); break;
                case (byte)ShieldIds.CLOCK_ID: CLOCK.processFrame(); break;
                case (byte)ShieldIds.KEYBOARD_ID: KEYBOARD.processFrame(); break;
                case (byte)ShieldIds.TWITTER_ID: TWITTER.processFrame(); break;
                case (byte)ShieldIds.VOICE_RECOGNITION_ID: VOICERECOGNITION.processFrame(); break;
                case (byte)ShieldIds.TERMINAL_ID: TERMINAL.processFrame(); break;
                case (byte)ShieldIds.PATTERN_ID: PATTERN.processFrame(); break;
                case (byte)ShieldIds.INTERNET_ID: INTERNET.processData(); break;
                case (byte)ShieldIds.REMOTE_SHEELD_ID:
                    for (int i = 0; i < remoteOneSheeldsCounter; i++)
                        listOfRemoteOneSheelds[i].processFrame();
                    if (isOneSheeldRemoteDataUsed)
                        processRemoteData();
                    break;
            }
        }

        void processFrame()
        {
            byte functionId = getFunctionId();
            //Check  the function ID 
            if (functionId == DISCONNECTION_CHECK_FUNCTION)
            {
                isOneSheeldConnected = false;
            }
            else if (functionId == CONNECTION_CHECK_FUNCTION)
            {
                isOneSheeldConnected = true;
            }
            else if (functionId == LIBRARY_VERSION_REQUEST)
            {
                sendPacket((byte) ShieldIds.ONESHEELD_ID, 0, SEND_LIBRARY_VERSION, 0, null);
            }
        }

        void processRemoteData()
        { 
            byte functionId = getFunctionId();

            if(functionId == READ_MESSAGE_FLOAT)
            {
                string remoteAddress = "";
                for (int i = 0; i < 36; i++)
                    remoteAddress += Convert.ToChar(getArgumentData(0)[i]);

                string key = "";
                int keyLength = getArgumentLength(1);
                for (int i = 0; i < keyLength; i++)
                    key += Convert.ToChar(getArgumentData(1)[i]);

                float incomingValue = convertBytesToFloat(getArgumentData(2));

                if(isOneSheeldRemoteDataUsed)
                    remoteCallBack.OnNewMessage(remoteAddress,key,incomingValue);
            }
            else if(functionId == READ_MESSAGE_STRING)
            {
                string remoteAddress = "";
                for (int i = 0; i < 36; i++)
                    remoteAddress += Convert.ToChar(getArgumentData(0)[i]);

                string key = "";
                int keyLength = getArgumentLength(1);
                for (int i = 0; i < keyLength; i++)
                    key += Convert.ToChar(getArgumentData(1)[i]);

                string stringData = "";
                int stringDataLength = getArgumentLength(2);
                for (int i = 0; i < stringDataLength; i++)
                    stringData += Convert.ToChar(getArgumentData(2)[i]);

                if(isOneSheeldRemoteDataUsed)
                    remoteCallBack.OnNewMessage(remoteAddress,key,stringData);
            }
        }

        // ---------------------- Public Methods ----------------------

        public OneSheeld()
        {
            ACCELEROMETER = new AccelerometerSensorShield(this);
            BUZZER = new BuzzerShield(this);
            CAMERA = new CameraShield(this);
            CLOCK = new ClockShield(this);
            DATALOGGER = new DataLoggerShield(this);
            EMAIL = new EmailShield(this);
            FACEBOOK = new FacebookShield(this);
            GAMEPAD = new GamePadShield(this);
            FOURSQUARE = new FoursquareShield(this);
            GPS = new GPSShield(this);
            GRAVITY = new GravitySensorShield(this);
            GYROSCOPE = new GyroscopeSensorShield(this);
            KEYBOARD = new KeyboardShield(this);
            KEYPAD = new KeypadShield(this);
            INTERNET = new InternetShield(this);
            LCD = new LCDShield(this);
            LED = new LedShield(this);
            LIGHT = new LightSensorShield(this);
            MAGNETOMETER = new MagnetometerSensorShield(this);
            MIC = new MicShield(this);
            MUSICPLAYER = new MusicPlayerShield(this);
            NOTIFICATION = new NotificationShield(this);
            ORIENTATION = new OrientationSensorShield(this);
            PATTERN = new PatternShield(this);
            PHONE = new PhoneShield(this);
            PRESSURE = new PressureSensorShield(this);
            PROXIMITY = new ProximitySensorShield(this);
            PUSHBUTTON = new PushButtonShield(this);
            SEVENSEGMENT = new SevenSegmentShield(this);
            SKYPE = new SkypeShield(this);
            SLIDER = new SliderShield(this);
            SMS = new SMSShield(this);
            TEMPERATURE = new TemperatureSensorShield(this);
            TERMINAL = new TerminalShield(this);
            TOGGLEBUTTON = new ToggleButtonShield(this);
            TTS = new TTSShield(this);
            TWITTER = new TwitterShield(this);
            VOICERECOGNITION = new VoiceRecognitionShield(this);

            listOfRemoteOneSheelds = new RemoteOneSheeld[MAX_REMOTE_CONNECTIONS];
        }

        public void processInput()
        {
            byte[] buffer = new byte[1];

            while (((SerialPort)OneSheeldSerial).BytesToRead > 0)
            {
                int datacount = OneSheeldSerial.Read(buffer, 0, 1);
                if (datacount == 0) return;
                processInput(buffer[0]);
            }
        }

        void processInput(byte data)
        {
            if (!framestart && data == START_OF_FRAME)
            {
                freeMemoryAllocated();
                counter = 0;
                framestart = true;
                arguments = null;
                argumentLengths = null;
                counter++;
            }
            else if (counter == 4 && framestart) // data is the no of arguments
            {
                datalengthcounter = 0;
                argumentcounter = 0;
                argumentnumber = data;
                counter++;
            }
            else if (counter == 5 && framestart) // data is the no of arguments
            {
                if ((255 - argumentnumber) == data && argumentnumber == 0)
                {
                    counter = 9;
                    return;
                }
                else if ((255 - argumentnumber) == data)
                {
                    // need to allocate stuff here
                    arguments = new byte[argumentnumber][];
                    isArgumentsNumberAllocated = true;
                    argumentLengths = new byte[argumentnumber];
                    isArgumentLengthsAllocated = true;
                    counter++;
                }
                else
                {
                    framestart = false;
                    freeMemoryAllocated();
                    return;
                }
            }
            else if (counter == 6 && framestart)  // data is the first argument
            {
                argumentLengths[argumentcounter] = data;
                counter++;
            }
            else if (counter == 7 && framestart)  // data is the first argument data information
            {
                if ((255 - argumentLengths[argumentcounter]) == data)
                {
                    if (argumentLengths[argumentcounter] != 0)
                    {
                        arguments[argumentcounter] = new byte[argumentLengths[argumentcounter]];
                        counter++;
                    }
                    else
                    {
                        arguments[argumentcounter] = null;
                        datalengthcounter = 0;
                        argumentcounter++;
                        if (argumentcounter == argumentnumber)
                            counter = 9;
                        else
                            counter = 6;
                    }
                    numberOfDataAllocated++;
                }
                else
                {
                    framestart = false;
                    freeMemoryAllocated();
                    return;
                }
            }
            else if (counter == 8 && framestart)
            {
                if (arguments[argumentcounter] != null)
                    arguments[argumentcounter][datalengthcounter++] = data;
                if (datalengthcounter == argumentLengths[argumentcounter])
                {
                    datalengthcounter = 0;
                    argumentcounter++;
                    if (argumentcounter == argumentnumber)
                    {
                        counter++;    // increment the counter to take the last byte which is the end of the frame
                    }
                    else
                    {
                        counter = 6;
                    }
                }
            }
            else if (counter == 9 && framestart)
            {
                endFrame = data;
                if (endFrame == END_OF_FRAME)
                {
                    sendToShields();
                    freeMemoryAllocated();
                }
                else
                {
                    freeMemoryAllocated();
                }
            }
            else if (framestart)
            {
                if (counter == 1)
                {
                    shield = data;
                    bool found = false;
                    if (shield == (byte) ShieldIds.ONESHEELD_ID || shield == (byte) ShieldIds.REMOTE_SHEELD_ID)
                        found = true;
                    else
                    {
                        for (int i = 0; i < inputShieldsList.Length; i++)
                        {
                            if (shield == (byte)inputShieldsList[i])
                            {
                                found = true;
                            }
                        }
                    }
                    if (!found)
                    {
                        framestart = false;
                        freeMemoryAllocated();
                        return;
                    }
                }
                else if (counter == 2)
                {
                    instance = data;
                }
                else if (counter == 3)
                {
                    functions = data;
                }
                counter++;
            }
        }

        public void waitForAppConnection()
        {
          isOneSheeldConnected = false;

          while(!isOneSheeldConnected)
          {
            processInput();
          }

        }

        public void begin()
        {
            begin(115200);
        }

        public void delay(long time)
        {
            long now = (DateTime.Now.Ticks / 10000L) + 1L;
            long mill = now;
            byte[] buffer = new byte[1];

            while (mill < (now+time) || framestart)
            {
                if (((SerialPort)OneSheeldSerial).BytesToRead > 0)
                {
                    int datacount = OneSheeldSerial.Read(buffer, 0, 1);
                    if (datacount == 0) return;
                    processInput(buffer[0]);
                }
                mill = (DateTime.Now.Ticks / 10000L) + 1L;
            }
        }

        public void sendPacket(ShieldIds shieldID, byte instanceID, byte functionID, int argNo, ArrayList args)
        {
            long mill = (DateTime.Now.Ticks / 10000L) + 1L;
            byte[] buffer = new byte[1];

            if((shieldID != ShieldIds.ONESHEELD_ID) && isFirstFrame == true && lastTimeFrameSent > 0L && (mill-lastTimeFrameSent) < TIME_GAP)
            {
                while (mill < (TIME_GAP+lastTimeFrameSent) || framestart)
                {
                    if (((SerialPort)OneSheeldSerial).BytesToRead > 0)
                    {
                        int datacount = OneSheeldSerial.Read(buffer, 0, 1);
                        if (datacount == 0) return;
                        processInput(buffer[0]);
                    }
                    mill = (DateTime.Now.Ticks / 10000L) + 1L;
                }
            }

            isFirstFrame=true;
            buffer[0] = START_OF_FRAME;
            OneSheeldSerial.Write(buffer, 0, 1);
            buffer[0] = LIBRARY_VERSION;
            OneSheeldSerial.Write(buffer, 0, 1);
            buffer[0] = (byte) shieldID;
            OneSheeldSerial.Write(buffer, 0, 1);
            buffer[0] = instanceID;
            OneSheeldSerial.Write(buffer, 0, 1);
            buffer[0] = functionID;
            OneSheeldSerial.Write(buffer, 0, 1);
            buffer[0] = (byte) argNo;
            OneSheeldSerial.Write(buffer, 0, 1);
            buffer[0] = (byte)(255 - argNo);
            OneSheeldSerial.Write(buffer, 0, 1);

            for (int i=0; i < argNo; i++)
            {
                FunctionArg temp = args[i] as FunctionArg;
                buffer[0] = (byte)temp.getLength();
                OneSheeldSerial.Write(buffer, 0, 1);
                buffer[0] = (byte)(0xFF - (temp.getLength()));
                OneSheeldSerial.Write(buffer, 0, 1);

                for (int j = 0 ; j < temp.getLength() ; j++)
                {
                    buffer[0] = temp.getData()[j];
                    OneSheeldSerial.Write(buffer, 0, 1);
                }

                temp = null;
            }
              
            buffer[0] = END_OF_FRAME;
            OneSheeldSerial.Write(buffer, 0, 1);
            lastTimeFrameSent = (DateTime.Now.Ticks / 10000L) + 1L;

        }

        public bool isAppConnected()
        {
          return isOneSheeldConnected;
        }

        public byte getShieldId()
        {
          return shield;
        } 

        public byte getInstanceId()
        {
            return instance;
        }

        public byte getFunctionId()
        {
            return functions;
        }

        public byte getArgumentNo()
        {
            return argumentnumber;
        }

        public byte getArgumentLength(byte x)
        {
            return argumentLengths[x];
        }

        public byte[] getArgumentData(byte x)
        {
          return arguments[x];
        }

        public float convertBytesToFloat(byte[] data)
        {
            return System.BitConverter.ToSingle(data, 0);
        }

        public byte[] convertFloatToBytes(float data)
        {
            return System.BitConverter.GetBytes(data);
        }

        public Cpu.Pin ConvertByteToPin(byte PinNumber)
        {
            Cpu.Pin retval = Cpu.Pin.GPIO_NONE;

            switch (PinNumber)
            {
                case 0: retval = Pins.GPIO_PIN_D0; break;
                case 1: retval = Pins.GPIO_PIN_D1; break;
                case 2: retval = Pins.GPIO_PIN_D2; break;
                case 3: retval = Pins.GPIO_PIN_D3; break;
                case 4: retval = Pins.GPIO_PIN_D4; break;
                case 5: retval = Pins.GPIO_PIN_D5; break;
                case 6: retval = Pins.GPIO_PIN_D6; break;
                case 7: retval = Pins.GPIO_PIN_D7; break;
                case 8: retval = Pins.GPIO_PIN_D8; break;
                case 9: retval = Pins.GPIO_PIN_D9; break;
                case 10: retval = Pins.GPIO_PIN_D10; break;
                case 11: retval = Pins.GPIO_PIN_D11; break;
                case 12: retval = Pins.GPIO_PIN_D12; break;
                case 13: retval = Pins.GPIO_PIN_D13; break;

                case 14: retval = Pins.GPIO_PIN_A0; break;
                case 15: retval = Pins.GPIO_PIN_A1; break;
                case 16: retval = Pins.GPIO_PIN_A2; break;
                case 17: retval = Pins.GPIO_PIN_A3; break;
                case 18: retval = Pins.GPIO_PIN_A4; break;
                case 19: retval = Pins.GPIO_PIN_A5; break;

                default: break;
            }

            return retval;
        }

        public byte ConvertPinToByte(Cpu.Pin pin)
        {
            byte retval = 0x0ff;

            if (pin.Equals(Pins.GPIO_PIN_D0))
                retval = 0;
            else if (pin.Equals(Pins.GPIO_PIN_D1))
                retval = 1;
            else if (pin.Equals(Pins.GPIO_PIN_D2))
                retval = 2;
            else if (pin.Equals(Pins.GPIO_PIN_D3))
                retval = 3;
            else if (pin.Equals(Pins.GPIO_PIN_D4))
                retval = 4;
            else if (pin.Equals(Pins.GPIO_PIN_D5))
                retval = 5;
            else if (pin.Equals(Pins.GPIO_PIN_D6))
                retval = 6;
            else if (pin.Equals(Pins.GPIO_PIN_D7))
                retval = 7;
            else if (pin.Equals(Pins.GPIO_PIN_D8))
                retval = 8;
            else if (pin.Equals(Pins.GPIO_PIN_D9))
                retval = 9;
            else if (pin.Equals(Pins.GPIO_PIN_D10))
                retval = 10;
            else if (pin.Equals(Pins.GPIO_PIN_D11))
                retval = 11;
            else if (pin.Equals(Pins.GPIO_PIN_D12))
                retval = 12;
            else if (pin.Equals(Pins.GPIO_PIN_D13))
                retval = 13;

            else if (pin.Equals(Pins.GPIO_PIN_A0))
                retval = 14;
            else if (pin.Equals(Pins.GPIO_PIN_A1))
                retval = 15;
            else if (pin.Equals(Pins.GPIO_PIN_A1))
                retval = 16;
            else if (pin.Equals(Pins.GPIO_PIN_A1))
                retval = 17;
            else if (pin.Equals(Pins.GPIO_PIN_A1))
                retval = 18;
            else if (pin.Equals(Pins.GPIO_PIN_A1))
                retval = 19;

            return retval;
        }

        public void listenToRemoteOneSheeld(RemoteOneSheeld remoteonesheeld)
        {
            if (remoteOneSheeldsCounter < MAX_REMOTE_CONNECTIONS)
                listOfRemoteOneSheelds[remoteOneSheeldsCounter++] = remoteonesheeld;
        }

        public void setOnNewMessage(IRemoteCallback userCallback)
        {
            isOneSheeldRemoteDataUsed = true;
            remoteCallBack = userCallback;
        }

        //Start and End of packet sent
        const byte START_OF_FRAME = 0xFF;
        const byte END_OF_FRAME = 0x00;

        //Library Version
        const byte LIBRARY_VERSION = 5;

        //Output function ID's
        const byte SEND_LIBRARY_VERSION = 0x01;

        //Input function ID's 
        //Checking Bluetooth connection
        const byte CONNECTION_CHECK_FUNCTION = 0x01;
        const byte DISCONNECTION_CHECK_FUNCTION = 0x02;
        const byte LIBRARY_VERSION_REQUEST = 0x03;

        // Time between sending frames
        const int TIME_GAP = 200;

        // Maximum number of Remote Connections
        const int MAX_REMOTE_CONNECTIONS = 10;

        //RemoteShield constants
        const byte READ_MESSAGE_FLOAT = 0x02;
        const byte READ_MESSAGE_STRING = 0x03;

        // inputShields
        ShieldIds[] inputShieldsList = { 
            ShieldIds.ONESHEELD_ID, ShieldIds.KEYPAD_SHIELD_ID, ShieldIds.GPS_ID, ShieldIds.SLIDER_ID, 
            ShieldIds.PUSH_BUTTON_ID, ShieldIds.TOGGLE_BUTTON_ID, ShieldIds.GAMEPAD_ID, ShieldIds.PROXIMITY_ID, 
            ShieldIds.MIC_ID, ShieldIds.TEMPERATURE_ID, ShieldIds.LIGHT_ID, ShieldIds.PRESSURE_ID, ShieldIds.GRAVITY_ID,
            ShieldIds.ACCELEROMETER_ID, ShieldIds.GYROSCOPE_ID, ShieldIds.ORIENTATION_ID, ShieldIds.MAGNETOMETER_ID, 
            ShieldIds.PHONE_ID, ShieldIds.SMS_ID, ShieldIds.CLOCK_ID, ShieldIds.KEYBOARD_ID, ShieldIds.TWITTER_ID, 
            ShieldIds.VOICE_RECOGNITION_ID, ShieldIds.TERMINAL_ID, ShieldIds.PATTERN_ID, ShieldIds.REMOTE_SHEELD_ID,
            ShieldIds.INTERNET_ID
        };

    }

    public enum ShieldIds
    {
        ONESHEELD_ID, SLIDER_ID, LED_ID, PUSH_BUTTON_ID, TOGGLE_BUTTON_ID, FLASH_ID, NOTIFICATION_ID, SEV_SEG_ID,
        BUZZER_ID, KEYPAD_SHIELD_ID, MAGNETOMETER_ID, ACCELEROMETER_ID, GAMEPAD_ID, SMS_ID, GYROSCOPE_ID, ORIENTATION_ID,
        LIGHT_ID, PRESSURE_ID, TEMPERATURE_ID, PROXIMITY_ID, GRAVITY_ID, CAMERA_ID, UNKNOWN1_ID, LCD_ID, MIC_ID, FACEBOOK_ID,
        TWITTER_ID, FOURSQUARE_ID, GPS_ID, MUSIC_PLAYER_ID, EMAIL_ID, SKYPE_ID, PHONE_ID, CLOCK_ID, KEYBOARD_ID, TTS_ID,
        VOICE_RECOGNITION_ID, DATA_LOGGER_ID, TERMINAL_ID, PATTERN_ID, REMOTE_SHEELD_ID, INTERNET_ID
    };

}
