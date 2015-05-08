using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleCOLOR
{
    public class Color : OneSheeldUser, IOneSheeldSketch,
        ISelectedCallback
    {
        PWM red = null;
        PWM green = null;
        PWM blue = null;

        public void Setup()
        {
            red = new PWM(PWMChannels.PWM_PIN_D9, 100.0, 0.0, false);
            red.Start();
            green = new PWM(PWMChannels.PWM_PIN_D10, 100.0, 0.0, false);
            green.Start();
            blue = new PWM(PWMChannels.PWM_PIN_D11, 100.0, 0.0, false);
            blue.Start();

            OneSheeld.begin();

            COLORDETECTOR.setOnSelected(this);
        }

        public void OnSelection()
        {
            COLORDETECTOR.setPalette(ColorShield._3_BIT_RGB_PALETTE);
        }

        public void Loop()
        {
            if (COLORDETECTOR.isNewColorReceived())
            {
                /* Read the last received color and save it locally. */
                ColorClass readColor = COLORDETECTOR.getLastColor();

                /* Get red, blue and green components values. */
                byte redValue = readColor.getRed();
                byte greenValue = readColor.getGreen();
                byte blueValue = readColor.getBlue();

                int redmapped = (int)map(redValue, 0, 255, 0, 100);
                int greenmapped = (int)map(greenValue, 0, 255, 0, 100);
                int bluemapped = (int)map(blueValue, 0, 255, 0, 100);

                /* Output the values on the RGB LED pins. */
                double redDutyCycle = (double)(redmapped / 100.0);
                red.DutyCycle = redDutyCycle;
                double greenDutyCycle = (double)(greenmapped / 100.0);
                green.DutyCycle = greenDutyCycle;
                double blueDutyCycle = (double)(bluemapped / 100.0);
                blue.DutyCycle = blueDutyCycle;
            }
        }

        private long map(long x, long in_min, long in_max, long out_min, long out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
        }
    }
}
