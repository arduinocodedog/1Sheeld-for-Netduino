namespace SimpleMIC
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new MicCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Mic());
        }

    }
}
