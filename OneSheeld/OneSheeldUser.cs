namespace OneSheeldClasses
{
    public class OneSheeldUser
    {
        public static AccelerometerSensorShield ACCELEROMETER
        {
            get { return OneSheeldMain._ACCELEROMETER;  }
            set { }
        }

        public static BarcodeScannerShield BARCODESCANNER
        {
            get { return OneSheeldMain._BARCODESCANNER; }
            set { }
        }

        public static BuzzerShield BUZZER
        {
            get { return OneSheeldMain._BUZZER;  }
            set { }
        }

        public static CameraShield CAMERA
        {
            get { return OneSheeldMain._CAMERA;  }
            set { }
        }

        public static ClockShield CLOCK
        {
            get { return OneSheeldMain._CLOCK;  }
            set { }
        }

        public static ColorShield COLORDETECTOR
        {
            get { return OneSheeldMain._COLORDETECTOR; }
            set { }
        }

        public static DataLoggerShield DATALOGGER
        {
            get { return OneSheeldMain._DATALOGGER;  }
            set { }
        }

        public static EmailShield EMAIL
        {
            get { return OneSheeldMain._EMAIL;  }
            set { }
        }

        public static FacebookShield FACEBOOK
        {
            get { return OneSheeldMain._FACEBOOK;  }
            set { }
        }

        public static FoursquareShield FOURSQUARE
        {
            get { return OneSheeldMain._FOURSQUARE;  }
            set { }
        }

        public static GamePadShield GAMEPAD
        {
            get { return OneSheeldMain._GAMEPAD;  }
            set { }
        }

        public static GLCDShield GLCD
        {
            get { return OneSheeldMain._GLCD; }
            set { }
        }

        public static GPSShield GPS
        {
            get { return OneSheeldMain._GPS;  }
            set { }
        }

        public static GravitySensorShield GRAVITY
        {
            get { return OneSheeldMain._GRAVITY;  }
            set { }
        }

        public static GyroscopeSensorShield GYROSCOPE
        {
            get { return OneSheeldMain._GYROSCOPE;  }
            set { }
        }

        public static InternetShield INTERNET
        {
            get { return OneSheeldMain._INTERNET;  }
            set { }
        }

        public static KeyboardShield KEYBOARD
        {
            get { return OneSheeldMain._KEYBOARD;  }
            set { }
        }

        public static KeypadShield KEYPAD
        {
            get { return OneSheeldMain._KEYPAD;  }
            set { }
        }

        public static LCDShield LCD
        {
            get { return OneSheeldMain._LCD;  }
            set { }
        }

        public static LedShield LED
        {
            get { return OneSheeldMain._LED;  }
            set { }
        }

        public static LightSensorShield LIGHT
        {
            get { return OneSheeldMain._LIGHT;  }
            set { }
        }

        public static MagnetometerSensorShield MAGNETOMETER
        {
            get { return OneSheeldMain._MAGNETOMETER;  }
            set { }
        }

        public static MicShield MIC
        {
            get { return OneSheeldMain._MIC;  }
            set { }
        }

        public static MusicPlayerShield MUSICPLAYER
        {
            get { return OneSheeldMain._MUSICPLAYER;  }
            set { }
        }

        public static NFCShield NFC
        {
            get { return OneSheeldMain._NFC; }
            set { }
        }

        public static NotificationShield NOTIFICATION
        {
            get { return OneSheeldMain._NOTIFICATION;  }
            set { }
        }

        public static OrientationSensorShield ORIENTATION
        {
            get { return OneSheeldMain._ORIENTATION;  }
            set { }
        }

        public static PatternShield PATTERN
        {
            get { return OneSheeldMain._PATTERN;  }
            set { }
        }

        public static PhoneShield PHONE
        {
            get { return OneSheeldMain._PHONE;  }
            set { }
        }

        public static PressureSensorShield PRESSURE
        {
            get { return OneSheeldMain._PRESSURE;  }
            set { }
        }

        public static ProximitySensorShield PROXIMITY
        {
            get { return OneSheeldMain._PROXIMITY; }
            set { }
        }

        public static PushButtonShield PUSHBUTTON
        {
            get { return OneSheeldMain._PUSHBUTTON;  }
            set { }
        }

        public static SevenSegmentShield SEVENSEGMENT
        {
            get { return OneSheeldMain._SEVENSEGMENT;  }
            set { }
        }

        public static SkypeShield SKYPE
        {
            get { return OneSheeldMain._SKYPE; }
            set { }
        }

        public static SliderShield SLIDER
        {
            get { return OneSheeldMain._SLIDER;  }
            set { }
        }

        public static SMSShield SMS
        {
            get { return OneSheeldMain._SMS;  }
            set { }
        }

        public static TemperatureSensorShield TEMPERATURE
        {
            get { return OneSheeldMain._TEMPERATURE;  }
            set { }
        }

        public static TerminalShield TERMINAL
        {
            get { return OneSheeldMain._TERMINAL;  }
            set { }
        }

        public static ToggleButtonShield TOGGLEBUTTON
        {
            get { return OneSheeldMain._TOGGLEBUTTON;  }
            set { }
        }

        public static TTSShield TTS
        {
            get { return OneSheeldMain._TTS;  }
            set { }
        }

        public static TwitterShield TWITTER
        {
            get { return OneSheeldMain._TWITTER;  }
            set { }
        }

        public static VibrationShield VIBRATION
        {
            get { return OneSheeldMain._VIBRATION; }
            set { }
        }

        public static VoiceRecognitionShield VOICERECOGNITION
        {
            get { return OneSheeldMain._VOICERECOGNITION;  }
            set { }
        }

        public static OneSheeldClass _OneSheeld = null;

        public OneSheeldClass OneSheeld
        {
            get
            {
                if (_OneSheeld == null)
                {
                    _OneSheeld = new OneSheeldClass();
                    OneSheeldMain.Init(_OneSheeld);
                }
                return _OneSheeld;
            }
            set { }
        }

        public static void Run(IOneSheeldSketch sketch)
        {
            sketch.Setup();
            while (true)
                sketch.Loop();
        }

    }
}
