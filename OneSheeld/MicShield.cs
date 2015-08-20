namespace OneSheeldClasses
{
    public class MicShield : ByteInputShield
    {
        public MicShield()
            : base(MIC_VALUE, ShieldIds.MIC_ID) { }
        
        const byte MIC_VALUE = 0x01;
    }
}
