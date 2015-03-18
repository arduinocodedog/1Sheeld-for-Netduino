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
    public class GamePadCallback : OneSheeldUser, IOneSheeldSketch, IGamePadCallback
    {
        bool upButton = false;
        bool downButton = false;
        bool rightButton = false;
        bool leftButton = false;
        bool orangeButton = false;
        bool redButton = false;
        bool greenButton = false;
        bool blueButton = false;

        public void Setup()
        {
            OneSheeld.begin();

            GAMEPAD.setOnButtonChange(this);
        }

        public void Loop()
        {
            if (upButton)
                TERMINAL.println("UP Pressed");

            if (downButton)
                TERMINAL.println("DOWN Pressed");

            if (leftButton)
                TERMINAL.println("LEFT Pressed");

            if (rightButton)
                TERMINAL.println("RIGHT Pressed");

            if (orangeButton)
                TERMINAL.println("ORANGE Pressed");

            if (redButton)
                TERMINAL.println("RED Pressed");

            if (greenButton)
                TERMINAL.println("GREEN Pressed");

            if (blueButton)
                TERMINAL.println("BLUE Pressed");

            Thread.Sleep(2000);
        }

        public void OnButtonChange(bool up, bool down, bool left, bool right, bool orange, bool red, bool green, bool blue)
        {
            upButton = up;
            downButton = down;
            leftButton = left;
            rightButton = right;
            orangeButton = orange;
            redButton = red;
            greenButton = green;
            blueButton = blue;
        }
    }
}
