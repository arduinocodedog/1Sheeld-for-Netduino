namespace SimpleLIGHT
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new LightCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Light());
        }

    }
}
