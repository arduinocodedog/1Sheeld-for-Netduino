using System.Collections;

namespace OneSheeldClasses
{
    public class MicShield : ByteInputShield
    {
        public MicShield()
            : base(MIC_VALUE, ShieldIds.MIC_ID) { }
        
        public void startRecording(string filename = null)
        {
            if (filename == null)
            {
                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MIC_ID, 0, MIC_START_RECORD, 0, null);
            }
            else
            {
                ArrayList args = new ArrayList();

                FunctionArg arg1 = new FunctionArg(filename);
                args.Add(arg1);

                OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MIC_ID, 0, MIC_START_RECORD, 1, args);
            }
        }

        public void stopRecording()
        {
            OneSheeldMain.OneSheeld.sendShieldFrame(ShieldIds.MIC_ID, 0, MIC_STOP_RECORD, 0, null);
        }

        // Input Function IDs
        const byte MIC_VALUE = 0x01;

        // Output Function IDs
        const byte MIC_START_RECORD = 0x01;
        const byte MIC_STOP_RECORD = 0x02;
    }
}
