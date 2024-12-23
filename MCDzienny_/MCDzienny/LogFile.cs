using System;

namespace MCDzienny
{
    sealed class LogFile
    {

        public LogFile(string path, string fileNameEnding)
        {
            Path = path;
            FileNameEnding = fileNameEnding;
        }

        public string Path { get; set; }

        public string FileNameEnding { get; set; }

        public string GeneratedPath { get { return Path + DateTime.Now.ToString("yyyy-MM-dd").Replace("/", "-") + FileNameEnding; } }
    }
}