namespace OneSheeldClasses
{
    public class ClockShield : ShieldParent
    {
        byte seconds = 0;
        byte hours = 0;
        byte minutes = 0;
        byte day = 0;
        byte month = 0;
        short year = 0;

        bool isClockInit = false;

        public ClockShield()
            : base(ShieldIds.CLOCK_ID)
        {
        }

        public void queryDateAndTime()
        {
            OneSheeldMain.OneSheeld.sendPacket(ShieldIds.CLOCK_ID,0,CLOCK_BEGIN);
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
	        byte functionId=getOneSheeldInstance().getFunctionId();

	        if(functionId==CLOCK_DATE_VALUE)
	        {
		        byte argumentNumber=getOneSheeldInstance().getArgumentNo();

		        switch(argumentNumber)
		        {
                    case 0x01: seconds = getOneSheeldInstance().getArgumentData(0)[0]; break;

                    case 0x02: seconds = getOneSheeldInstance().getArgumentData(0)[0];
							   minutes = getOneSheeldInstance().getArgumentData(1)[0]; break;

                    case 0x03: seconds = getOneSheeldInstance().getArgumentData(0)[0];
                               minutes = getOneSheeldInstance().getArgumentData(1)[0];
						   	   hours =   getOneSheeldInstance().getArgumentData(2)[0]; break;

                    case 0x04: seconds = getOneSheeldInstance().getArgumentData(0)[0];
                               minutes = getOneSheeldInstance().getArgumentData(1)[0];
                               hours =   getOneSheeldInstance().getArgumentData(2)[0];
						   	   day =     getOneSheeldInstance().getArgumentData(3)[0]; break;

                    case 0x05: seconds = getOneSheeldInstance().getArgumentData(0)[0];
                               minutes = getOneSheeldInstance().getArgumentData(1)[0];
                               hours =   getOneSheeldInstance().getArgumentData(2)[0];
                               day =     getOneSheeldInstance().getArgumentData(3)[0];
						   	   month =   getOneSheeldInstance().getArgumentData(4)[0]; break;

			        case 0x06: isClockInit=true;
                               seconds = getOneSheeldInstance().getArgumentData(0)[0];
                               minutes = getOneSheeldInstance().getArgumentData(1)[0];
                               hours =   getOneSheeldInstance().getArgumentData(2)[0];
                               day =     getOneSheeldInstance().getArgumentData(3)[0];
                               month =   getOneSheeldInstance().getArgumentData(4)[0];

                               ushort uyear = 0;
                               uyear =   (ushort)getOneSheeldInstance().getArgumentData(5)[0];
						   	   uyear |=  (ushort)(getOneSheeldInstance().getArgumentData(5)[1]<<8);
                               year  =   (short)uyear;  break;

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
