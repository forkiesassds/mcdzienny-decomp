using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using MCDzienny_.Properties;
using Microsoft.CSharp;

namespace MCDzienny.StoreSystem
{
    public class ZombieScriptLoader
    {
        static readonly CSharpCodeProvider compiler = new CSharpCodeProvider(new Dictionary<string, string>
        {
            {
                "CompilerVersion", "v3.5"
            }
        });

        static readonly CompilerParameters parameters = new CompilerParameters();

        static CompilerResults results;

        static readonly string StoreItemsFileName = "ZombieStoreItems";

        static readonly string StoreItemsFilePath = "scripts/" + StoreItemsFileName + ".cs";

        readonly string dllPath = "scripts/dll/";

        readonly string scriptsPath = "scripts/";

        public bool Compile()
        {
            //IL_0236: Unknown result type (might be due to invalid IL or missing references)
            //IL_023d: Expected O, but got Unknown
            if (!Directory.Exists("scripts/dll"))
            {
                Directory.CreateDirectory("scripts/dll");
            }
            if (!Directory.Exists("logs/scripts"))
            {
                Directory.CreateDirectory("logs/scripts");
            }
            if (!File.Exists(StoreItemsFilePath))
            {
                using (StreamWriter streamWriter = new StreamWriter(File.Create(StoreItemsFilePath)))
                {
                    streamWriter.Write(Resources.ZombieStoreItems);
                }
            }
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = dllPath + StoreItemsFileName + ".dll";
            parameters.ReferencedAssemblies.Add("MCDzienny_.dll");
            using (StreamReader streamReader = new StreamReader(scriptsPath + StoreItemsFileName + ".cs"))
            {
                results = compiler.CompileAssemblyFromSource(parameters, streamReader.ReadToEnd());
            }
            string value = new string('-', 25);
            switch (results.Errors.Count)
            {
                case 0:
                    return true;
                case 1:
                {
                    CompilerError val2 = results.Errors[0];
                    bool flag = File.Exists("logs/scripts/errors.txt") ? true : false;
                    StringBuilder stringBuilder = new StringBuilder();
                    if (flag)
                    {
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine(value);
                        stringBuilder.AppendLine();
                    }
                    stringBuilder.AppendLine("Error " + val2.ErrorNumber);
                    stringBuilder.AppendLine("Message: " + val2.ErrorText);
                    stringBuilder.AppendLine("Line: " + val2.Line);
                    using (StreamWriter streamWriter3 = new StreamWriter("logs/scripts/errors.txt", flag))
                    {
                        streamWriter3.Write(stringBuilder.ToString());
                    }
                    return false;
                }
                default:
                {
                    bool flag = File.Exists("logs/scripts/errors.txt") ? true : false;
                    StringBuilder stringBuilder = new StringBuilder();
                    bool flag2 = true;
                    if (flag)
                    {
                        stringBuilder.AppendLine();
                        stringBuilder.AppendLine(value);
                        stringBuilder.AppendLine();
                    }
                    foreach (CompilerError item in results.Errors)
                    {
                        CompilerError val = item;
                        if (!flag2)
                        {
                            stringBuilder.AppendLine();
                            stringBuilder.AppendLine(value);
                            stringBuilder.AppendLine();
                        }
                        stringBuilder.AppendLine("Error #" + val.ErrorNumber);
                        stringBuilder.AppendLine("Message: " + val.ErrorText);
                        stringBuilder.AppendLine("Line: " + val.Line);
                        if (flag2)
                        {
                            flag2 = false;
                        }
                    }
                    using (StreamWriter streamWriter2 = new StreamWriter("logs/scripts/errors.txt", flag))
                    {
                        streamWriter2.Write(stringBuilder.ToString());
                    }
                    return false;
                }
            }
        }

        public string Load()
        {
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                using (FileStream fileStream = File.Open(dllPath + StoreItemsFileName + ".dll", FileMode.Open))
                {
                    int num = 0;
                    byte[] buffer = new byte[8192];
                    while ((num = fileStream.Read(buffer, 0, 8192)) > 0)
                    {
                        memoryStream.Write(buffer, 0, num);
                    }
                }
                Assembly assembly = Assembly.Load(memoryStream.ToArray());
                memoryStream.Close();
                memoryStream.Dispose();
                Type[] types = assembly.GetTypes();
                UseLoadedCode(types);
            }
            catch (FileNotFoundException ex)
            {
                Server.ErrorLog(ex);
                return StoreItemsFileName + ".dll does not exist in the DLL folder, or is missing a dependency.  Details in the error log.";
            }
            catch (BadImageFormatException ex2)
            {
                Server.ErrorLog(ex2);
                return StoreItemsFileName + ".dll is not a valid assembly, or has an invalid dependency.  Details in the error log.";
            }
            catch (PathTooLongException)
            {
                return "Class name is too long.";
            }
            catch (FileLoadException ex4)
            {
                Server.ErrorLog(ex4);
                return StoreItemsFileName + ".dll or one of its dependencies could not be loaded.  Details in the error log.";
            }
            catch (Exception ex5)
            {
                Server.ErrorLog(ex5);
                return "An unknown error occured and has been logged.";
            }
            return null;
        }

        static void UseLoadedCode(Type[] types)
        {
            ZombieStore.storeItems.Clear();
            foreach (Type type in types)
            {
                if (type.BaseType == typeof(Item))
                {
                    object obj = Activator.CreateInstance(type);
                    ZombieStore.storeItems.Add((Item)obj);
                }
            }
        }
    }
}