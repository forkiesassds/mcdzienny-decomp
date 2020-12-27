namespace MCDzienny
{
    // Token: 0x02000341 RID: 833
    public class UpdateHash
    {
        // Token: 0x04000C48 RID: 3144
        public int b;

        // Token: 0x04000C4A RID: 3146
        public string extraInfo = "";

        // Token: 0x04000C49 RID: 3145
        public byte type;

        // Token: 0x06001806 RID: 6150 RVA: 0x000A0C98 File Offset: 0x0009EE98
        public UpdateHash(int b, byte type, string extraInfo = "")
        {
            this.b = b;
            this.type = type;
            this.extraInfo = extraInfo;
        }
    }
}