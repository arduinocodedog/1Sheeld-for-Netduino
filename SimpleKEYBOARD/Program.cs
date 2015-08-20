namespace SimpleKEYBOARD
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new KeyboardCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Keyboard());
        }

    }
}
