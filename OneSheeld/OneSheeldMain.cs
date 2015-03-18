using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class OneSheeldMain
    {
        public static AccelerometerSensorShield _ACCELEROMETER = null;
        public static BuzzerShield _BUZZER = null;
        public static CameraShield _CAMERA = null;
        public static ClockShield _CLOCK = null;
        public static DataLoggerShield _DATALOGGER = null;
        public static EmailShield _EMAIL = null;
        public static FacebookShield _FACEBOOK = null;
        public static FoursquareShield _FOURSQUARE = null;
        public static GamePadShield _GAMEPAD = null;
        public static GPSShield _GPS = null;
        public static GravitySensorShield _GRAVITY = null;
        public static GyroscopeSensorShield _GYROSCOPE = null;
        public static InternetShield _INTERNET = null;
        public static KeyboardShield _KEYBOARD = null;
        public static KeypadShield _KEYPAD = null;
        public static LCDShield _LCD = null;
        public static LedShield _LED = null;
        public static LightSensorShield _LIGHT = null;
        public static MagnetometerSensorShield _MAGNETOMETER = null;
        public static MicShield _MIC = null;
        public static MusicPlayerShield _MUSICPLAYER = null;
        public static NotificationShield _NOTIFICATION = null;
        public static OrientationSensorShield _ORIENTATION = null;
        public static PatternShield _PATTERN = null;
        public static PhoneShield _PHONE = null;
        public static PressureSensorShield _PRESSURE = null;
        public static ProximitySensorShield _PROXIMITY = null;
        public static PushButtonShield _PUSHBUTTON = null;
        public static SevenSegmentShield _SEVENSEGMENT = null;
        public static SkypeShield _SKYPE = null;
        public static SliderShield _SLIDER = null;
        public static SMSShield _SMS = null;
        public static TemperatureSensorShield _TEMPERATURE = null;
        public static TerminalShield _TERMINAL = null;
        public static ToggleButtonShield _TOGGLEBUTTON = null;
        public static TTSShield _TTS = null;
        public static TwitterShield _TWITTER = null;
        public static VoiceRecognitionShield _VOICERECOGNITION = null;

        public static OneSheeldClass OneSheeld = null; 

        public static void Init(OneSheeldClass onesheeld)
        {
            OneSheeld = onesheeld;

            _ACCELEROMETER = new AccelerometerSensorShield();
            _BUZZER = new BuzzerShield();
            _CAMERA = new CameraShield();
            _CLOCK = new ClockShield();
            _DATALOGGER = new DataLoggerShield();
            _EMAIL = new EmailShield();
            _FACEBOOK = new FacebookShield();
            _FOURSQUARE = new FoursquareShield();
            _GAMEPAD = new GamePadShield();
            _GPS = new GPSShield();
            _GRAVITY = new GravitySensorShield();
            _GYROSCOPE = new GyroscopeSensorShield();
            _INTERNET = new InternetShield();
            _KEYBOARD = new KeyboardShield();
            _KEYPAD = new KeypadShield();
            _LCD = new LCDShield();
            _LED = new LedShield();
            _LIGHT = new LightSensorShield();
            _MAGNETOMETER = new MagnetometerSensorShield();
            _MIC = new MicShield();
            _MUSICPLAYER = new MusicPlayerShield();
            _NOTIFICATION = new NotificationShield();
            _ORIENTATION = new OrientationSensorShield();
            _PATTERN = new PatternShield();
            _PHONE = new PhoneShield();
            _PRESSURE = new PressureSensorShield();
            _PROXIMITY = new ProximitySensorShield();
            _PUSHBUTTON = new PushButtonShield();
            _SEVENSEGMENT = new SevenSegmentShield();
            _SKYPE = new SkypeShield();
            _SLIDER = new SliderShield();
            _SMS = new SMSShield();
            _TEMPERATURE = new TemperatureSensorShield();
            _TERMINAL = new TerminalShield();
            _TOGGLEBUTTON = new ToggleButtonShield();
            _TTS = new TTSShield();
            _TWITTER = new TwitterShield();
            _VOICERECOGNITION = new VoiceRecognitionShield();
        }


    }
}
