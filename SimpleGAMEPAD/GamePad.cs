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
            if (OneSheeld.GAMEPAD.isUpPressed())
                OneSheeld.TERMINAL.println("UP Pressed");

            if (OneSheeld.GAMEPAD.isDownPressed())
                OneSheeld.TERMINAL.println("DOWN Pressed");

            if (OneSheeld.GAMEPAD.isLeftPressed())
                OneSheeld.TERMINAL.println("LEFT Pressed");

            if (OneSheeld.GAMEPAD.isRightPressed())
                OneSheeld.TERMINAL.println("RIGHT Pressed");

            if (OneSheeld.GAMEPAD.isOrangePressed())
                OneSheeld.TERMINAL.println("ORANGE Pressed");

            if (OneSheeld.GAMEPAD.isRedPressed())
                OneSheeld.TERMINAL.println("RED Pressed");

            if (OneSheeld.GAMEPAD.isGreenPressed())
                OneSheeld.TERMINAL.println("GREEN Pressed");

            if (OneSheeld.GAMEPAD.isBluePressed())
                OneSheeld.TERMINAL.println("BLUE Pressed");

            Thread.Sleep(2000);

        }

    }
}
