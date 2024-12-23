using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using MCDzienny.Gui;
using MCDzienny.Misc;
using Microsoft.CSharp;

namespace MCDzienny
{
    static class Scripting
    {
        const string PathCompilerLogs = "logs/compiler";

        const string PathAutoload = "text/cmdautoload.txt";

        const string CompilationErrorFileName = "errors.txt";

        static readonly CSharpCodeProvider compiler = new CSharpCodeProvider(new Dictionary<string, string>
        {
            {
                "CompilerVersion", "v3.5"
            }
        });

        static CompilerParameters parameters = new CompilerParameters();

        static CompilerResults results;

        public static readonly string PathSource = "extra/commands/source/";

        public static readonly string PathDll = "extra/commands/dll/";

        public static void CreateNew(string cmdName)
        {
            DirectoryUtil.CreateIfNotExists(PathSource);
            using (StreamWriter streamWriter = new StreamWriter(File.Create(PathSource + "Cmd" + cmdName + ".cs")))
            {
                streamWriter.Write("/*" + Environment.NewLine + "\tAuto-generated command skeleton class." + Environment.NewLine + Environment.NewLine +
                                   "\tUse this as a basis for custom commands implemented via the MCDzienny scripting framework." + Environment.NewLine +
                                   "\tFile and class should be named a specific way.  For example, /update is named 'CmdUpdate.cs' for the file, and 'CmdUpdate' for the class." +
                                   Environment.NewLine + "*/" + Environment.NewLine + Environment.NewLine +
                                   "// Add any other using statements you need up here, of course." + Environment.NewLine +
                                   "// As a note, MCDzienny is designed for .NET 3.5." + Environment.NewLine + "using System;" + Environment.NewLine +
                                   Environment.NewLine + "namespace MCDzienny" + Environment.NewLine + "{" + Environment.NewLine + "\tpublic class " + ClassName(cmdName) +
                                   " : Command" + Environment.NewLine + "\t{" + Environment.NewLine +
                                   "\t\t// The command's name, in all lowercase.  What you'll be putting behind the slash when using it." + Environment.NewLine +
                                   "\t\tpublic override string name { get { return \"" + cmdName.ToLower() + "\"; } }" + Environment.NewLine + Environment.NewLine +
                                   "\t\t// Command's shortcut (please take care not to use an existing one, or you may have issues." + Environment.NewLine +
                                   "\t\tpublic override string shortcut { get { return \"\"; } }" + Environment.NewLine + Environment.NewLine +
                                   "\t\t// Determines which submenu the command displays in under /help." + Environment.NewLine +
                                   "\t\tpublic override string type { get { return \"other\"; } }" + Environment.NewLine + Environment.NewLine +
                                   "\t\t// Determines whether or not this command can be used in a museum.  Block/map altering commands should be made false to avoid errors." +
                                   Environment.NewLine + "\t\tpublic override bool museumUsable { get { return false; } }" + Environment.NewLine + Environment.NewLine +
                                   "\t\t// Determines the command's default rank.  Valid values are:" + Environment.NewLine +
                                   "\t\t// LevelPermission.Nobody, LevelPermission.Banned, LevelPermission.Guest" + Environment.NewLine +
                                   "\t\t// LevelPermission.Builder, LevelPermission.AdvBuilder, LevelPermission.Operator, LevelPermission.Admin" + Environment.NewLine +
                                   "\t\tpublic override LevelPermission defaultRank { get { return LevelPermission.Banned; } }" + Environment.NewLine +
                                   Environment.NewLine + "\t\t// This is where the magic happens, naturally." + Environment.NewLine +
                                   "\t\t// p is the player object for the player executing the command.  message is everything after the command invocation itself." +
                                   Environment.NewLine + "\t\tpublic override void Use(Player p, string message)" + Environment.NewLine + "\t\t{" + Environment.NewLine +
                                   "\t\t\tPlayer.SendMessage(p, \"Hello World!\");" + Environment.NewLine + "\t\t}" + Environment.NewLine + Environment.NewLine +
                                   "\t\t// This one controls what happens when you use /help [commandname]." + Environment.NewLine +
                                   "\t\tpublic override void Help(Player p)" + Environment.NewLine + "\t\t{" + Environment.NewLine + "\t\t\tPlayer.SendMessage(p, \"/" +
                                   cmdName.ToLower() + " - Does stuff.  Example command.\");" + Environment.NewLine + "\t\t}" + Environment.NewLine + "\t}" +
                                   Environment.NewLine + "}");
            }
        }

