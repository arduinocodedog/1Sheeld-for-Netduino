using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleORIENTATION
{
    public class OrientationCallback : OneSheeldUser, IOneSheeldSketch, IXYZFloatCallback
    {
        OutputPort led = null;

        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            ORIENTATION.setOnValueChange(this);
        }

        public void Loop()
        {
            if (x > 350 ||
                y > 170 ||
                z > 80)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        //Callback implementation
        public void OnChange(float valueX, float valueY, float valueZ)
        {
            x = valueX;
            y = valueY;
            z = valueZ;
        }
    }
}
