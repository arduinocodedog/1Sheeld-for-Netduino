using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class DataLoggerShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        public DataLoggerShield(OneSheeld onesheeld)
            :base(onesheeld, ShieldIds.DATA_LOGGER_ID)
        {
            Sheeld = onesheeld;
        }

        public void start()
        {
            Sheeld.sendPacket(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_START_LOG, 0, null);
        }

        public void start(string fileName)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(fileName.Length, System.Text.Encoding.UTF8.GetBytes(fileName));

            args.Add(arg);

            Sheeld.sendPacket(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_START_LOG, 1, args);
        }

        public void stop()
        {
            Sheeld.sendPacket(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_STOP_LOG, 0, null);
        }

        public void add(string key, float value)
        {
            ArrayList args = new ArrayList();

            FunctionArg keyarg = new FunctionArg(key.Length, System.Text.Encoding.UTF8.GetBytes(key));

            args.Add(keyarg);

            FunctionArg floatarg = new FunctionArg(sizeof(float), Sheeld.convertFloatToBytes(value));

            args.Add(floatarg);

            Sheeld.sendPacket(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_ADD_FLOAT, 2, args);
        }

        public void add(string key, string data)
        {
            ArrayList args = new ArrayList();

            FunctionArg keyarg = new FunctionArg(key.Length, System.Text.Encoding.UTF8.GetBytes(key));

            args.Add(keyarg);

            FunctionArg dataarg = new FunctionArg(data.Length, System.Text.Encoding.UTF8.GetBytes(data));

            args.Add(dataarg);

            Sheeld.sendPacket(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_ADD_STRING, 2, args);
        }

        public void log()
        {
            Sheeld.sendPacket(ShieldIds.DATA_LOGGER_ID, 0, LOGGER_LOG_DATA, 0, null);
        }

        //Ouput Function ID's
        const byte LOGGER_START_LOG = 0x01;
        const byte LOGGER_STOP_LOG	= 0x02;
        const byte LOGGER_ADD_FLOAT = 0x03;
        const byte LOGGER_ADD_STRING = 0x04;
        const byte LOGGER_LOG_DATA = 0x05;
    }
}
