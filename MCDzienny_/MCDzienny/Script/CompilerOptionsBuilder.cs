using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace MCDzienny.Script
{
    // Token: 0x02000205 RID: 517
    public class CompilerOptionsBuilder
    {
        // Token: 0x04000793 RID: 1939
        private readonly string path;

        // Token: 0x04000794 RID: 1940
        private readonly List<string> references;

        // Token: 0x06000E43 RID: 3651 RVA: 0x0004FC60 File Offset: 0x0004DE60
        public CompilerOptionsBuilder()
        {
            path = RuntimeEnvironment.GetRuntimeDirectory();
            references = new List<string>();
        }

        // Token: 0x06000E44 RID: 3652 RVA: 0x0004FC80 File Offset: 0x0004DE80
        public CompilerOptionsBuilder AddReferenceIfExists(string netDllFileName)
        {
            if (File.Exists(path + netDllFileName)) references.Add(path + netDllFileName);
            return this;
        }

        // Token: 0x06000E45 RID: 3653 RVA: 0x0004FCB0 File Offset: 0x0004DEB0
        public string Build()
        {
            if (references.Count == 0) return "";
            return "/reference:" + string.Join(" /reference:", references.ToArray());
        }
    }
}