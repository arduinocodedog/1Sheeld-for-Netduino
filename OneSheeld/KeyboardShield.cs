using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class KeyboardShield : CharInputShield
    {
        public KeyboardShield(OneSheeld onesheeld)
            : base(onesheeld, KEYBOARD_GET_CHAR, ShieldIds.KEYBOARD_ID) { }


        public char getCharacter()
        {
            return character;
        }

        const byte KEYBOARD_GET_CHAR = 0x01;
    }
}
