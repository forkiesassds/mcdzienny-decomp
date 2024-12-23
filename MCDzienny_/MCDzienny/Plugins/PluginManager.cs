using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using MCDzienny.Gui;
using MCDzienny.Script;

namespace MCDzienny.Plugins
{
    public class PluginManager
    {

        public readonly string PluginsPath = "plugins";
        AvailablePluginCollection availablePlugins = new AvailablePluginCollection();

        public AvailablePluginCollection AvailablePlugins { get { return availablePlugins; } set { availablePlugins = value; } }

        public void LoadPlugins()
        {
            LoadPlugins(PluginsPath);
        }

        public void LoadPlugins(string path)
        {
            availablePlugins.Clear();
            string[] files = Directory.GetFiles(path);
            foreach (string text in files)
            {
                FileInfo fileInfo = new FileInfo(text);
                if (fileInfo.Extension.Equals(".dll"))
                {
                    AddPluginFromString(text);
                }
            }
        }

        public void ClosePlugins()
        {
            foreach (AvailablePlugin availablePlugin in availablePlugins)
            {
                availablePlugin.Instance.Terminate();
            }
            availablePlugins.Clear();
        }

        public void RemovePluginByName(string name)
        {
            AvailablePlugin availablePlugin = availablePlugins.SingleOrDefault(p => p.Instance.Name == name);
            if (availablePlugin != null)
            {
                availablePlugin.Instance.Terminate();
                availablePlugins.Remove(availablePlugin);
            }
            Window.thisWindow.RemoveNodeFromPluginList(name);
        }

        public void AddPluginFromString(string sourceCode)
        {
            //IL_0039: Unknown result type (might be due to invalid IL or missing references)
            //IL_003f: Expected O, but got Unknown
            //IL_01e9: Unknown result type (might be due to invalid IL or missing references)
            //IL_01f0: Expected O, but got Unknown
            //IL_0195: Unknown result type (might be due to invalid IL or missing references)
            //IL_019a: Invalid comparison between I4 and Unknown
            CompilerResults val = CompilerManager.Default.CompileFromString(sourceCode);
            if (val.Errors.Count > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (CompilerError item in val.Errors)
                {
                    CompilerError val2 = item;
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine(new string('-', 25));
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("Error #" + val2.ErrorNumber);
                    stringBuilder.AppendLine("Message: " + val2.ErrorText);
                    stringBuilder.AppendLine("Line: " + val2.Line);
                }
                ShowErrorBox(stringBuilder);
                return;
            }
            Assembly compiledAssembly = val.CompiledAssembly;
            Type[] types = compiledAssembly.GetTypes();
            foreach (Type type in types)
            {
                if (!type.IsPublic || type.IsAbstract)
                {
                    continue;
                }
                Type baseType = type.BaseType;
                if (!(baseType.FullName == "MCDzienny.Plugins.Plugin"))
                {
                    continue;
                }
                AvailablePlugin newPlugin = new AvailablePlugin();
                newPlugin.Instance = (Plugin)Activator.CreateInstance(compiledAssembly.GetType(type.ToString()));
                if (availablePlugins.Any(p => p.Instance.Name == newPlugin.Instance.Name))
                {
                    if (6 != (int)MessageBox.Show("Override existing plugin named " + newPlugin.Instance.Name, "Duplicate", (MessageBoxButtons)4))
                    {
                        break;
                    }
                    RemovePluginByName(newPlugin.Instance.Name);
                }
                newPlugin.Instance.Initialize();
                availablePlugins.Add(newPlugin);
                TreeNode node = new TreeNode(newPlugin.Instance.Name);
                Window.thisWindow.AddNodeToPluginList(node);
            }
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

        void AddPlugin(string FileName)
        {
            Assembly assembly = Assembly.LoadFrom(FileName);
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.IsPublic && !type.IsAbstract)
                {
                    Type @interface = type.GetInterface("MCDzienny.Plugin", ignoreCase: true);
                    if (@interface != null)
                    {
                        AvailablePlugin availablePlugin = new AvailablePlugin();
                        availablePlugin.AssemblyPath = FileName;
                        availablePlugin.Instance = (Plugin)Activator.CreateInstance(assembly.GetType(type.ToString()));
                        availablePlugin.Instance.Initialize();
                        availablePlugins.Add(availablePlugin);
                    }
                }
            }
        }
    }
}