using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IColorsCallback
    {
        void OnColorsReceived(ColorClass upperleft, ColorClass uppermid, ColorClass upperright, 
                            ColorClass centerleft, ColorClass centermid, ColorClass centerright,
                            ColorClass lowerleft, ColorClass lowermid, ColorClass lowerright);
    }
}
