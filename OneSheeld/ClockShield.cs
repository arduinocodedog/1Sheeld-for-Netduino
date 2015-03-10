using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class ClockShield : ShieldParent
    {
        OneSheeld Sheeld = null;

        byte seconds = 0;
        byte hours = 0;
        byte minutes = 0;
        byte day = 0;
        byte month = 0;
        short year = 0;

        bool isClockInit = false;

        ulong timeStart = 0L;
        ulong timeCheck = 0L;

        public ClockShield(OneSheeld onesheeld)
            : base(onesheeld, (byte) ShieldIds.CLOCK_ID)
        {
            Sheeld = onesheeld;
        }

        public void begin()
        {
            Sheeld.sendPacket(ShieldIds.CLOCK_ID,0,CLOCK_BEGIN,0,null);
	        timeStart= (ulong)(DateTime.Now.Ticks / 10000L);
	        isClockInit=false;
	        while(!isClockInit)
	        {
		        timeCheck=(ulong)(DateTime.Now.Ticks / 10000L);
                if (timeCheck - timeStart >= ONE_SECOND)
                {
                    break;
                }

		        Sheeld.processInput();
            }		
        }

        public bool isInitialized()
        {
            return isClockInit;
        }

        //Seconds getter 
        public byte getSeconds()
        {
	        return seconds;
        }

        //Minutes getter 
        public byte getMinutes()
        {
	        return minutes;
        }

        //Hours getter 
        public byte getHours()
        {
	        return hours;
        }

        //Day getter 
        public byte getDay()
        {
	        return day;
        }

        //Month getter 
        public byte getMonth()
        {
	        return month;
        }

        //Year getter 
        public short getYear()
        {
	        return year;
        }

        //Clock Input Data Processing 
        public override void processData()
        {
	        //Checking Function-ID
	        byte functionId=Sheeld.getFunctionId();

	        if(functionId==CLOCK_DATE_VALUE)
	        {
		        byte argumentNumber=Sheeld.getArgumentNo();

		        switch(argumentNumber)
		        {
			        case 0x01 	: 	seconds=Sheeld.getArgumentData(0)[0];break;
			
			        case 0x02 	: 	seconds=Sheeld.getArgumentData(0)[0];
							        minutes=Sheeld.getArgumentData(1)[0];break;
			
			        case 0x03	: 	seconds=Sheeld.getArgumentData(0)[0];
						            minutes=Sheeld.getArgumentData(1)[0];
						   	        hours=Sheeld.getArgumentData(2)[0];break;
			
			        case 0x04 	: 	seconds=Sheeld.getArgumentData(0)[0];
						            minutes=Sheeld.getArgumentData(1)[0];
						   	        hours=Sheeld.getArgumentData(2)[0];
						   	        day=Sheeld.getArgumentData(3)[0];break;
			
			        case 0x05 	:	seconds=Sheeld.getArgumentData(0)[0];
						            minutes=Sheeld.getArgumentData(1)[0];
						   	        hours=Sheeld.getArgumentData(2)[0];
						   	        day=Sheeld.getArgumentData(3)[0];
						   	        month=Sheeld.getArgumentData(4)[0];break;

			        case 0x06	:	isClockInit=true;
							        seconds=Sheeld.getArgumentData(0)[0];
						            minutes=Sheeld.getArgumentData(1)[0];
						   	        hours=Sheeld.getArgumentData(2)[0];
						   	        day=Sheeld.getArgumentData(3)[0];
						   	        month=Sheeld.getArgumentData(4)[0];
						   	        year=(short)Sheeld.getArgumentData(5)[0];
						   	        year|=(short)(Sheeld.getArgumentData(5)[1]<<8);break;

		        }
	        }
        }
        
        //Output Function ID
        const byte CLOCK_BEGIN = 0x01;

        //Input Function ID
        const byte CLOCK_DATE_VALUE = 0x01;

        //Number of milliseconds in one second
        const int ONE_SECOND = 1000;
    }
}
