namespace SimplePUSHBUTTON
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new PushButtonCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new PushButton());
        }
    }
}
