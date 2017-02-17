namespace OneSheeldClasses
{
    public class ChartShield : ShieldParent
    {
        private byte keysCounter = 0;
        private string key = null;
        private string[] namesArray = null;
        private float[] floatArray = null;
        private byte[] chartIDArray = null;

        //Constructor
        public ChartShield()
            :base(ShieldIds.CHART_ID)
        {
            namesArray = new string[5];
            floatArray = new float[5];
            chartIDArray = new byte[5];

            for (int i = 0; i < 5; i++)
            {
                namesArray[i] = null;
                floatArray[i] = 0;
                chartIDArray[i] = 0;
            }
        }

        public void add(string _key, float _value, byte _chartID = 0)
        {
            bool found = false;
            if(_chartID>=5)
            {
                _chartID=4;
            }

            for(int i = 0; i<keysCounter; i++)
            {
                if(!namesArray[i].Equals(_key) && chartIDArray[i]==_chartID)
                {
                    floatArray[i]=_value;
                    found=true;
                }
            }

            int keyLength = _key.Length;
            if(keyLength == 0 || keysCounter >= 5) return;	

            if(!found)
            {
                key = _key;
                namesArray[keysCounter]=key;
                floatArray[keysCounter]=_value;
                chartIDArray[keysCounter]=_chartID;
                keysCounter++;
            }
        }

        public void plot()
        {
            if (keysCounter > 0)
            {
                FunctionArgs args = new FunctionArgs();
                byte stepCounter = 0;
                for (int i = 0; i < keysCounter * 3; i += 3)
                {
                    FunctionArg arg = new FunctionArg(namesArray[stepCounter]);
                    args.Add(arg);

                    FunctionArg arg2 = new FunctionArg(floatArray[stepCounter]);
                    args.Add(arg2);

                    FunctionArg arg3 = new FunctionArg(chartIDArray[stepCounter]);
                    args.Add(arg3);

                    stepCounter++;
                }
             
                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CHART_ID, 0, CHART_PLOT, keysCounter * 3, args);

                for (int i = 0; i < keysCounter; i++)
                {
                   namesArray[i] = null;
                   floatArray[i] = 0;
                   chartIDArray[i] = 0;
                }

                keysCounter = 0;
            }
        }

        public void saveCsv(string fileName, byte _chartID)
        {
            if (_chartID < 5)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg = new FunctionArg(fileName);
                args.Add(arg);

                FunctionArg arg2 = new FunctionArg(_chartID);
                args.Add(arg2);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CHART_ID, 0, CHART_SAVE_CSV, 2, args);
            }
        }

        public void saveScreenshot(byte _chartID)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(_chartID);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CHART_ID, 0, CHART_SAVE_SCREENSHOT, 1, args);
        }

        public void clear(byte _chartID)
        {
            if (_chartID < 5)
            {
                FunctionArgs args = new FunctionArgs();

                FunctionArg arg = new FunctionArg(_chartID);
                args.Add(arg);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CHART_ID, 0, CHART_CLEAR, 1, args);
            }
        }

        public void autoScroll(byte state)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(state);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.CHART_ID, 0, CHART_AUTO_SCROLL, 1, args);
        }

        //Output Function ID
        const byte CHART_PLOT = 0x01;
        const byte CHART_CLEAR = 0x02;
        const byte CHART_AUTO_SCROLL = 0x03;
        const byte CHART_SAVE_CSV = 0x04;
        const byte CHART_SAVE_SCREENSHOT = 0x05;

        //Input Function ID

        //Literals
        public byte CHART_0 = 0x00;
        public byte CHART_1 = 0x01;
        public byte CHART_2 = 0x02;
        public byte CHART_3 = 0x03;
        public byte CHART_4 = 0x04;
    }
}
