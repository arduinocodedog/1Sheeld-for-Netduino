using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedSEVENSEGMENT
{
    class SevenSegment : OneSheeldUser, IOneSheeldSketch
    {
        InputPort button = null;
        byte number = 0;

        public void Setup()
        {
            OneSheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, false, Port.ResistorMode.Disabled);
        }

        public void Loop()
        {
            if (button.Read())
            {
                SEVENSEGMENT.setNumber(number);
                OneSheeld.delay(1000);
                number++;
                if (number > 9)
                    number = 0;
            }
        }
    }
}
