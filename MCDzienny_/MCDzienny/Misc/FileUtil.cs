using System.IO;

namespace MCDzienny.Misc
{
    // Token: 0x020001B9 RID: 441
    public static class FileUtil
    {
        // Token: 0x06000C85 RID: 3205 RVA: 0x00048BD4 File Offset: 0x00046DD4
        public static bool CreateIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return true;
            }

            return false;
        }

        // Token: 0x06000C86 RID: 3206 RVA: 0x00048BEC File Offset: 0x00046DEC
        public static bool DeleteIfExists(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }
    }
}