using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;

namespace SimplePATTERN
{
    public class Pattern
    {
        OneSheeld sheeld = null;
        OutputPort led = null;

        // Initialize the pattern we want to see
        PatternNode[] patternStored = 
        {   
            new PatternNode(0, 0),   // Start in Upper Left              The Pattern we are looking for
            new PatternNode(1, 0),   // Go Down One                                 .
            new PatternNode(2, 0),   // Go Down Another                             .
            new PatternNode(2, 1),   // Go Right One                                . . .
            new PatternNode(2, 2)    // Go Right Another
        };

        int counter = 0;
        int length = 0;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();

            led = new OutputPort(Pins.GPIO_PIN_D13, false);
        }

        public void Loop()
        {
            if (sheeld.PATTERN.isNewPatternReceived())
            {
                PatternNode[] patternEntered = sheeld.PATTERN.getLastPattern();

                length = sheeld.PATTERN.getLastPatternLength();

                if (length == 5)
                {
                    for (int i = 0; i < length; i++)
                    {
                        // Orginal Code checks .row and .col for equality
                        // But, isn't that what the overloaded operators are for???
                        if (patternEntered[i] == patternStored[i]) 
                        {
                            counter++;
                        }
                    }

                    if (counter == 5)
                    {
                        led.Write(true);
                        counter = 0;
                    }
                    else
                    {
                        led.Write(false);
                        counter = 0;
                    }
                }
                else
                {
                    led.Write(false);
                }
            }
        }
    }
}
