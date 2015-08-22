using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedGLCD
{
    public class LightButton1 : GLCDButton, IInteractiveShapeChangeCallback
    {
        OutputPort led = null;

        public LightButton1(int x, int y, int w, int h, string dataString) 
            : base(x, y, w, h, dataString)
        {
            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            SetOnChange(this);
        }

        public void OnChange(object data)
        {
            bool button1State = (bool) data;

            if (button1State)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }
    }
}
