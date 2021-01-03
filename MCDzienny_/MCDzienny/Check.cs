namespace MCDzienny
{
    // Token: 0x0200033F RID: 831
    public class Check
    {
        // Token: 0x04000C42 RID: 3138
        public int b;

        // Token: 0x04000C44 RID: 3140
        public string extraInfo = "";

        // Token: 0x04000C43 RID: 3139
        public byte time;

        // Token: 0x06001804 RID: 6148 RVA: 0x000A0C48 File Offset: 0x0009EE48
        public Check(int b, string extraInfo = "")
        {
            this.b = b;
            time = 0;
            this.extraInfo = extraInfo;
        }
    }
}