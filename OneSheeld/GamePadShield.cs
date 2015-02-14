using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class GamePadShield
    {
        OneSheeld Sheeld = null;

        bool isCallBackAssigned = false;

        bool up = false;
        bool down = false;
        bool left = false;
        bool right = false;
        bool orange = false;
        bool red = false;
        bool green = false;
        bool blue = false;

        IGamePadCallback buttonChangeCallBack = null;

        public GamePadShield(OneSheeld onesheeld)
        {
            Sheeld = onesheeld;
        }

        //Up ArrowChecker
        public bool isUpPressed()
        {
	        return up;
        }

        //Down Arrow Checker 
        public bool isDownPressed()
        {
	        return down;
        }

        //Left Arrow Checker
        public bool isLeftPressed()
        {
	        return left;
        }

        //Right Arrow Checker
        public bool isRightPressed()
        {
	        return right;
        }

        //Orange Button Checker
        public bool isOrangePressed()
        {
	        return orange;
        }

        //Red Button Checker 
        public bool isRedPressed()
        {
	        return red;
        }

        //Green Button Checker 
        public bool isGreenPressed()
        {
	        return green;
        }

        //Blue Button Checker
        public bool isBluePressed()
        {
	        return blue;
        }

        //GamePad Input Data Processing  
        public void processData()
        {
	        //Checking Function-ID
	        byte functionId = Sheeld.getFunctionId();
	        if (functionId == GAMEPAD_VALUE)
	        {
		        byte value = Sheeld.getArgumentData(0)[0];

                up = (value & (1 << UP_BIT)) != 0;
                down = (value & (1 << DOWN_BIT)) != 0;
                left = (value & (1 << LEFT_BIT)) != 0;
                right = (value & (1 << RIGHT_BIT)) != 0;
                orange = (value & (1 << ORANGE_BIT)) != 0;
                red = (value & (1 << RED_BIT)) != 0;
                green = (value & (1 << GREEN_BIT)) != 0;
                blue = (value & (1 << BLUE_BIT)) != 0;

                //Users Function Invoked
		        if(isCallBackAssigned)
		        {
			        buttonChangeCallBack.OnButtonChange(up, down, left, right, orange, red, green, blue);
		        }
	        }
        }

        //Users Function Setter 
        public void setOnButtonChange(IGamePadCallback userCallback)
        {
	        buttonChangeCallBack = userCallback;
	        isCallBackAssigned = true;
        }

        //Input Function ID
        const byte GAMEPAD_VALUE = 0x01;

        //GamePad Bit Reference 
        const byte ORANGE_BIT = 0;
        const byte RED_BIT = 1;
        const byte GREEN_BIT = 2; 
        const byte BLUE_BIT = 3; 
        const byte UP_BIT = 4;
        const byte DOWN_BIT = 5;
        const byte LEFT_BIT = 6;
        const byte RIGHT_BIT = 7;
    }
}
