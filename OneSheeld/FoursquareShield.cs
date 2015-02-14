using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class FoursquareShield
    {
        OneSheeld Sheeld = null;

        public FoursquareShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
        }

        public void checkIn(string placeId, string message)
        {
            ArrayList args = new ArrayList();

            FunctionArg arg1 = new FunctionArg(placeId.Length, System.Text.Encoding.UTF8.GetBytes(placeId));

            args.Add(arg1);

            FunctionArg arg2 = new FunctionArg(message.Length, System.Text.Encoding.UTF8.GetBytes(message));

            args.Add(arg2);

            Sheeld.sendPacket(ShieldIds.FOURSQUARE_ID, 0, FOURSQUARE_CHECK_IN, 2, args);
        }

        const byte FOURSQUARE_CHECK_IN = 0x01;
    }
}
