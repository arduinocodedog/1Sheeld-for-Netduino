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
    public class GamePadCallback : IGamePadCallback
    {
        OneSheeld sheeld = null;

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
            sheeld = new OneSheeld();
            sheeld.begin();

            sheeld.GAMEPAD.setOnButtonChange(this);
        }

        public void Loop()
        {
            if (upButton)
               sheeld.TERMINAL.println("UP Pressed");

            if (downButton)
                sheeld.TERMINAL.println("DOWN Pressed");

            if (leftButton)
                sheeld.TERMINAL.println("LEFT Pressed");

            if (rightButton)
                sheeld.TERMINAL.println("RIGHT Pressed");

            if (orangeButton)
                sheeld.TERMINAL.println("ORANGE Pressed");

            if (redButton)
                sheeld.TERMINAL.println("RED Pressed");

            if (greenButton)
                sheeld.TERMINAL.println("GREEN Pressed");

            if (blueButton)
                sheeld.TERMINAL.println("BLUE Pressed");

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
