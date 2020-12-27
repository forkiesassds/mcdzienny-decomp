using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using MCDzienny.Properties;
using Microsoft.CSharp;

namespace MCDzienny.StoreSystem
{
    // Token: 0x0200023D RID: 573
    public class ZombieScriptLoader
    {
        // Token: 0x0400089A RID: 2202
        private static readonly CSharpCodeProvider compiler = new CSharpCodeProvider(new Dictionary<string, string>
        {
            {
                "CompilerVersion",
                "v3.5"
            }
        });

        // Token: 0x0400089B RID: 2203
        private static readonly CompilerParameters parameters = new CompilerParameters();

        // Token: 0x0400089C RID: 2204
        private static CompilerResults results;

        // Token: 0x0400089F RID: 2207
        private static readonly string StoreItemsFileName = "ZombieStoreItems";

        // Token: 0x040008A0 RID: 2208
        private static readonly string StoreItemsFilePath = "scripts/" + StoreItemsFileName + ".cs";

        // Token: 0x0400089E RID: 2206
        private readonly string dllPath = "scripts/dll/";

        // Token: 0x0400089D RID: 2205
        private readonly string scriptsPath = "scripts/";

        // Token: 0x060010AB RID: 4267 RVA: 0x00056170 File Offset: 0x00054370
        public bool Compile()
        {
            if (!Directory.Exists("scripts/dll")) Directory.CreateDirectory("scripts/dll");
            if (!Directory.Exists("logs/scripts")) Directory.CreateDirectory("logs/scripts");
            if (!File.Exists(StoreItemsFilePath))
                using (var streamWriter = new StreamWriter(File.Create(StoreItemsFilePath)))
                {
                    streamWriter.Write(Resources.ZombieStoreItems);
                }

            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = dllPath + StoreItemsFileName + ".dll";
            parameters.ReferencedAssemblies.Add("MCDzienny_.dll");
            using (var streamReader = new StreamReader(scriptsPath + StoreItemsFileName + ".cs"))
            {
                results = compiler.CompileAssemblyFromSource(parameters, streamReader.ReadToEnd());
            }

            var value = new string('-', 25);
            switch (results.Errors.Count)
            {
                case 0:
                    return true;
                case 1:
                {
                    var compilerError = results.Errors[0];
                    var flag = File.Exists("logs/scripts/errors.txt");
                    var stringBuilder = new StringBuilder();
                    if (flag)
                    {
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine(value);
                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine("Error " + compilerError.ErrorNumber);
                    stringBuilder.AppendLine("Message: " + compilerError.ErrorText);
                    stringBuilder.AppendLine("Line: " + compilerError.Line);
                    using (var streamWriter2 = new StreamWriter("logs/scripts/errors.txt", flag))
                    {
                        streamWriter2.Write(stringBuilder.ToString());
                    }

                    return false;
                }
                default:
                {
                    var flag = File.Exists("logs/scripts/errors.txt");
                    var stringBuilder = new StringBuilder();
                    var flag2 = true;
                    if (flag)
                    {
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine(value);
                        stringBuilder.AppendLine();
                    }

                    foreach (var obj in results.Errors)
                    {
                        var compilerError2 = (CompilerError) obj;
                        if (!flag2)
                        {
                            stringBuilder.AppendLine();
                            stringBuilder.AppendLine(value);
                            stringBuilder.AppendLine();
                        }

                        stringBuilder.AppendLine("Error #" + compilerError2.ErrorNumber);
                        stringBuilder.AppendLine("Message: " + compilerError2.ErrorText);
                        stringBuilder.AppendLine("Line: " + compilerError2.Line);
                        if (flag2) flag2 = false;
                    }

                    using (var streamWriter3 = new StreamWriter("logs/scripts/errors.txt", flag))
                    {
                        streamWriter3.Write(stringBuilder.ToString());
                    }

                    return false;
                }
            }
        }

        // Token: 0x060010AC RID: 4268 RVA: 0x000564BC File Offset: 0x000546BC
        public string Load()
        {
            try
            {
                var memoryStream = new MemoryStream();
                using (var fileStream = File.Open(dllPath + StoreItemsFileName + ".dll", FileMode.Open))
                {
                    var buffer = new byte[8192];
                    int count;
                    while ((count = fileStream.Read(buffer, 0, 8192)) > 0) memoryStream.Write(buffer, 0, count);
                }

                var assembly = Assembly.Load(memoryStream.ToArray());
                memoryStream.Close();
                memoryStream.Dispose();
                var types = assembly.GetTypes();
                UseLoadedCode(types);
            }
            catch (FileNotFoundException ex)
            {
                Server.ErrorLog(ex);
                return StoreItemsFileName +
                       ".dll does not exist in the DLL folder, or is missing a dependency.  Details in the error log.";
            }
            catch (BadImageFormatException ex2)
            {
                Server.ErrorLog(ex2);
                return StoreItemsFileName +
                       ".dll is not a valid assembly, or has an invalid dependency.  Details in the error log.";
            }
            catch (PathTooLongException)
            {
                return "Class name is too long.";
            }
            catch (FileLoadException ex3)
            {
                Server.ErrorLog(ex3);
                return StoreItemsFileName +
                       ".dll or one of its dependencies could not be loaded.  Details in the error log.";
            }
            catch (Exception ex4)
            {
                Server.ErrorLog(ex4);
                return "An unknown error occured and has been logged.";
            }

            return null;
        }

        // Token: 0x060010AD RID: 4269 RVA: 0x0005660C File Offset: 0x0005480C
        private static void UseLoadedCode(Type[] types)
        {
            ZombieStore.storeItems.Clear();
            foreach (var type in types)
                if (type.BaseType == typeof(Item))
                {
                    var obj = Activator.CreateInstance(type);
                    ZombieStore.storeItems.Add((Item) obj);
                }
        }
    }
}