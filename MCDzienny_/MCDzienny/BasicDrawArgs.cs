namespace MCDzienny
{
    public class BasicDrawArgs : DrawArgs
    {

        public BasicDrawArgs(byte type1)
            : this(type1, byte.MaxValue, 0) {}

        public BasicDrawArgs(byte type1, byte type2)
            : this(type1, type2, 0) {}

        public BasicDrawArgs(byte type1, int number1)
            : this(type1, byte.MaxValue, number1) {}

        public BasicDrawArgs(byte type1, byte type2, int number1)
        {
            Type1 = type1;
            Type2 = type2;
            Integer = number1;
        }
        public byte Type1 { get; set; }

        public byte Type2 { get; set; }

        public int Integer { get; set; }
    }
}