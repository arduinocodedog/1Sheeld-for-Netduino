using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class JsonKey
    {
        bool isText = false;
        int number = 0;
        string text = null;

        public JsonKey(int key)
        {
            number = key;
            isText = false;
        }

        public JsonKey(string key)
        {
            text = key;
            isText = true;
        }

        public JsonKey(JsonKey old)
        {
            isText = old.isText;
            if (isText)
            {
                text = old.text;
            }
            else
            {
                number = old.number;
            }
        }

        public bool isString()
        {
            return isText;
        }

        public bool isNumber()
        {
            return !isText;
        }

        public string getString()
        {
            return text;
        }

        public int getNumber()
        {
            return number;
        }
    }
}
