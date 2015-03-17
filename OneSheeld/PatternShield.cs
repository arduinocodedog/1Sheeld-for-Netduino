using System;
using Microsoft.SPOT;

namespace OneSheeldClasses
{
    public class PatternShield : ShieldParent
    {
        OneSheeld Sheeld = null;
        IPatternCallback userCallback = null;
        bool isCallBackAssigned = false;
        bool isNewPattern = false;
        int length = 0;

        PatternNode[] nodes = null;

        public PatternShield(OneSheeld onesheeld)
<<<<<<< HEAD
            : base (onesheeld, ShieldIds.PATTERN_ID)
=======
            : base (onesheeld, (byte) ShieldIds.PATTERN_ID)
>>>>>>> origin/master
        {
            Sheeld = onesheeld;
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
<<<<<<< HEAD
            byte functionID = getOneSheeldInstance().getFunctionId();
=======
            byte functionID = Sheeld.getFunctionId();
>>>>>>> origin/master

            if (functionID == PATTERN_VALUE)
            {
                isNewPattern = true;
<<<<<<< HEAD
                length = getOneSheeldInstance().getArgumentLength(0);
=======
                length = Sheeld.getArgumentLength(0);
>>>>>>> origin/master
                if (length > MAX_PATTERN_SIZE)
                    return;
                for (int i = 0; i < length; i++)
                {
                    PatternNode node = new PatternNode();
<<<<<<< HEAD
                    node.setValue(getOneSheeldInstance().getArgumentData(0)[i]);
                    nodes[i] = node;
                }

                if (isCallBackAssigned && !isInACallback())
                {
                    enteringACallback();
                    userCallback.OnNewPattern(nodes, length);
                    exitingACallback();
                }
=======
                    node.setValue(Sheeld.getArgumentData(0)[i]);
                    nodes[i] = node;
                }

                if (isCallBackAssigned)
                    userCallback.OnNewPattern(nodes, length);
>>>>>>> origin/master
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
