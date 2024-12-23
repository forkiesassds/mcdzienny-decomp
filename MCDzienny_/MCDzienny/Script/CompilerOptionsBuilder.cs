using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MCDzienny.Script
{
    public class CompilerOptionsBuilder
    {
        readonly string path;

        readonly List<string> references;

        public CompilerOptionsBuilder()
        {
            path = RuntimeEnvironment.GetRuntimeDirectory();
            references = new List<string>();
        }

        public CompilerOptionsBuilder AddReferenceIfExists(string netDllFileName)
        {
            if (File.Exists(path + netDllFileName))
            {
                references.Add(path + netDllFileName);
            }
            return this;
        }

        public string Build()
        {
            if (references.Count == 0)
            {
                return "";
            }
            return "/reference:" + string.Join(" /reference:", references.ToArray());
        }
    }
}