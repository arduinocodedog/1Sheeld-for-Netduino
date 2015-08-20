namespace SimpleTEMPERATURE
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new TemperatureCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Temperature());
        }

    }
}
