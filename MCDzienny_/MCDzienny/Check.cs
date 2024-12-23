namespace MCDzienny
{
    public class Check
    {
        public int b;

        public string extraInfo = "";

        public byte time;

        public Check(int b, string extraInfo = "")
        {
            this.b = b;
            time = 0;
            this.extraInfo = extraInfo;
        }
    }
}