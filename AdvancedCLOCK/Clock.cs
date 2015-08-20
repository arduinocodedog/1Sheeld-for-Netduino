using System;
using OneSheeldClasses;

namespace AdvancedCLOCK
{
    public class Clock : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            if (!CLOCK.isInitialized())
            {
                TERMINAL.println("Initializing Clock.");
                CLOCK.queryDateAndTime();
            }
            else
            {
                byte hour = CLOCK.getHours();
                byte minute = CLOCK.getMinutes();
                byte second = CLOCK.getSeconds();
                byte day = CLOCK.getDay();
                byte month = CLOCK.getMonth();
                short year = CLOCK.getYear();

                DateTime dt = new DateTime(year, month, day, hour, minute, second);
                TERMINAL.println(dt.ToString());
            }

            OneSheeld.delay(2000);
        }
    }
}
