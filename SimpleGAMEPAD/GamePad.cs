using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using OneSheeldClasses;


namespace SimpleGAMEPAD
{
    public class GamePad : OneSheeldUser, IOneSheeldSketch
    {
        public void Setup()
        {
            OneSheeld.begin();
        }

        public void Loop()
        {
            if (GAMEPAD.isUpPressed())
                TERMINAL.println("UP Pressed");

            if (GAMEPAD.isDownPressed())
                TERMINAL.println("DOWN Pressed");

            if (GAMEPAD.isLeftPressed())
                TERMINAL.println("LEFT Pressed");

            if (GAMEPAD.isRightPressed())
                TERMINAL.println("RIGHT Pressed");

            if (GAMEPAD.isOrangePressed())
                TERMINAL.println("ORANGE Pressed");

            if (GAMEPAD.isRedPressed())
                TERMINAL.println("RED Pressed");

            if (GAMEPAD.isGreenPressed())
                TERMINAL.println("GREEN Pressed");

            if (GAMEPAD.isBluePressed())
                TERMINAL.println("BLUE Pressed");

            OneSheeld.delay(2000);

        }

    }
}
