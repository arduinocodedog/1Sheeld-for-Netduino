namespace SimpleSLIDER
{
    public class Program
    {
        public static void Main()
        {
            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new SliderCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Slider());
        }

    }
}
