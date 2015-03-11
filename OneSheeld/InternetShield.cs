using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class InternetShield : ShieldParent 
    {
        OneSheeld Sheeld = null;

        public InternetShield(OneSheeld onesheeld)
            :base(onesheeld, (byte) ShieldIds.INTERNET_ID)
        {
            Sheeld = onesheeld;
        }

        public override void processData()
        {
            throw new NotImplementedException();
        }
    }
}
