using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedGLCD
{
    public class LightButton2 : GLCDButton, IInteractiveShapeChangeCallback
    {
        OutputPort led = null;

        public LightButton2(int x, int y, int w, int h, string dataString)
            : base(x, y, w, h, dataString)
        {
            led = new OutputPort(Pins.GPIO_PIN_D12, false);

            SetOnChange(this);
        }

        public void OnChange(byte button2State)
        {
            if (button2State != 0)
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
