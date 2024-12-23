namespace MCDzienny
{
    public class UpdateHash
    {
        public int b;

        public string extraInfo = "";

        public byte type;

        public UpdateHash(int b, byte type, string extraInfo = "")
        {
            this.b = b;
            this.type = type;
            this.extraInfo = extraInfo;
        }
    }
}