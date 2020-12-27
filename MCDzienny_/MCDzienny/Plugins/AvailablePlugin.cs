namespace MCDzienny.Plugins
{
    // Token: 0x020001E0 RID: 480
    public class AvailablePlugin
    {
        // Token: 0x04000705 RID: 1797
        private string myAssemblyPath = string.Empty;

        // Token: 0x04000704 RID: 1796

        // Token: 0x170004FE RID: 1278
        // (get) Token: 0x06000D5D RID: 3421 RVA: 0x0004C58C File Offset: 0x0004A78C
        // (set) Token: 0x06000D5E RID: 3422 RVA: 0x0004C594 File Offset: 0x0004A794
        public Plugin Instance { get; set; }

        // Token: 0x170004FF RID: 1279
        // (get) Token: 0x06000D5F RID: 3423 RVA: 0x0004C5A0 File Offset: 0x0004A7A0
        // (set) Token: 0x06000D60 RID: 3424 RVA: 0x0004C5A8 File Offset: 0x0004A7A8
        public string AssemblyPath
        {
            get { return myAssemblyPath; }
            set { myAssemblyPath = value; }
        }

        // Token: 0x17000500 RID: 1280
        // (get) Token: 0x06000D61 RID: 3425 RVA: 0x0004C5B4 File Offset: 0x0004A7B4
        // (set) Token: 0x06000D62 RID: 3426 RVA: 0x0004C5BC File Offset: 0x0004A7BC
        public bool IsCore { get; set; }
    }
}