namespace MCDzienny.Plugins
{
    // Token: 0x020001F3 RID: 499
    public class versionInfo
    {
        // Token: 0x06000DD0 RID: 3536 RVA: 0x0004DCF4 File Offset: 0x0004BEF4
        public versionInfo(string nameOfAssembly, string description, string author, string version, int versionNumber)
        {
            name = nameOfAssembly;
            descr = description;
            auth = author;
            ver = version;
            verNumber = versionNumber;
        }

        // Token: 0x17000520 RID: 1312
        // (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0004DC90 File Offset: 0x0004BE90
        // (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0004DC98 File Offset: 0x0004BE98
        public string name { get; set; }

        // Token: 0x17000521 RID: 1313
        // (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0004DCA4 File Offset: 0x0004BEA4
        // (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0004DCAC File Offset: 0x0004BEAC
        public string descr { get; set; }

        // Token: 0x17000522 RID: 1314
        // (get) Token: 0x06000DCA RID: 3530 RVA: 0x0004DCB8 File Offset: 0x0004BEB8
        // (set) Token: 0x06000DCB RID: 3531 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
        public string auth { get; set; }

        // Token: 0x17000523 RID: 1315
        // (get) Token: 0x06000DCC RID: 3532 RVA: 0x0004DCCC File Offset: 0x0004BECC
        // (set) Token: 0x06000DCD RID: 3533 RVA: 0x0004DCD4 File Offset: 0x0004BED4
        public string ver { get; set; }

        // Token: 0x17000524 RID: 1316
        // (get) Token: 0x06000DCE RID: 3534 RVA: 0x0004DCE0 File Offset: 0x0004BEE0
        // (set) Token: 0x06000DCF RID: 3535 RVA: 0x0004DCE8 File Offset: 0x0004BEE8
        public int verNumber { get; set; }
    }
}