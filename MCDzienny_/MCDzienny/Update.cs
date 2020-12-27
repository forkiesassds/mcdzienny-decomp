namespace MCDzienny
{
    // Token: 0x02000340 RID: 832
    public class Update
    {
        // Token: 0x04000C45 RID: 3141
        public int b;

        // Token: 0x04000C47 RID: 3143
        public string extraInfo = "";

        // Token: 0x04000C46 RID: 3142
        public byte type;

        // Token: 0x06001805 RID: 6149 RVA: 0x000A0C70 File Offset: 0x0009EE70
        public Update(int b, byte type, string extraInfo = "")
        {
            this.b = b;
            this.type = type;
            this.extraInfo = extraInfo;
        }
    }
}