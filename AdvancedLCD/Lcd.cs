using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedLCD
{
    public class Lcd : OneSheeldUser, IOneSheeldSketch
    {
        InputPort button = null;

        public void Setup()
        {
            OneSheeld.begin();

            button = new InputPort(Pins.GPIO_PIN_D11, false, Port.ResistorMode.Disabled);
        }

        public void Loop()
        {
            if (button.Read())
            {
                LCD.begin();
                LCD.blink();
                OneSheeld.delay(5000);
                LCD.noBlink();
                LCD.write('A');
                LCD.write('B');
                LCD.write('C');
                OneSheeld.delay(2000);
                LCD.clear();
                LCD.print("Hello, World!");
                LCD.setCursor(1, 0);
                LCD.print("This is 1Sheeld");
                OneSheeld.delay(10000);
                LCD.clear();
                LCD.print("Closing!");
                OneSheeld.delay(10000);
            }
        }
    }
}
