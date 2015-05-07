using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace AdvancedCOLOR
{
    public class Color : OneSheeldUser, IOneSheeldSketch,
        ISelectedCallback,
        IColorsCallback
    {
        ulong black = 0L;

        public void Setup()
        {
            OneSheeld.begin();
            COLORDETECTOR.setOnSelected(this);
        }

        public void Loop()
        {
        }

        public void OnSelection()
        {
            COLORDETECTOR.setPalette(ColorShield._1_BIT_GRAYSCALE_PALETTE);
            COLORDETECTOR.enableFullOperation();
            COLORDETECTOR.setCalculationMode(ColorShield.MOST_DOMINANT_COLOR);
            COLORDETECTOR.setOnNewColor(this);
        }

        public void OnColorsReceived(ColorClass upperleft, ColorClass uppermid, ColorClass upperright, ColorClass centerleft, ColorClass centermid, ColorClass centerright, ColorClass lowerleft, ColorClass lowermid, ColorClass lowerright)
        {
            if (upperright == black && upperleft != black)
            {
                TTS.say("Turn Right");
                OneSheeld.delay(300);
            }
            else if (upperleft == black && upperright != black)
            {
                TTS.say("Turn Left");
                OneSheeld.delay(300);
            }
        }
    }
}
