using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class OneSheeldPrint : ShieldParent
    {
        OneSheeld Sheeld = null;
        ShieldIds shieldId = 0x00;
        byte print_fn_id = 0x00;
        byte write_fn_id = 0x00;

        public OneSheeldPrint(OneSheeld onesheeld, ShieldIds shid, byte writefnid, byte printfnid)
            : base(onesheeld, (byte) shid)
        {
            Sheeld = onesheeld;
            shieldId = shid;
            print_fn_id = printfnid;
            write_fn_id = writefnid;
        }

        //Write character 
        public void write(char data)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = (byte)data;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

            Sheeld.sendPacket(shieldId, 0, write_fn_id, 1, args);
        }

        //Print character
        public void print(char data)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = (byte)data;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

            Sheeld.sendPacket(shieldId, 0, print_fn_id, 1, args);
        }

        //Write unsigned byte
        public void write(sbyte data)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = (byte) data;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

	        Sheeld.sendPacket(shieldId,0,write_fn_id,1,args);
        }

        //Print unsigned byte
        public void print(sbyte data)
        {
            ArrayList args = new ArrayList();

            byte[] datas = new byte[1];
            datas[0] = (byte) data;

            FunctionArg arg = new FunctionArg(1, datas);

            args.Add(arg);

	        Sheeld.sendPacket(shieldId,0,print_fn_id,1,args);
        }

        //Print integers
        public void print(int data, byte b)
        {
            ArrayList args = new ArrayList();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas.Length, System.Text.Encoding.UTF8.GetBytes(datas));

            args.Add(arg);

	        Sheeld.sendPacket(shieldId,0,print_fn_id,1,args);
        }

        //Print unsigned integers 
        public void print(uint data, byte b)
        {
           ArrayList args = new ArrayList();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas.Length, System.Text.Encoding.UTF8.GetBytes(datas));

            args.Add(arg);
	
	        Sheeld.sendPacket(shieldId,0,print_fn_id,1,args);
        }

        //Print long integers
        public void print(long data, byte b)
        {
           ArrayList args = new ArrayList();

           string datas = data.ToString();

           FunctionArg arg = new FunctionArg(datas.Length, System.Text.Encoding.UTF8.GetBytes(datas));

            args.Add(arg);
	
	        Sheeld.sendPacket(shieldId,0,print_fn_id,1,args);
        }

        //Print unsigned long integers
        public void print(ulong data , byte b)
        {
            ArrayList args = new ArrayList();

            string datas = data.ToString();

            FunctionArg arg = new FunctionArg(datas.Length, System.Text.Encoding.UTF8.GetBytes(datas));

            args.Add(arg);
	
	        Sheeld.sendPacket(shieldId,0,print_fn_id,1,args);	
        }

        //Print string
        public void print(string stringData)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg = new FunctionArg(stringData.Length, System.Text.Encoding.UTF8.GetBytes(stringData));

            args.Add(arg);

            Sheeld.sendPacket(shieldId, 0, print_fn_id, 1, args);	
        }

        public void print(double data, int precision)
        {
            ArrayList args = new ArrayList();

            string datas = Round(data, precision);

            FunctionArg arg = new FunctionArg(datas.Length, System.Text.Encoding.UTF8.GetBytes(datas));

            args.Add(arg);

            Sheeld.sendPacket(shieldId, 0, print_fn_id, 1, args);
	    }

        public void print(float data, int precision)
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


        public override void processData()
        {
            throw new NotImplementedException();
        }
    }
}
