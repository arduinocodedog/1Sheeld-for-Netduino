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
    public class GamePad
    {
        OneSheeld sheeld = null;

        public void Setup()
        {
            sheeld = new OneSheeld();
            sheeld.begin();
        }

        public void Loop()
        {
            if (sheeld.GAMEPAD.isUpPressed())
                sheeld.TERMINAL.println("UP Pressed");

            if (sheeld.GAMEPAD.isDownPressed())
                sheeld.TERMINAL.println("DOWN Pressed");

            if (sheeld.GAMEPAD.isLeftPressed())
                sheeld.TERMINAL.println("LEFT Pressed");

            if (sheeld.GAMEPAD.isRightPressed())
                sheeld.TERMINAL.println("RIGHT Pressed");

            if (sheeld.GAMEPAD.isOrangePressed())
                sheeld.TERMINAL.println("ORANGE Pressed");

            if (sheeld.GAMEPAD.isRedPressed())
                sheeld.TERMINAL.println("RED Pressed");

            if (sheeld.GAMEPAD.isGreenPressed())
                sheeld.TERMINAL.println("GREEN Pressed");

            if (sheeld.GAMEPAD.isBluePressed())
                sheeld.TERMINAL.println("BLUE Pressed");

            Thread.Sleep(2000);

        }

    }
}
