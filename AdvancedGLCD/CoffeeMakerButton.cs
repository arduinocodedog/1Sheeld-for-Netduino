using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedGLCD
{
    public class CoffeeMakerButton : GLCDButton, IInteractiveShapeChangeCallback
    {
        OutputPort relay = null;

        bool cmWasON = false;
        bool firstTimePressed = false;


        public CoffeeMakerButton(int x, int y, int w, int h, string dataString)
            : base(x, y, w, h, dataString)
        {
            relay = new OutputPort(Pins.GPIO_PIN_D11, false);

            SetOnChange(this);
        }

        public void OnChange(object data)
        {
            bool coffeeMakerButtonState = (bool) data;

            if (coffeeMakerButtonState)
            {
                if (firstTimePressed)
                {
                    relay.Write(false);
                    setText("CM:ON");
                    cmWasON = true;
                    firstTimePressed = false;
                }
                else
                {
                    relay.Write(true);
                    setText("CM:OFF");
                    cmWasON = false;
                }
            }
            else if (!cmWasON)
            {

                firstTimePressed = true;
            }

        }
    }
}
