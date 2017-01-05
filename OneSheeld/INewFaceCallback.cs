using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public interface INewFaceCallback
    {
        void OnNewFace(Face myFace);
    }
}
