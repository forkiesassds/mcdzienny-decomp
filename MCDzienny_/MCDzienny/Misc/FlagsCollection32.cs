namespace MCDzienny.Misc
{
    public class FlagsCollection32
    {

        public FlagsCollection32() {}

        public FlagsCollection32(int flagContainer)
        {
            FlagContainer = flagContainer;
        }

        public int FlagContainer { get; set; }

        public bool GetFlag(int pos)
        {
            return (FlagContainer & 1 << pos) == 1 << pos;
        }

        public void SetFlag(int pos, bool flag)
        {
            if (flag)
            {
                FlagContainer |= 1 << pos;
            }
            else
            {
                FlagContainer &= ~(1 << pos);
            }
        }
    }
}