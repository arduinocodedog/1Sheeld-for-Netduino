#define ACCELEROMETER_SHIELD
#define BUZZER_SHIELD
#define CAMERA_SHIELD
#define CLOCK_SHIELD
#define DATA_LOGGER_SHIELD
#define EMAIL_SHIELD
#define FACEBOOK_SHIELD
#define FOURSQUARE_SHIELD
#define GAMEPAD_SHIELD
#define GPS_SHIELD
#define GRAVITY_SHIELD
#define GYROSCOPE_SHIELD
#define INTERNET_SHIELD
#define KEYBOARD_SHIELD
#define KEYPAD_SHIELD
#define LCD_SHIELD
#define LED_SHIELD
#define LIGHT_SHIELD
#define SLIDER_SHIELD
#define PUSH_BUTTON_SHIELD
#define TOGGLE_BUTTON_SHIELD
#define PROXIMITY_SHIELD
#define MAGNETOMETER_SHIELD
#define MIC_SHIELD
#define MUSIC_PLAYER_SHIELD
#define NOTIFICATION_SHIELD
#define TEMPERATURE_SHIELD
#define ORIENTATION_SHIELD
#define PATTERN_SHIELD
#define PHONE_SHIELD
#define PRESSURE_SHIELD
#define REMOTE_SHIELD
#define SEVEN_SEGMENT_SHIELD
#define SKYPE_SHIELD
#define SMS_SHIELD
#define TERMINAL_SHIELD
#define TEXTTOSPEECH_SHIELD
#define TWITTER_SHIELD
#define VOICE_RECOGNITION_SHIELD

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

#if ACCELEROMETER_SHIELD
            _ACCELEROMETER = new AccelerometerSensorShield();
#endif

#if BUZZER_SHIELD
            _BUZZER = new BuzzerShield();
#endif

#if CAMERA_SHIELD
            _CAMERA = new CameraShield();
#endif

#if CLOCK_SHIELD
            _CLOCK = new ClockShield();
#endif

#if DATA_LOGGER_SHIELD
            _DATALOGGER = new DataLoggerShield();
#endif

#if EMAIL_SHIELD
            _EMAIL = new EmailShield();
#endif

#if FACEBOOK_SHIELD
            _FACEBOOK = new FacebookShield();
#endif

#if FOURSQUARE_SHIELD
            _FOURSQUARE = new FoursquareShield();
#endif

#if GAMEPAD_SHIELD
            _GAMEPAD = new GamePadShield();
#endif

#if GPS_SHIELD
            _GPS = new GPSShield();
#endif

#if GRAVITY_SHIELD
            _GRAVITY = new GravitySensorShield();
#endif

#if GYROSCOPE_SHIELD
            _GYROSCOPE = new GyroscopeSensorShield();
#endif

#if INTERNET_SHIELD
            _INTERNET = new InternetShield();
#endif

#if KEYBOARD_SHIELD
            _KEYBOARD = new KeyboardShield();
#endif

#if KEYPAD_SHIELD
            _KEYPAD = new KeypadShield();
#endif

#if LCD_SHIELD
            _LCD = new LCDShield();
#endif

#if LED_SHIELD
            _LED = new LedShield();
#endif

#if LIGHT_SHIELD
            _LIGHT = new LightSensorShield();
#endif

#if MAGNETOMETER_SHIELD
            _MAGNETOMETER = new MagnetometerSensorShield();
#endif

#if MIC_SHIELD
            _MIC = new MicShield();
#endif

#if MUSIC_PLAYER_SHIELD
            _MUSICPLAYER = new MusicPlayerShield();
#endif

#if NOTIFICATION_SHIELD
            _NOTIFICATION = new NotificationShield();
#endif

#if ORIENTATION_SHIELD
            _ORIENTATION = new OrientationSensorShield();
#endif

#if PATTERN_SHIELD
            _PATTERN = new PatternShield();
#endif

#if PHONE_SHIELD
            _PHONE = new PhoneShield();
#endif

#if PRESSURE_SHIELD
            _PRESSURE = new PressureSensorShield();
#endif

#if PROXIMITY_SHIELD
            _PROXIMITY = new ProximitySensorShield();
#endif

#if PUSH_BUTTON_SHIELD
            _PUSHBUTTON = new PushButtonShield();
#endif

#if REMOTE_SHIELD
            OneSheeld.listOfRemoteOneSheelds = new RemoteOneSheeld[MAX_REMOTE_CONNECTIONS];
#endif

#if SEVEN_SEGMENT_SHIELD
            _SEVENSEGMENT = new SevenSegmentShield();
#endif

#if SKYPE_SHIELD
            _SKYPE = new SkypeShield();
#endif

#if SLIDER_SHIELD
            _SLIDER = new SliderShield();
#endif

#if SMS_SHIELD
            _SMS = new SMSShield();
#endif

#if TEMPERATURE_SHIELD
            _TEMPERATURE = new TemperatureSensorShield();
#endif

#if TERMINAL_SHIELD
            _TERMINAL = new TerminalShield();
#endif

#if TEXTTOSPEECH_SHIELD
            _TTS = new TTSShield();
#endif

#if TOGGLE_BUTTON_SHIELD
            _TOGGLEBUTTON = new ToggleButtonShield();
#endif

#if TWITTER_SHIELD
            _TWITTER = new TwitterShield();
#endif

#if VOICE_RECOGNITION_SHIELD
            _VOICERECOGNITION = new VoiceRecognitionShield();
#endif
        }

        // Maximum number of Remote Connections
        const int MAX_REMOTE_CONNECTIONS = 10;
    }
}
