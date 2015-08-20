namespace OneSheeldClasses
{
    public class PatternShield : ShieldParent
    {
        IPatternCallback userCallback = null;
        bool isCallBackAssigned = false;
        bool isNewPattern = false;
        int length = 0;

        PatternNode[] nodes = null;

        public PatternShield()
            : base (ShieldIds.PATTERN_ID)
        {
            nodes = new PatternNode[MAX_PATTERN_SIZE];
        }

        public PatternNode[] getLastPattern()
        {
            isNewPattern = false;
            return nodes;
        }

        public int getLastPatternLength()
        {
            return length;
        }

        public bool isNewPatternReceived()
        {
            return isNewPattern;
        }

        public override void processData()
        {
            byte functionID = getOneSheeldInstance().getFunctionId();

            if (functionID == PATTERN_VALUE)
            {
                isNewPattern = true;
                length = getOneSheeldInstance().getArgumentLength(0);
                if (length > MAX_PATTERN_SIZE)
                    return;
                for (int i = 0; i < length; i++)
                {
                    PatternNode node = new PatternNode();
                    node.setValue(getOneSheeldInstance().getArgumentData(0)[i]);
                    nodes[i] = node;
                }

                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    userCallback.OnNewPattern(nodes, length);
                    exitingACallback();
                }
            }

        }

        public void SetOnNewPattern(IPatternCallback callback)
        {
            userCallback = callback;
            isCallBackAssigned = true;
        }

        const byte PATTERN_VALUE = 0x01;
        const byte MAX_PATTERN_SIZE = 9;
    }
}
