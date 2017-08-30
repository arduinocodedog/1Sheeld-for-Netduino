namespace OneSheeldClasses
{
    public class DataLoggerShield : ShieldParent
    {
        public DataLoggerShield()
            :base(ShieldIds.DATA_LOGGER_ID)
        {
        }

        public void start()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_START_LOG);
        }

        public void start(string fileName, bool saveOnOld = false)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(fileName);
            args.Add(arg);

            FunctionArg arg2 = new OneSheeldClasses.FunctionArg(saveOnOld);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_START_LOG, 2, args);
        }

        public void stop()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_STOP_LOG);
        }

        public void add(string key, float value)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg keyarg = new FunctionArg(key);
            args.Add(keyarg);

            FunctionArg floatarg = new FunctionArg(value);
            args.Add(floatarg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_ADD_FLOAT, 2, args);
        }

        public void add(string key, string data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg keyarg = new FunctionArg(key);
            args.Add(keyarg);

            FunctionArg dataarg = new FunctionArg(data);
            args.Add(dataarg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_ADD_STRING, 2, args);
        }

        public void log()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_LOG_DATA);
        }

        //Ouput Function ID's
        const byte LOGGER_START_LOG = 0x01;
        const byte LOGGER_STOP_LOG	= 0x02;
        const byte LOGGER_ADD_FLOAT = 0x03;
        const byte LOGGER_ADD_STRING = 0x04;
        const byte LOGGER_LOG_DATA = 0x05;
    }
}
