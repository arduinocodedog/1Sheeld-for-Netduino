namespace OneSheeldClasses
{
    public class OneSheeldPrint : ShieldParent
    {
        ShieldIds shieldId = 0x00;
        byte print_fn_id = 0x00;
        byte write_fn_id = 0x00;

        public OneSheeldPrint(ShieldIds shid, byte writefnid, byte printfnid)
            : base(shid)
        {
            shieldId = shid;
            print_fn_id = printfnid;
            write_fn_id = writefnid;
        }

        //Write character 
        public void write(char data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(shieldId, 0, write_fn_id, 1, args);
        }

        //Print character
        public void print(char data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(shieldId, 0, print_fn_id, 1, args);
        }

        //Write unsigned byte
        public void write(sbyte data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(shieldId,0,write_fn_id,1,args);
        }

        //Print unsigned byte
        public void print(sbyte data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(shieldId,0,print_fn_id,1,args);
        }

        //Print integers
        public void print(int data, byte b = DEC)
        {
            FunctionArgs args = new FunctionArgs();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas);
            args.Add(arg);

	        OneSheeldMain.OneSheeld.sendShieldFrame(shieldId,0,print_fn_id,1,args);
        }

        //Print unsigned integers 
        public void print(uint data, byte b = DEC)
        {
            FunctionArgs args = new FunctionArgs();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas);
            args.Add(arg);
	
	        OneSheeldMain.OneSheeld.sendShieldFrame(shieldId,0,print_fn_id,1,args);
        }

        //Print long integers
        public void print(long data, byte b = DEC)
        {
            FunctionArgs args = new FunctionArgs();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas);
            args.Add(arg);
	
	        OneSheeldMain.OneSheeld.sendShieldFrame(shieldId,0,print_fn_id,1,args);
        }

        //Print unsigned long integers
        public void print(ulong data , byte b = DEC)
        {
            FunctionArgs args = new FunctionArgs();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas);
            args.Add(arg);
	
	        OneSheeldMain.OneSheeld.sendShieldFrame(shieldId,0,print_fn_id,1,args);	
        }

        // Print byte Array
        public void print (byte[] data)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(data);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(shieldId, 0, print_fn_id, 1, args);	

        }

        //Print string
        public void print(string stringData)
        {
            FunctionArgs args = new FunctionArgs();

            FunctionArg arg = new FunctionArg(stringData);

            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(shieldId, 0, print_fn_id, 1, args);	
        }

        public void print(double data, int precision = 3)
        {
            FunctionArgs args = new FunctionArgs();

            string datas = Round(data, precision);

            FunctionArg arg = new FunctionArg(datas);
            args.Add(arg);

            OneSheeldMain.OneSheeld.sendShieldFrame(shieldId, 0, print_fn_id, 1, args);
	    }

        public void print(float data, int precision = 3)
        {
            print((double) data, precision);
        }

        protected string Round(double data, int precision)
        {
            string datastr = data.ToString();
            string[] parts = datastr.Split('.');
            string newsecondpart = "";  // just in case we don't have a second part (i.e. no decimal point)

            // if there are two parts, save what we have in the second part
            if (parts.Length == 2)
            {
                newsecondpart = parts[1];
            }

            // if length of the second part is < precesion required add zeros
            while (newsecondpart.Length < precision) 
               newsecondpart += "0";

            // strip and extra digits from the second part
            string precise = newsecondpart.Substring(0, precision);

            // put together a new string that is the precise value
            string newstr = parts[0] + "." + precise;

            // return the value as a double
            return newstr;
        }

        const byte DEC = 10;
    }
}
