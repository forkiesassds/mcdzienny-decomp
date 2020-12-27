using System;

namespace MCDzienny
{
    // Token: 0x02000353 RID: 851
    internal sealed class LogFile
    {
        // Token: 0x04000C9F RID: 3231

        // Token: 0x04000C9E RID: 3230

        // Token: 0x06001860 RID: 6240 RVA: 0x000A4DD0 File Offset: 0x000A2FD0
        public LogFile(string path, string fileNameEnding)
        {
            this.Path = path;
            this.FileNameEnding = fileNameEnding;
        }

        // Token: 0x170008EC RID: 2284
        // (get) Token: 0x06001861 RID: 6241 RVA: 0x000A4DE8 File Offset: 0x000A2FE8
        // (set) Token: 0x06001862 RID: 6242 RVA: 0x000A4DF0 File Offset: 0x000A2FF0
        public string Path { get; set; }

        // Token: 0x170008ED RID: 2285
        // (get) Token: 0x06001863 RID: 6243 RVA: 0x000A4DFC File Offset: 0x000A2FFC
        // (set) Token: 0x06001864 RID: 6244 RVA: 0x000A4E04 File Offset: 0x000A3004
        public string FileNameEnding { get; set; }

        // Token: 0x170008EE RID: 2286
        // (get) Token: 0x06001865 RID: 6245 RVA: 0x000A4E10 File Offset: 0x000A3010
        public string GeneratedPath
        {
            get { return Path + DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-") + FileNameEnding; }
        }
    }
}