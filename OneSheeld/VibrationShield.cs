using System;
using System.Collections;

namespace OneSheeldClasses
{
    public class VibrationShield : ShieldParent
    {
        public VibrationShield() :
            base(ShieldIds.VIBRATION_ID)
        { }

        public void start(int patternLength, int[] pattern, int repetitionDelay = VIBRATION_NO_REPEAT)
        {
            ArrayList args = new ArrayList();

            byte[] bytepattern = new byte[patternLength * sizeof(short)];
            int patternpos = 0;

            ushort[] ushortPattern = new ushort[patternLength];
            for (int i = 0; i < patternLength; i++)
            {
                ushortPattern[i] = (ushort)pattern[i];

                byte[] bytes = BitConverter.GetBytes(ushortPattern[i]);

                bytepattern[patternpos] = bytes[0];
                patternpos++;
                bytepattern[patternpos] = bytes[1];
                patternpos++;
            }

            FunctionArg arg1 = new FunctionArg(bytepattern);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg((ushort) repetitionDelay);
            args.Add(arg2);
            
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.VIBRATION_ID, 0, VIBRATION_START_PATTERN, 2, args);

            ushortPattern = null;
        }

        public void start(int duration, int repetitionDelay = VIBRATION_NO_REPEAT)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg((ushort) duration);
            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg((ushort) repetitionDelay);
            args.Add(arg2);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.VIBRATION_ID, 0, VIBRATION_START_DURATION, 2, args);
        }

        public void stop()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.VIBRATION_ID, 0, VIBRATION_STOP, 0);
        }


        public const ushort VIBRATION_NO_REPEAT = 65535;

        //Output Function IDs
        const byte VIBRATION_START_PATTERN = 0x01;
        const byte VIBRATION_START_DURATION = 0x02;
        const byte VIBRATION_STOP = 0x03;
    }
}