        public static void AddReference(string assembly)
        {
            parameters.ReferencedAssemblies.Add(assembly);
        }

        public static bool Compile(string commandName)
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_000a: Expected O, but got Unknown
            //IL_030e: Unknown result type (might be due to invalid IL or missing references)
            //IL_0315: Expected O, but got Unknown
            parameters = new CompilerParameters();
            DirectoryUtil.CreateIfNotExists("logs/compiler");
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
            string path = "logs/compiler" + Path.DirectorySeparatorChar + "errors.txt";
            string value = Environment.NewLine + new string('-', 25) + Environment.NewLine;
            if (!File.Exists(PathSource + "Cmd" + commandName + ".cs"))
            {
                bool flag = File.Exists(path);
                using (StreamWriter streamWriter = new StreamWriter(path, flag))
                {
                    if (flag)
                    {
                        streamWriter.WriteLine(value);
                    }
                    streamWriter.WriteLine("File not found: Cmd" + commandName + ".cs");
                }
                return false;
            }
            DirectoryUtil.CreateIfNotExists(PathDll);
            parameters.GenerateExecutable = false;
            parameters.IncludeDebugInformation = true;
            parameters.OutputAssembly = PathDll + "Cmd" + commandName + ".dll";
            AddReference("MCDzienny_.dll");
            string path2 = PathSource + "Cmd" + commandName + ".cs";
            string text = File.ReadAllText(path2);
            string[] array = ExtractReferences(text);
            string[] array2 = array;
            foreach (string assembly in array2)
            {
                AddReference(assembly);
            }
            results = compiler.CompileAssemblyFromSource(parameters, text);
            switch (results.Errors.Count)
            {
                case 0:
                    return true;
                case 1:
                {
                    CompilerError val2 = results.Errors[0];
                    bool flag2 = File.Exists(path);
                    StringBuilder stringBuilder = new StringBuilder();
                    if (flag2)
                    {
                        stringBuilder.AppendLine(value);
                    }
                    stringBuilder.AppendLine("Error " + val2.ErrorNumber);
                    stringBuilder.AppendLine("Message: " + val2.ErrorText);
                    stringBuilder.AppendLine("Line: " + val2.Line);
                    using (StreamWriter streamWriter3 = new StreamWriter(path, flag2))
                    {
                        streamWriter3.Write(stringBuilder.ToString());
                    }
                    ShowErrorBox(stringBuilder);
                    return false;
                }
                default:
                {
                    bool flag2 = File.Exists(path);
                    StringBuilder stringBuilder = new StringBuilder();
                    bool flag3 = true;
                    if (flag2)
                    {
                        stringBuilder.AppendLine(value);
                    }
                    foreach (CompilerError item in results.Errors)
                    {
                        CompilerError val = item;
                        if (!flag3)
                        {
                            stringBuilder.AppendLine(value);
                        }
                        stringBuilder.AppendLine("Error #" + val.ErrorNumber);
                        stringBuilder.AppendLine("Message: " + val.ErrorText);
                        stringBuilder.AppendLine("Line: " + val.Line);
                        if (flag3)
                        {
                            flag3 = false;
                        }
                    }
                    using (StreamWriter streamWriter2 = new StreamWriter(path, flag2))
                    {
                        streamWriter2.Write(stringBuilder.ToString());
                    }
                    ShowErrorBox(stringBuilder);
                    return false;
                }
            }
        }

