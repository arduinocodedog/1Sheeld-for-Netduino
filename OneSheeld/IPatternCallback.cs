using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface IPatternCallback
    {
        void OnNewPattern(PatternNode[] nodes, int length);
    }
}
