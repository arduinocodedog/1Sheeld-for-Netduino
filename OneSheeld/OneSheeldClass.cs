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
    public class OneSheeldClass : OneSheeldMain
    {
        static ulong lastTimeFrameSent = 0L;
        static bool isFirstFrame = false;
        static bool isInit = false;
        static bool callbacksInterrupts = false;
        static bool inACallback = false;
        static int shieldsCounter = 0;
        static byte requestsCounter = 0;
        static ShieldParent[] shieldsArray = null;
        static HttpRequest[] requestsArray = null;
        static SerialPort Serial1 = null;

        Stream OneSheeldSerial = null;
        bool framestart = false;
        bool isArgumentsNumberAllocated = false;
        bool isArgumentLengthsAllocated = false;
        bool isOneSheeldConnected = false;
        bool isAppConnectedCallBack = false;
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

        IAppConnected AppConnectedCallBack = null;

        public OneSheeldClass()
        {
            if (OneSheeldClass.shieldsArray == null)
                OneSheeldClass.shieldsArray = new ShieldParent[SHIELDS_NO];

            if (OneSheeldClass.requestsArray == null)
                OneSheeldClass.requestsArray = new HttpRequest[MAX_NO_OF_REQUESTS];
        }

        void begin(int baud)
        {
            OneSheeldClass.Serial1 = new SerialPort(SerialPorts.COM1, baud, Parity.None, 8, StopBits.One);
            OneSheeldClass.Serial1.DataReceived += processInput;
            OneSheeldClass.Serial1.Open();
            OneSheeldSerial = OneSheeldClass.Serial1;
        }

        public void waitForAppConnection()
        {
            isOneSheeldConnected = false;

            while (!isOneSheeldConnected)
            {
                processInput();
            }
        }

        public void begin()
        {
            begin(115200);
            sendPacket(ShieldIds.ONESHEELD_ID, 0, CHECK_APP_CONNECTION);
            OneSheeldClass.isInitialized = true;
            for (int i = 0; i < OneSheeldClass.requestsCounter; i++)
                OneSheeldClass.requestsArray[i].sendInitFrame();
            requestsArray = null;
        }

        public static void addToShieldsArray(ShieldParent shield)
        {
            if (OneSheeldClass.shieldsCounter == SHIELDS_NO)
                return;
            OneSheeldClass.shieldsArray[OneSheeldClass.shieldsCounter++] = shield;
        }

        public static void addToUnSentRequestsArray(HttpRequest request)
        {
            if (OneSheeldClass.requestsCounter == MAX_NO_OF_REQUESTS)
                return;
            OneSheeldClass.requestsArray[OneSheeldClass.requestsCounter++] = request;
        }

        public static bool isInitialized
        {
            get { return OneSheeldClass.isInit; }
            set { OneSheeldClass.isInit = value; }
        }

        public void sendPacket(ShieldIds shieldID, byte instanceID, byte functionID, int argNo = 0, ArrayList args = null)
        {
            ulong mill = millis() + 1;
            ulong localLastTimeFrameSent = OneSheeldClass.lastTimeFrameSent;
            if ((shieldID != ShieldIds.ONESHEELD_ID) && OneSheeldClass.isFirstFrame == true && localLastTimeFrameSent > 0L && (mill - localLastTimeFrameSent) < TIME_GAP)
            {
                byte[] buffer = new byte[1];

                if (isInACallback())
                {
                    OneSheeldClass TempOneSheeld = new OneSheeldClass();
                    TempOneSheeld.OneSheeldSerial = OneSheeldSerial;

                    ShieldParent.setOneSheeldInstance(TempOneSheeld);
                    while ((millis() < (TIME_GAP + localLastTimeFrameSent)) || TempOneSheeld.framestart)
                    {
                        if (((SerialPort)TempOneSheeld.OneSheeldSerial).BytesToRead > 0)
                        {
                            int datacount = TempOneSheeld.OneSheeldSerial.Read(buffer, 0, 1);
                            if (datacount == 0) return;
                            TempOneSheeld.processInput(buffer[0]);
                        }
                    }
                    ShieldParent.unSetOneSheeldInstance();
                }
                else
                {
                    while ((millis() < (TIME_GAP + localLastTimeFrameSent)) || framestart)
                    {
                        if (((SerialPort)OneSheeldSerial).BytesToRead > 0)
                        {
                            int datacount = OneSheeldSerial.Read(buffer, 0, 1);
                            if (datacount == 0) return;
                            processInput(buffer[0]);
                        }
                    }
                }
            }

            OneSheeldClass.isFirstFrame = true;
            Write(START_OF_FRAME);
            Write(LIBRARY_VERSION);
            Write((byte)shieldID);
            Write(instanceID);
            Write(functionID);
            Write((byte)argNo);
            Write((byte)(255 - argNo));

            for (int i = 0; i < argNo; i++)
            {
                FunctionArg temp = args[i] as FunctionArg;
                Write((byte)temp.getLength());
                Write((byte)(255 - (temp.getLength())));
                for (int j = 0; j < temp.getLength(); j++)
                {
                    Write(temp.getData()[j]);
                }
                temp = null;
            }

            Write(END_OF_FRAME);
            if (shieldID != ShieldIds.ONESHEELD_ID) 
                OneSheeldClass.lastTimeFrameSent = millis() + 1;
        }

        public bool isAppConnected()
        {
            return isOneSheeldConnected;
        }

        void setOnAppConnected(IAppConnected userCallback)
        {
          AppConnectedCallBack = userCallback;
          isAppConnectedCallBack = true;
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
            if (argumentLengths[x] != 0)
                return arguments[x];
            return null;
        }

        public byte[] convertFloatToBytes(float data)
        {
            return System.BitConverter.GetBytes(data);
        }

        public float convertBytesToFloat(byte[] data)
        {
            return System.BitConverter.ToSingle(data, 0);
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
                    if (shield == (byte)ShieldIds.ONESHEELD_ID)
                        found = true;
                    else
                    {
                        for (int i = 0; i < OneSheeldClass.shieldsCounter; i++)
                        {
                            if (shield == OneSheeldClass.shieldsArray[i].getShieldID())
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

        void freeMemoryAllocated()
        {
            framestart = false;
            if (isArgumentsNumberAllocated)
            {
                for (int i = 0; i < numberOfDataAllocated; i++)
                {
                    if (arguments[i] != null)
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
                case (byte)ShieldIds.ONESHEELD_ID:
                    processFrame(); break;

                default:
                    for (int i = 0; i < OneSheeldClass.shieldsCounter; i++)
                    {
                        OneSheeldClass.shieldsArray[i].processFrame();
                    }
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
                if (isAppConnectedCallBack)
                    AppConnectedCallBack.OnAppConnected(isOneSheeldConnected);
            }
            else if (functionId == CONNECTION_CHECK_FUNCTION)
            {
                isOneSheeldConnected = true;
                if (isAppConnectedCallBack)
                    AppConnectedCallBack.OnAppConnected(isOneSheeldConnected);
            }
            else if (functionId == LIBRARY_VERSION_REQUEST)
            {
                sendPacket(ShieldIds.ONESHEELD_ID, 0, SEND_LIBRARY_VERSION);
            }
        }

        /***** NO analogRead ... not used anyhow *****/

        public void enteringACallback()
        {
            if (!isInACallback())
            {
                OneSheeldClass.inACallback = true;
                sendPacket(ShieldIds.ONESHEELD_ID, 0, CALLBACK_ENTERED);
            }
        }

        public void exitingACallback()
        {
            if (isInACallback())
            {
                OneSheeldClass.inACallback = false;
                sendPacket(ShieldIds.ONESHEELD_ID, 0, CALLBACK_EXITED);
            }
        }

        public bool isInACallback()
        {
            return (OneSheeldClass.inACallback && !OneSheeldClass.callbacksInterrupts);
        }

        public bool callbacksInterruptsSet()
        {
            return OneSheeldClass.callbacksInterrupts;
        }

        public void disablecallbacksInterrupts()
        {
            OneSheeldClass.callbacksInterrupts = false;
        }

        public void enablecallbacksInterrupts()
        {
            OneSheeldClass.callbacksInterrupts = true;
        }

        public void delay(long time)
        {
            ulong now = millis();
            byte[] buffer = new byte[1];

            if (isInACallback())
            {
                OneSheeldClass TempOneSheeld = new OneSheeldClass();
                TempOneSheeld.OneSheeldSerial = OneSheeldSerial;

                ShieldParent.setOneSheeldInstance(TempOneSheeld);
                while ((millis() < (now + (ulong)time)) || TempOneSheeld.framestart)
                {
                    if (((SerialPort)TempOneSheeld.OneSheeldSerial).BytesToRead > 0)
                    {
                        int datacount = TempOneSheeld.OneSheeldSerial.Read(buffer, 0, 1);
                        if (datacount == 0) return;
                        processInput(buffer[0]);
                    }
                }
                ShieldParent.unSetOneSheeldInstance();
            }
            else
            {
                while ((millis() < (now + (ulong)time)) || framestart)
                {
                    if (((SerialPort)OneSheeldSerial).BytesToRead > 0)
                    {
                        int datacount = OneSheeldSerial.Read(buffer, 0, 1);
                        if (datacount == 0) return;
                        processInput(buffer[0]);
                    }
                }
            }
        }

        // ----- Netduino Specific Functions -----

        // For Serial Event Handling on Netduino
        void processInput(Object sender, SerialDataReceivedEventArgs e)
        {
            processInput();
        }

        public ulong millis()
        {
            return (ulong)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
        }

        // Write a byte to the Serial Port
        public void Write(byte b)
        {
            byte[] buffer = new byte[1];
            buffer[0] = b;

            OneSheeldSerial.Write(buffer, 0, 1);
        }

        // Convert a Arduino Pin # to a Netduino GPIO_PIN
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

        // Convert a Netduino GPIO_PIN to an Arduino Pin #
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

        //Start and End of packet sent
        const byte START_OF_FRAME = 0xFF;
        const byte END_OF_FRAME = 0x00;

        //Library Version
        const byte LIBRARY_VERSION = 6;

        //Output function ID's
        const byte SEND_LIBRARY_VERSION = 0x01;
        const byte CHECK_APP_CONNECTION = 0x02;
        const byte CALLBACK_ENTERED = 0x03;
        const byte CALLBACK_EXITED = 0x04;

        //Input function ID's 
        //Checking Bluetooth connection
        const byte CONNECTION_CHECK_FUNCTION = 0x01;
        const byte DISCONNECTION_CHECK_FUNCTION = 0x02;
        const byte LIBRARY_VERSION_REQUEST = 0x03;

        // Time between sending frames
        const int TIME_GAP = 200;

        // Number of Shields
        const int SHIELDS_NO = 40;

        // Maximum number of Remote Connections
        const int MAX_REMOTE_CONNECTIONS = 10;

        const int MAX_NO_OF_REQUESTS = 20;

        //RemoteShield constants
        const byte READ_MESSAGE_FLOAT = 0x02;
        const byte READ_MESSAGE_STRING = 0x03;
    }
}
