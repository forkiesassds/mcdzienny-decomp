using System.IO;

namespace MCDzienny.Misc
{
    public static class FileUtil
    {
        public static bool CreateIfNotExists(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return true;
            }
            return false;
        }

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