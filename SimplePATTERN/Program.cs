namespace SimplePATTERN
{
    public class Program
    {
        public static void Main()
        {
            // Don't Use Callback
            OneSheeldClasses.OneSheeldUser.Run(new Pattern());

            // Use Callback
            // OneSheeldClasses.OneSheeldUser.Run(new PatternCallback());
        }

    }
}