        static string[] ExtractReferences(string cmdContent)
        {
            int num = cmdContent.IndexOfAny(new char[2]
            {
                '\r', '\n'
            });
            string text = num == -1 ? cmdContent : cmdContent.Substring(0, num);
            text = text.Trim();
            if (!text.StartsWith("//"))
            {
                return new string[0];
            }
            if (!text.Contains("References:"))
            {
                return new string[0];
            }
            Regex regex = new Regex("\"([^\"]+[.]dll)\"");
            MatchCollection matchCollection = regex.Matches(text);
            if (matchCollection.Count == 0)
            {
                return new string[0];
            }
            var list = new List<string>();
            foreach (Match item2 in matchCollection)
            {
                string item = item2.Groups[1].Value.Trim();
                list.Add(item);
            }
            return list.ToArray();
        }

        static void ShowErrorBox(StringBuilder sb)
        {
            //IL_0013: Unknown result type (might be due to invalid IL or missing references)
            //IL_0019: Invalid comparison between Unknown and I4
            if (!Server.mono && !Server.CLI && (int)Window.thisWindow.WindowState != 1)
            {
                new PopUpMessage(sb.ToString(), "Error Box", "Compiler errors:").Show();
            }
        }

        public static void Autoload()
        {
            if (FileUtil.CreateIfNotExists("text/cmdautoload.txt"))
            {
                return;
            }
            string[] array = File.ReadAllLines("text/cmdautoload.txt");
            string[] array2 = array;
            foreach (string text in array2)
            {
                if (!(text.Trim() == ""))
                {
                    string text2 = Load("Cmd" + text.ToLower());
                    if (text2 != null)
                    {
                        Server.s.Log(text2);
                    }
                    else
                    {
                        Server.s.Log("AUTOLOAD: Loaded " + text.ToLower() + ".dll");
                    }
                }
            }
        }

        public static string Load(string command)
        {
            if (command.Length < 3 || command.Substring(0, 3).ToLower() != "cmd")
            {
                return "Invalid command name specified.";
            }
            try
            {
                Assembly assembly = null;
                using (FileStream fileStream = File.Open(PathDll + command + ".dll", FileMode.Open))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        int num = 8388608;
                        byte[] buffer = new byte[num];
                        int num2 = 0;
                        while (true)
                        {
                            num2 = fileStream.Read(buffer, 0, num);
                            if (num2 <= 0)
                            {
                                break;
                            }
                            memoryStream.Write(buffer, 0, num2);
                        }
                        assembly = Assembly.Load(memoryStream.ToArray());
                    }
                }
                Type[] types = assembly.GetTypes();
                Type[] array = types;
                foreach (Type type in array)
                {
                    if (type.BaseType == typeof(Command))
                    {
                        object obj = Activator.CreateInstance(type);
                        Command.all.Add((Command)obj);
                        try
                        {
                            ((Command)obj).Init();
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex2)
            {
                Server.ErrorLog(ex2);
                return command + ".dll does not exist in the DLL folder, or is missing a dependency.  Details in the error log.";
            }
            catch (BadImageFormatException ex3)
            {
                Server.ErrorLog(ex3);
                return command + ".dll is not a valid assembly, or has an invalid dependency.  Details in the error log.";
            }
            catch (PathTooLongException)
            {
                return "Class name is too long.";
            }
            catch (FileLoadException ex5)
            {
                Server.ErrorLog(ex5);
                return command + ".dll or one of its dependencies could not be loaded.  Details in the error log.";
            }
            catch (Exception ex6)
            {
                Server.ErrorLog(ex6);
                return "An unknown error occured and has been logged.";
            }
            return null;
        }

        static string ClassName(string name)
        {
            char[] array = name.ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return "Cmd" + new string(array);
        }
    }
}