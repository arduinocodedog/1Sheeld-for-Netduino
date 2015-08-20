namespace OneSheeldClasses
{
    public class PatternNode
    {
        byte row;
        byte col;

        public PatternNode() {}

        public PatternNode(byte r, byte c)
        {
            row = r;
            col = c;
        }

        public void setValue(byte val)
        {
            row = (byte)(val & 0x0f);
            col = (byte)((val & 0xf0) >> 4);
        }

        public static bool operator==(PatternNode a, PatternNode b)
        {
            return (a.row == b.row) && (a.col == b.col);
        }

        public static bool operator!=(PatternNode a, PatternNode b)
        {
            return !(a == b);
        }

        public override bool Equals(object o)
        {
            PatternNode other = o as PatternNode;
            if (other == null)
                return false;

            return (this == other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
