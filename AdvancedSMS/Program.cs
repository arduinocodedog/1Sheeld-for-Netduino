namespace AdvancedSMS
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            SMS sms = new SMS();
            sms.Setup();
            while (true)
                sms.Loop();
        }

    }
}
