using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;

namespace MCDzienny.Script
{
    // Token: 0x02000204 RID: 516
    public class CompilerManager
    {
        // Token: 0x04000790 RID: 1936
        public static CompilerManager Default = new CompilerManager(NetFrameworkVersion.Net3_5);

        // Token: 0x04000792 RID: 1938
        private readonly CSharpCodeProvider compiler;

        // Token: 0x04000791 RID: 1937
        private readonly CompilerParameters parameters;

        // Token: 0x06000E3D RID: 3645 RVA: 0x0004FAA4 File Offset: 0x0004DCA4
        public CompilerManager(NetFrameworkVersion version)
        {
            compiler = new CSharpCodeProvider(new Dictionary<string, string>
            {
                {
                    "CompilerVersion",
                    version.GetVersionSignature()
                }
            });
            parameters = new CompilerParameters();
            AddReferencesToParameters(parameters);
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.IncludeDebugInformation = true;
        }

        // Token: 0x06000E3E RID: 3646 RVA: 0x0004FB18 File Offset: 0x0004DD18
        public CompilerResults CompileFromFile(string filePath)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("Path: " + filePath);
            return compiler.CompileAssemblyFromSource(parameters, File.ReadAllText(filePath));
        }

        // Token: 0x06000E3F RID: 3647 RVA: 0x0004FB60 File Offset: 0x0004DD60
        public CompilerResults CompileFromString(string fileContent)
        {
            return compiler.CompileAssemblyFromSource(parameters, fileContent);
        }

        // Token: 0x06000E40 RID: 3648 RVA: 0x0004FB8C File Offset: 0x0004DD8C
        private void AddReferencesToParameters(CompilerParameters parameters)
        {
            if (!Server.mono)
                foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                    if (!assemblyName.Name.Contains("IRC") && !assemblyName.Name.Contains("Mono") &&
                        !assemblyName.Name.Contains("Socket") && !assemblyName.Name.Contains("log4net") &&
                        !assemblyName.Name.Contains("Json"))
                        AddReference(assemblyName.Name + ".dll");
            AddReference("MCDzienny_.dll");
        }

        // Token: 0x06000E41 RID: 3649 RVA: 0x0004FC3C File Offset: 0x0004DE3C
        public void AddReference(string assembly)
        {
            parameters.ReferencedAssemblies.Add(assembly);
        }
    }
}