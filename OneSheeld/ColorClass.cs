namespace OneSheeldClasses
{
    public class ColorClass
    {
        ulong rgb = 0;
	    ulong hsb = 0;

        public ColorClass(ulong _rgb)
        {
            rgb = _rgb;
	        hsb = ColorShield.convertRgbToHsb(rgb);
        }

        public ColorClass()
        {
        }

        public void setColor(ulong _rgb)
        {
            rgb = _rgb;
            hsb = ColorShield.convertRgbToHsb(rgb);
        }

        public static bool operator==(ColorClass a, ColorClass b)
	    {
	        return (a.rgb==b.rgb)||(a.hsb==b.hsb);
	    }

	    public static bool operator!=(ColorClass a, ColorClass b)
	    {
		    return !(a == b);
	    }

	    public static bool operator==(ColorClass a, ulong _b) 
	    {
            ColorClass b = new ColorClass(_b);
	        return a == b;
	    }

	    public static bool operator!=(ColorClass a, ulong b) 
	    {
	        return !(a == b);
	    }

        public override bool Equals(object o)
        {
            ColorClass other = o as ColorClass;
            if (other == null)
                return false;

            return (this == other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public byte getRed()
	    {
		    return (byte)(rgb>>16);
	    }

        public byte getGreen()
	    {
		    return (byte)(rgb>>8);
	    }

        public byte getBlue()
	    {
		    return (byte)(rgb);
	    }

        public uint getHue()
	    {
		    return (uint)(hsb>>16);
	    }

        public byte getSaturation()
	    {
		    return (byte)(hsb>>8);
	    }
	
	    public byte getBrightness()
	    {
		    return (byte)hsb;
	    }
    }
}
