using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimpleTEMPERATURE
{
    public class TemperatureCallback : OneSheeldUser, IOneSheeldSketch, ISByteCallback
    {
        OutputPort led = null;

        sbyte value = 0x00;

        public void Setup()
        {
            OneSheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);

            TEMPERATURE.setOnValueChange(this);
        }

        public void Loop()
        {
            if (value < 10)
            {
                led.Write(true);
            }
            else
            {
                led.Write(false);
            }
        }

        //Callback implementation
        public void OnChange(sbyte val)
        {
            value = val;
        }
    }
}
