using System;
<<<<<<< HEAD
using System.Collections;
=======
>>>>>>> origin/master
using Microsoft.SPOT;

namespace OneSheeldClasses
{
<<<<<<< HEAD
    public class JsonKeyChain
    {
        OneSheeld Sheeld = null;
        int counter = 0;
        int request = 0;
        JsonKey[] keysArray = new JsonKey[MAX_JSON_KEY_DEPTH];

        public JsonKeyChain(OneSheeld onesheeld)
        {
            counter = 0;
            request = 0;
            Sheeld = onesheeld;
        }

        public JsonKeyChain(OneSheeld onesheeld, int id)
        {
            counter = 0;
            request = id;
            Sheeld = onesheeld;
        }

        public JsonKeyChain(JsonKeyChain old)
        {
            counter = old.counter;
            request = old.request;
            if (counter > 0)
            {
                for (int i = 0; i < counter; i++)
                {
                    keysArray[i] = new JsonKey(old.keysArray[i]);
                }
            }
            Sheeld = old.Sheeld;
        }

        ~JsonKeyChain()
        {
            dispose();
        }

        void dispose()
        {
            for (int i = 0; i < counter; i++)
            {
                keysArray[i] = null;
            }
            keysArray = null;
            counter = 0;
        }

        public static bool operator ==(JsonKeyChain a, JsonKeyChain b)
        {
            if (a.counter != b.counter)
                return false;

            for (int i = 0; i < a.counter; i++)
            {
                if (a.keysArray[i].isString() != b.keysArray[i].isString())
                    return false;

                if (a.keysArray[i].isString() && a.keysArray[i].getString().Equals(b.keysArray[i].getString()))
                    return false;

                if (a.keysArray[i].isNumber() && a.keysArray[i].getNumber().Equals(b.keysArray[i].getNumber()))
                    return false;
            }

            return true;
        }

        public static bool operator !=(JsonKeyChain a, JsonKeyChain b)
        {
            return !(a == b);
        }

        public override bool Equals(object o)
        {
            JsonKeyChain other = o as JsonKeyChain;
            if (other == null)
                return false;

            return (this == other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public JsonKeyChain AddKeytoChain(int key)
        {
            if (counter < MAX_JSON_KEY_DEPTH)
            {
                keysArray[counter++] = new JsonKey(key);
                return this;
            }

            return null;
        }

        public JsonKeyChain AddKeytoChain(string key)
        {
            if (counter < MAX_JSON_KEY_DEPTH)
            {
                keysArray[counter++] = new JsonKey(key);
                return this;
            }
           
            return null;
        }

        public void query()
        {
            sendQueryFrame(INTERNET_QUERY_JSON);
        }

        public void queryArrayLength()
        {
            sendQueryFrame(INTERNET_QUERY_JSON_ARRAY_LENGTH);
        }

        void sendQueryFrame(byte functionId)
        {
            if (counter > 16 || counter == 0 || request == 0)
                return;

            int types = 0;
            FunctionArg[] arguments = new FunctionArg[counter + 2];

            for (int i = 2; i < counter + 2; i++)
            {
                if (keysArray[i - 2].isString())
                    arguments[i] = new FunctionArg(keysArray[i - 2].getString().Length, System.Text.Encoding.UTF8.GetBytes(keysArray[i - 2].getString()));
                else
                {
                    byte[] integerArray = new byte[2];
                    integerArray[1] = (byte)((keysArray[i - 2].getNumber() >> 8) & 0xFF);
                    integerArray[0] = (byte)(keysArray[i-2].getNumber() & 0xFF);
                    arguments[i] = new FunctionArg(2, integerArray);
                }
                int isString = keysArray[i-2].isString() ? 1 : 0;
                types |= ((isString) << (i - 2));
            }

            byte[] typeArray = new byte[2];
            typeArray[1] = (byte) ((types >> 8) & 0xFF);
            typeArray[0] = (byte) (types & 0xFF);
            arguments[1] = new FunctionArg(2, typeArray);

            byte[] requestIdArray = new byte[2];
            requestIdArray[1] = (byte) ((request >> 8) & 0xFF);
            requestIdArray[0] = (byte) (request & 0xFF);
            arguments[0] = new FunctionArg(2, requestIdArray);

            ArrayList args = new ArrayList();

            for (int c = 0; c < counter+2; c++)
            {
                args.Add(arguments[c]);
            }

            Sheeld.sendPacket(ShieldIds.INTERNET_ID, 0, functionId, counter+2, args);

            for (int d = 0; d < counter+2; d++)
            {
                arguments[d] = null;
            }
            arguments = null;
            dispose();
        }

        const int MAX_JSON_KEY_DEPTH = 8;

        const byte INTERNET_QUERY_JSON = 0x14;
        const byte INTERNET_QUERY_JSON_ARRAY_LENGTH = 0x17;
=======
    class JsonKeyChain
    {
>>>>>>> origin/master
    }
}
