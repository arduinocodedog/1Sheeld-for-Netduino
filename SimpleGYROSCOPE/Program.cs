namespace SimpleGYROSCOPE
{
    public class Program
    {
        public static void Main()
        {
            // write your code here

            // Callback Version
            OneSheeldClasses.OneSheeldUser.Run(new GyroscopeCallback());

            // Non-Callback Version
            // OneSheeldClasses.OneSheeldUser.Run(new Gyroscope());
        }

    }
}
