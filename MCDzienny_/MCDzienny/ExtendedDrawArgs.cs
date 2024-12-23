namespace MCDzienny
{
    public class ExtendedDrawArgs : BasicDrawArgs
    {

        public ExtendedDrawArgs(byte type1, int integer, params int[] integers)
            : base(type1, integer)
        {
            Integers = integers;
        }

        public ExtendedDrawArgs(byte type1, byte type2, int integer, params int[] integers)
            : base(type1, type2, integer)
        {
            Integers = integers;
        }
        public int[] Integers { get; set; }
    }
}