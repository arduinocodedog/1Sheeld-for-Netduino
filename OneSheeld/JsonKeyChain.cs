using System.Collections;

namespace OneSheeldClasses
{
    public class JsonKeyChain
    {
        int counter = 0;
        int request = 0;
        JsonKey[] keysArray = new JsonKey[MAX_JSON_KEY_DEPTH];

        public JsonKeyChain()
        {
            counter = 0;
            request = 0;
        }

        public JsonKeyChain(int id)
        {
            counter = 0;
            request = id;
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

        public JsonKeyChain this[int key]
        {
            get { return AddKeytoChain(key); }
            set { }
        }

        public JsonKeyChain this[string key]
        {
            get { return AddKeytoChain(key); }
            set { }
        }


        public JsonKeyChain AddKeytoChain(int key)
        {
            if (counter < MAX_JSON_KEY_DEPTH)
            {
                keysArray[counter++] = new JsonKey(key);
            }

            return this;
        }

        public JsonKeyChain AddKeytoChain(string key)
        {
            if (counter < MAX_JSON_KEY_DEPTH)
            {
                keysArray[counter++] = new JsonKey(key);
            }

            return this;
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
                {
                    arguments[i] = new FunctionArg(keysArray[i - 2].getString());

                    //Debug.Print("Argument # (string):" + i.ToString() + "   Length: " + keysArray[i - 2].getString().Length.ToString() + "   Data: " + keysArray[i - 2].getString());
                }
                else
                {
                    arguments[i] = new FunctionArg(keysArray[i - 2].getNumber());

                    //Debug.Print("Argument # (number):" + i.ToString() + "   Length: 2" + "   Data: " + keysArray[i - 2].getNumber().ToString());
                }
                int isString = keysArray[i-2].isString() ? 1 : 0;
                types |= ((isString) << (i - 2));
            }

            arguments[1] = new FunctionArg(types);
            //Debug.Print("Argument #: 1   Types: " + types.ToString() + "   Length: 2");

            arguments[0] = new FunctionArg(request);
            //Debug.Print("Argument #: 0   request: " + request.ToString() + "   Length: 2");

            ArrayList args = new ArrayList();

            for (int c = 0; c < counter+2; c++)
            {
                args.Add(arguments[c]);
            }

            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.INTERNET_ID, 0, functionId, counter+2, args);

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
    }
}
