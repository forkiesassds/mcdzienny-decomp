using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MCDzienny.Gui;
using MCDzienny.Misc;
using Microsoft.CSharp;

namespace MCDzienny
{
    // Token: 0x0200038F RID: 911
    internal static class Scripting
    {
        // Token: 0x04000E35 RID: 3637
        private const string PathCompilerLogs = "logs/compiler";

        // Token: 0x04000E36 RID: 3638
        private const string PathAutoload = "text/cmdautoload.txt";

        // Token: 0x04000E37 RID: 3639
        private const string CompilationErrorFileName = "errors.txt";

        // Token: 0x04000E38 RID: 3640
        private static readonly CSharpCodeProvider compiler = new CSharpCodeProvider(new Dictionary<string, string>
        {
            {
                "CompilerVersion",
                "v3.5"
            }
        });

        // Token: 0x04000E39 RID: 3641
        private static CompilerParameters parameters = new CompilerParameters();

        // Token: 0x04000E3A RID: 3642
        private static CompilerResults results;

        // Token: 0x04000E3B RID: 3643
        public static readonly string PathSource = "extra/commands/source/";

        // Token: 0x04000E3C RID: 3644
        public static readonly string PathDll = "extra/commands/dll/";

        // Token: 0x06001A01 RID: 6657 RVA: 0x000B6604 File Offset: 0x000B4804
        public static void CreateNew(string cmdName)
        {
            DirectoryUtil.CreateIfNotExists(PathSource);
            using (var streamWriter = new StreamWriter(File.Create(PathSource + "Cmd" + cmdName + ".cs")))
            {
                streamWriter.Write(string.Concat("/*", Environment.NewLine, "\tAuto-generated command skeleton class.",
                    Environment.NewLine, Environment.NewLine,
                    "\tUse this as a basis for custom commands implemented via the MCDzienny scripting framework.",
                    Environment.NewLine,
                    "\tFile and class should be named a specific way.  For example, /update is named 'CmdUpdate.cs' for the file, and 'CmdUpdate' for the class.",
                    Environment.NewLine, "*/", Environment.NewLine, Environment.NewLine,
                    "// Add any other using statements you need up here, of course.", Environment.NewLine,
                    "// As a note, MCDzienny is designed for .NET 3.5.", Environment.NewLine, "using System;",
                    Environment.NewLine, Environment.NewLine, "namespace MCDzienny", Environment.NewLine, "{",
                    Environment.NewLine, "\tpublic class ", ClassName(cmdName), " : Command", Environment.NewLine,
                    "\t{", Environment.NewLine,
                    "\t\t// The command's name, in all lowercase.  What you'll be putting behind the slash when using it.",
                    Environment.NewLine, "\t\tpublic override string name { get { return \"", cmdName.ToLower(),
                    "\"; } }", Environment.NewLine, Environment.NewLine,
                    "\t\t// Command's shortcut (please take care not to use an existing one, or you may have issues.",
                    Environment.NewLine, "\t\tpublic override string shortcut { get { return \"\"; } }",
                    Environment.NewLine, Environment.NewLine,
                    "\t\t// Determines which submenu the command displays in under /help.", Environment.NewLine,
                    "\t\tpublic override string type { get { return \"other\"; } }", Environment.NewLine,
                    Environment.NewLine,
                    "\t\t// Determines whether or not this command can be used in a museum.  Block/map altering commands should be made false to avoid errors.",
                    Environment.NewLine, "\t\tpublic override bool museumUsable { get { return false; } }",
                    Environment.NewLine, Environment.NewLine,
                    "\t\t// Determines the command's default rank.  Valid values are:", Environment.NewLine,
                    "\t\t// LevelPermission.Nobody, LevelPermission.Banned, LevelPermission.Guest", Environment.NewLine,
                    "\t\t// LevelPermission.Builder, LevelPermission.AdvBuilder, LevelPermission.Operator, LevelPermission.Admin",
                    Environment.NewLine,
                    "\t\tpublic override LevelPermission defaultRank { get { return LevelPermission.Banned; } }",
                    Environment.NewLine, Environment.NewLine, "\t\t// This is where the magic happens, naturally.",
                    Environment.NewLine,
                    "\t\t// p is the player object for the player executing the command.  message is everything after the command invocation itself.",
                    Environment.NewLine, "\t\tpublic override void Use(Player p, string message)", Environment.NewLine,
                    "\t\t{", Environment.NewLine, "\t\t\tPlayer.SendMessage(p, \"Hello World!\");", Environment.NewLine,
                    "\t\t}", Environment.NewLine, Environment.NewLine,
                    "\t\t// This one controls what happens when you use /help [commandname].", Environment.NewLine,
                    "\t\tpublic override void Help(Player p)", Environment.NewLine, "\t\t{", Environment.NewLine,
                    "\t\t\tPlayer.SendMessage(p, \"/", cmdName.ToLower(), " - Does stuff.  Example command.\");",
                    Environment.NewLine, "\t\t}", Environment.NewLine, "\t}", Environment.NewLine, "}"));
            }
        }

