using System.Collections;

namespace OneSheeldClasses
{
    public class LCDShield : OneSheeldPrint
    {
        public LCDShield()
            : base(ShieldIds.LCD_ID, LCD_WRITE, LCD_PRINT)
        {
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
	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_CLEAR);
        }

        //Home Setter
        public void home()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_HOME);
        }

        //NoBlink Setter
        public void noBlink()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_NOBLINK);
        }

        //Blink Setter
        public void blink()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_BLINK);
        }

        //NoCursor Setter
        public void noCursor()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_NOCURSOR);
        }

        //Display Cursor Setter
        public void cursor()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_CURSOR);
        }

        //Scrolling Setter
        public void scrollDisplayLeft()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_SCROLLLEFT);
        }

        //Scrolling Setter
        public void scrollDisplayRight()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_SCROLLRIGHT);
        }

        //left-Right Setter
        public void leftToRight()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_LEFT_RIGHT);
        }

        //Right-left Setter
        public void rightToLeft()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_RIGHT_LEFT);
        }

        //AutoScroll Setter
        public void autoScroll()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_AUTOSCROLL);
        }

        //NoAutoScroll Setter
        public void noAutoScroll()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID, 0, LCD_NOAUTOSCROLL);
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


	        OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.LCD_ID,0,LCD_SETCURSOR,2,args);
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
