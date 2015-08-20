namespace SimpleORIENTATION
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new OrientationCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Orientation());
        }

    }
}