        // Token: 0x06001A02 RID: 6658 RVA: 0x000B698C File Offset: 0x000B4B8C
        public static void AddReference(string assembly)
        {
            parameters.ReferencedAssemblies.Add(assembly);
        }

        // Token: 0x06001A03 RID: 6659 RVA: 0x000B69A0 File Offset: 0x000B4BA0
        public static bool Compile(string commandName)
        {
            parameters = new CompilerParameters();
            DirectoryUtil.CreateIfNotExists("logs/compiler");
            if (!Server.mono)
                foreach (var assemblyName in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
                    if (!assemblyName.Name.Contains("IRC") && !assemblyName.Name.Contains("Mono") &&
                        !assemblyName.Name.Contains("Socket") && !assemblyName.Name.Contains("log4net") &&
                        !assemblyName.Name.Contains("Json"))
                        AddReference(assemblyName.Name + ".dll");
            var path = "logs/compiler" + Path.DirectorySeparatorChar + "errors.txt";
            var value = Environment.NewLine + new string('-', 25) + Environment.NewLine;
            if (!File.Exists(PathSource + "Cmd" + commandName + ".cs"))
            {
                var flag = File.Exists(path);
                using (var streamWriter = new StreamWriter(path, flag))
                {
                    if (flag) streamWriter.WriteLine(value);
                    streamWriter.WriteLine("File not found: Cmd" + commandName + ".cs");
                }

                return false;
            }

            DirectoryUtil.CreateIfNotExists(PathDll);
            parameters.GenerateExecutable = false;
            parameters.IncludeDebugInformation = true;
            parameters.OutputAssembly = PathDll + "Cmd" + commandName + ".dll";
            AddReference("MCDzienny_.dll");
            var path2 = PathSource + "Cmd" + commandName + ".cs";
            var text = File.ReadAllText(path2);
            var array = ExtractReferences(text);
            foreach (var assembly in array) AddReference(assembly);
            results = compiler.CompileAssemblyFromSource(parameters, text);
            switch (results.Errors.Count)
            {
                case 0:
                    return true;
                case 1:
                {
                    var compilerError = results.Errors[0];
                    var flag2 = File.Exists(path);
                    var stringBuilder = new StringBuilder();
                    if (flag2) stringBuilder.AppendLine(value);
                    stringBuilder.AppendLine("Error " + compilerError.ErrorNumber);
                    stringBuilder.AppendLine("Message: " + compilerError.ErrorText);
                    stringBuilder.AppendLine("Line: " + compilerError.Line);
                    using (var streamWriter2 = new StreamWriter(path, flag2))
                    {
                        streamWriter2.Write(stringBuilder.ToString());
                    }

                    ShowErrorBox(stringBuilder);
                    return false;
                }
                default:
                {
                    var flag2 = File.Exists(path);
                    var stringBuilder = new StringBuilder();
                    var flag3 = true;
                    if (flag2) stringBuilder.AppendLine(value);
                    foreach (var obj in results.Errors)
                    {
                        var compilerError2 = (CompilerError) obj;
                        if (!flag3) stringBuilder.AppendLine(value);
                        stringBuilder.AppendLine("Error #" + compilerError2.ErrorNumber);
                        stringBuilder.AppendLine("Message: " + compilerError2.ErrorText);
                        stringBuilder.AppendLine("Line: " + compilerError2.Line);
                        if (flag3) flag3 = false;
                    }

                    using (var streamWriter3 = new StreamWriter(path, flag2))
                    {
                        streamWriter3.Write(stringBuilder.ToString());
                    }

                    ShowErrorBox(stringBuilder);
                    return false;
                }
            }
        }

        // Token: 0x06001A04 RID: 6660 RVA: 0x000B6DA8 File Offset: 0x000B4FA8
        private static string[] ExtractReferences(string cmdContent)
        {
            var num = cmdContent.IndexOfAny(new[]
            {
                '\r',
                '\n'
            });
            var text = num == -1 ? cmdContent : cmdContent.Substring(0, num);
            text = text.Trim();
            if (!text.StartsWith("//")) return new string[0];
            if (!text.Contains("References:")) return new string[0];
            var regex = new Regex("\"([^\"]+[.]dll)\"");
            var matchCollection = regex.Matches(text);
            if (matchCollection.Count == 0) return new string[0];
            var list = new List<string>();
            foreach (var obj in matchCollection)
            {
                var match = (Match) obj;
                var item = match.Groups[1].Value.Trim();
                list.Add(item);
            }

            return list.ToArray();
        }

        // Token: 0x06001A05 RID: 6661 RVA: 0x000B6EAC File Offset: 0x000B50AC
        private static void ShowErrorBox(StringBuilder sb)
        {
            if (!Server.mono && !Server.CLI && Window.thisWindow.WindowState != FormWindowState.Minimized)
                new PopUpMessage(sb.ToString(), "Error Box", "Compiler errors:").Show();
        }

        // Token: 0x06001A06 RID: 6662 RVA: 0x000B6EE4 File Offset: 0x000B50E4
        public static void Autoload()
        {
            if (FileUtil.CreateIfNotExists("text/cmdautoload.txt")) return;
            var array = File.ReadAllLines("text/cmdautoload.txt");
            foreach (var text in array)
                if (!(text.Trim() == ""))
                {
                    var text2 = Load("Cmd" + text.ToLower());
                    if (text2 != null)
                        Server.s.Log(text2);
                    else
                        Server.s.Log("AUTOLOAD: Loaded " + text.ToLower() + ".dll");
                }
        }

        // Token: 0x06001A07 RID: 6663 RVA: 0x000B6F7C File Offset: 0x000B517C
        public static string Load(string command)
        {
            if (command.Length < 3 || command.Substring(0, 3).ToLower() != "cmd")
                return "Invalid command name specified.";
            try
            {
                Assembly assembly = null;
                using (var fileStream = File.Open(PathDll + command + ".dll", FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        var num = 8388608;
                        var buffer = new byte[num];
                        var num2 = 0;
                        while (true)
                        {
                            num2 = fileStream.Read(buffer, 0, num);
                            if (num2 <= 0) break;
                            memoryStream.Write(buffer, 0, num2);
                        }

                        assembly = Assembly.Load(memoryStream.ToArray());
                    }
                }

                var types = assembly.GetTypes();
                var array = types;
                foreach (var type in array)
                    if (type.BaseType == typeof(Command))
                    {
                        var obj = Activator.CreateInstance(type);
                        Command.all.Add((Command) obj);
                        try
                        {
                            ((Command) obj).Init();
                        }
                        catch (Exception ex)
                        {
                            Server.ErrorLog(ex);
                        }
                    }
            }
            catch (FileNotFoundException ex2)
            {
                Server.ErrorLog(ex2);
                return command +
                       ".dll does not exist in the DLL folder, or is missing a dependency.  Details in the error log.";
            }
            catch (BadImageFormatException ex3)
            {
                Server.ErrorLog(ex3);
                return command +
                       ".dll is not a valid assembly, or has an invalid dependency.  Details in the error log.";
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

        // Token: 0x06001A08 RID: 6664 RVA: 0x000B7160 File Offset: 0x000B5360
        private static string ClassName(string name)
        {
            var array = name.ToCharArray();
            array[0] = char.ToUpper(array[0]);
            return "Cmd" + new string(array);
        }
    }
}