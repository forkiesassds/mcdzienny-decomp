using System.IO;

namespace MCDzienny.Misc
{
    public class DirectoryUtil
    {
        public static void CreateIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}