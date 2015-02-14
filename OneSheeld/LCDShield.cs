using System;
using System.Collections;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class LCDShield : OneSheeldPrint
    {
        OneSheeld Sheeld = null;

        public LCDShield(OneSheeld onesheeld)
            : base(onesheeld, ShieldIds.LCD_ID, LCD_WRITE, LCD_PRINT)
        {
            Sheeld = onesheeld;
        }

        public void begin()
        { 
	        clear();
	        noAutoScroll();
	        setCursor(0,0);
	        noCursor();
	        noBlink();
        }

        //Clear Setter
        public void clear()
        {
	        Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_CLEAR, 0, null);
        }

        //Home Setter
        public void home()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_HOME, 0, null);
        }

        //NoBlink Setter
        public void noBlink()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_NOBLINK, 0, null);
        }

        //Blink Setter
        public void blink()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_BLINK, 0, null);
        }

        //NoCursor Setter
        public void noCursor()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_NOCURSOR, 0, null);
        }

        //Display Cursor Setter
        public void cursor()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_CURSOR, 0, null);
        }

        //Scrolling Setter
        public void scrollDisplayLeft()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_SCROLLLEFT, 0, null);
        }

        //Scrolling Setter
        public void scrollDisplayRight()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_SCROLLRIGHT, 0, null);
        }

        //left-Right Setter
        public void leftToRight()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_LEFT_RIGHT, 0, null);
        }

        //Right-left Setter
        public void rightToLeft()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_RIGHT_LEFT, 0, null);
        }

        //AutoScroll Setter
        public void autoScroll()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_AUTOSCROLL, 0, null);
        }

        //NoAutoScroll Setter
        public void noAutoScroll()
        {
            Sheeld.sendPacket(ShieldIds.LCD_ID, 0, LCD_NOAUTOSCROLL, 0, null);
        }

        //Cursor Setter
        public void setCursor(byte x ,byte y)
        {
            ArrayList args = new ArrayList();

            byte[] xarg = new byte[1];
            xarg[0] = x;

            FunctionArg arg1 = new FunctionArg(1, xarg);

            args.Add(arg1);

            byte[] yarg = new byte[1];
            yarg[0] = y;

            FunctionArg arg2 = new FunctionArg(1, yarg);

            args.Add(arg2);


	        Sheeld.sendPacket(ShieldIds.LCD_ID,0,LCD_SETCURSOR,2,args);
        }

        //Output Functions ID's
        const byte LCD_BEGIN = 0x01; 
        const byte LCD_CLEAR = 0x02;
        const byte LCD_HOME = 0x03;
        const byte LCD_NOBLINK = 0x04;
        const byte LCD_BLINK = 0x05;
        const byte LCD_NOCURSOR = 0x06;
        const byte LCD_CURSOR = 0x07;
        const byte LCD_SCROLLLEFT = 0x08;
        const byte LCD_SCROLLRIGHT = 0x09;
        const byte LCD_LEFT_RIGHT = 0x0A;
        const byte LCD_RIGHT_LEFT = 0x0B;
        const byte LCD_AUTOSCROLL = 0x0C;
        const byte LCD_NOAUTOSCROLL = 0x0D;
        const byte LCD_SETCURSOR = 0x0E;
        const byte LCD_WRITE = 0x0F;
        const byte LCD_PRINT = 0x11;
    }
}
