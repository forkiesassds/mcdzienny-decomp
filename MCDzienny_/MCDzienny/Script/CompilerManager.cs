using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;

namespace MCDzienny.Script
{
    public class CompilerManager
    {
        public static CompilerManager Default = new CompilerManager(NetFrameworkVersion.Net3_5);

        readonly CSharpCodeProvider compiler;

        readonly CompilerParameters parameters;

        public CompilerManager(NetFrameworkVersion version)
        {
            //IL_001f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0029: Expected O, but got Unknown
            //IL_002a: Unknown result type (might be due to invalid IL or missing references)
            //IL_0034: Expected O, but got Unknown
            compiler = new CSharpCodeProvider(new Dictionary<string, string>
            {
                {
                    "CompilerVersion", version.GetVersionSignature()
                }
            });
            parameters = new CompilerParameters();
            AddReferencesToParameters(parameters);
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.IncludeDebugInformation = true;
        }

        public CompilerResults CompileFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Path: " + filePath);
            }
            return compiler.CompileAssemblyFromSource(parameters, File.ReadAllText(filePath));
        }

        public CompilerResults CompileFromString(string fileContent)
        {
            return compiler.CompileAssemblyFromSource(parameters, fileContent);
        }

        void AddReferencesToParameters(CompilerParameters parameters)
        {
            if (!Server.mono)
            {
                AssemblyName[] referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                foreach (AssemblyName assemblyName in referencedAssemblies)
                {
                    if (!assemblyName.Name.Contains("IRC") && !assemblyName.Name.Contains("Mono") && !assemblyName.Name.Contains("Socket") &&
                        !assemblyName.Name.Contains("log4net") && !assemblyName.Name.Contains("Json"))
                    {
                        AddReference(assemblyName.Name + ".dll");
                    }
                }
            }
            AddReference("MCDzienny_.dll");
        }

        public void AddReference(string assembly)
        {
            parameters.ReferencedAssemblies.Add(assembly);
        }
    }
}