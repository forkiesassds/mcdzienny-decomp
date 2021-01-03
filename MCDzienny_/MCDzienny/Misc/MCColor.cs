namespace MCDzienny.Misc
{
    // Token: 0x020001B5 RID: 437
    public struct MCColor
    {
        // Token: 0x06000C64 RID: 3172 RVA: 0x00048848 File Offset: 0x00046A48
        private MCColor(string name, string code)
        {
            this.name = name;
            this.code = code;
        }

        // Token: 0x170004CF RID: 1231
        // (get) Token: 0x06000C65 RID: 3173 RVA: 0x00048858 File Offset: 0x00046A58
        public static MCColor DarkGreen
        {
            get { return new MCColor("DarkGreen", "%2"); }
        }

        // Token: 0x170004D0 RID: 1232
        // (get) Token: 0x06000C66 RID: 3174 RVA: 0x0004886C File Offset: 0x00046A6C
        public static MCColor DarkTeal
        {
            get { return new MCColor("DarkTeal", "%3"); }
        }

        // Token: 0x170004D1 RID: 1233
        // (get) Token: 0x06000C67 RID: 3175 RVA: 0x00048880 File Offset: 0x00046A80
        public static MCColor DarkRed
        {
            get { return new MCColor("DarkRed", "%4"); }
        }

        // Token: 0x170004D2 RID: 1234
        // (get) Token: 0x06000C68 RID: 3176 RVA: 0x00048894 File Offset: 0x00046A94
        public static MCColor Purple
        {
            get { return new MCColor("Purple", "%5"); }
        }

        // Token: 0x170004D3 RID: 1235
        // (get) Token: 0x06000C69 RID: 3177 RVA: 0x000488A8 File Offset: 0x00046AA8
        public static MCColor Gold
        {
            get { return new MCColor("Gold", "%6"); }
        }

        // Token: 0x170004D4 RID: 1236
        // (get) Token: 0x06000C6A RID: 3178 RVA: 0x000488BC File Offset: 0x00046ABC
        public static MCColor Grey
        {
            get { return new MCColor("Grey", "%7"); }
        }

        // Token: 0x170004D5 RID: 1237
        // (get) Token: 0x06000C6B RID: 3179 RVA: 0x000488D0 File Offset: 0x00046AD0
        public static MCColor DarkGray
        {
            get { return new MCColor("DarkGrey", "%8"); }
        }

        // Token: 0x170004D6 RID: 1238
        // (get) Token: 0x06000C6C RID: 3180 RVA: 0x000488E4 File Offset: 0x00046AE4
        public static MCColor Blue
        {
            get { return new MCColor("Blue", "%9"); }
        }

        // Token: 0x170004D7 RID: 1239
        // (get) Token: 0x06000C6D RID: 3181 RVA: 0x000488F8 File Offset: 0x00046AF8
        public static MCColor Lime
        {
            get { return new MCColor("Lime", "%a"); }
        }

        // Token: 0x170004D8 RID: 1240
        // (get) Token: 0x06000C6E RID: 3182 RVA: 0x0004890C File Offset: 0x00046B0C
        public static MCColor Teal
        {
            get { return new MCColor("Teal", "%b"); }
        }

        // Token: 0x170004D9 RID: 1241
        // (get) Token: 0x06000C6F RID: 3183 RVA: 0x00048920 File Offset: 0x00046B20
        public static MCColor Red
        {
            get { return new MCColor("Red", "%c"); }
        }

        // Token: 0x170004DA RID: 1242
        // (get) Token: 0x06000C70 RID: 3184 RVA: 0x00048934 File Offset: 0x00046B34
        public static MCColor Pink
        {
            get { return new MCColor("Pink", "%d"); }
        }

        // Token: 0x170004DB RID: 1243
        // (get) Token: 0x06000C71 RID: 3185 RVA: 0x00048948 File Offset: 0x00046B48
        public static MCColor Yellow
        {
            get { return new MCColor("Yellow", "%e"); }
        }

        // Token: 0x170004DC RID: 1244
        // (get) Token: 0x06000C72 RID: 3186 RVA: 0x0004895C File Offset: 0x00046B5C
        public static MCColor White
        {
            get { return new MCColor("White", "%f"); }
        }

        // Token: 0x06000C73 RID: 3187 RVA: 0x00048970 File Offset: 0x00046B70
        public override string ToString()
        {
            return code;
        }

        // Token: 0x170004DD RID: 1245
        // (get) Token: 0x06000C74 RID: 3188 RVA: 0x00048978 File Offset: 0x00046B78
        public string Name
        {
            get { return name; }
        }

        // Token: 0x170004DE RID: 1246
        // (get) Token: 0x06000C75 RID: 3189 RVA: 0x00048980 File Offset: 0x00046B80
        public string Code
        {
            get { return code; }
        }

        // Token: 0x06000C76 RID: 3190 RVA: 0x00048988 File Offset: 0x00046B88
        public static implicit operator string(MCColor c)
        {
            return c.code;
        }

        // Token: 0x04000678 RID: 1656
        private readonly string code;

        // Token: 0x04000679 RID: 1657
        private readonly string name;

        // Token: 0x0400067A RID: 1658
        public static readonly MCColor Black = new MCColor("Black", "%0");

        // Token: 0x0400067B RID: 1659
        public static readonly MCColor DarkBlue = new MCColor("DarkBlue", "%1");
    }
}