using System.IO;

namespace MCDzienny.Misc
{
    // Token: 0x02000163 RID: 355
    public class DirectoryUtil
    {
        // Token: 0x06000A0C RID: 2572 RVA: 0x00035018 File Offset: 0x00033218
        public static void CreateIfNotExists(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
        }
    }
}