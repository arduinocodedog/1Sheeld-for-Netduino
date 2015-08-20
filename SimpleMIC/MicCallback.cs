using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleMIC
{
    public class MicCallback : OneSheeldUser, IOneSheeldSketch, IByteCallback
    {
        OutputPort led = null;

        byte value = 0x00;
        
        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            MIC.setOnValueChange(this);
        }

        public void Loop()
        {
            if (value > 80)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        public void OnChange(byte val)
        {
            value = val;
        }
    }
}
