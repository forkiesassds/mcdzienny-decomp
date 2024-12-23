namespace MCDzienny
{
    public class Update
    {
        public int b;

        public string extraInfo = "";

        public byte type;

        public Update(int b, byte type, string extraInfo = "")
        {
            this.b = b;
            this.type = type;
            this.extraInfo = extraInfo;
        }
    }
}