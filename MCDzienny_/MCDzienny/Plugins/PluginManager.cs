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
    // Token: 0x020001ED RID: 493
    public class PluginManager
    {
        // Token: 0x0400072F RID: 1839
        public readonly string PluginsPath = "plugins";

        // Token: 0x0400072E RID: 1838
        private AvailablePluginCollection availablePlugins = new AvailablePluginCollection();

        // Token: 0x17000513 RID: 1299
        // (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0004D668 File Offset: 0x0004B868
        // (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0004D670 File Offset: 0x0004B870
        public AvailablePluginCollection AvailablePlugins
        {
            get { return availablePlugins; }
            set { availablePlugins = value; }
        }

        // Token: 0x06000DA7 RID: 3495 RVA: 0x0004D69C File Offset: 0x0004B89C
        public void LoadPlugins()
        {
            LoadPlugins(PluginsPath);
        }

        // Token: 0x06000DA8 RID: 3496 RVA: 0x0004D6AC File Offset: 0x0004B8AC
        public void LoadPlugins(string path)
        {
            availablePlugins.Clear();
            foreach (var text in Directory.GetFiles(path))
            {
                var fileInfo = new FileInfo(text);
                if (fileInfo.Extension.Equals(".dll")) AddPluginFromString(text);
            }
        }

        // Token: 0x06000DA9 RID: 3497 RVA: 0x0004D700 File Offset: 0x0004B900
        public void ClosePlugins()
        {
            foreach (var availablePlugin in availablePlugins) availablePlugin.Instance.Terminate();
            availablePlugins.Clear();
        }

        // Token: 0x06000DAA RID: 3498 RVA: 0x0004D764 File Offset: 0x0004B964
        public void RemovePluginByName(string name)
        {
            var availablePlugin = availablePlugins.SingleOrDefault(p => p.Instance.Name == name);
            if (availablePlugin != null)
            {
                availablePlugin.Instance.Terminate();
                availablePlugins.Remove(availablePlugin);
            }

            Window.thisWindow.RemoveNodeFromPluginList(name);
        }

        // Token: 0x06000DAB RID: 3499 RVA: 0x0004D7C0 File Offset: 0x0004B9C0
        public void AddPluginFromString(string sourceCode)
        {
            var compilerResults = CompilerManager.Default.CompileFromString(sourceCode);
            if (compilerResults.Errors.Count > 0)
            {
                var stringBuilder = new StringBuilder();
                foreach (var obj in compilerResults.Errors)
                {
                    var compilerError = (CompilerError) obj;
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine(new string('-', 25));
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine("Error #" + compilerError.ErrorNumber);
                    stringBuilder.AppendLine("Message: " + compilerError.ErrorText);
                    stringBuilder.AppendLine("Line: " + compilerError.Line);
                }

                ShowErrorBox(stringBuilder);
                return;
            }

            var compiledAssembly = compilerResults.CompiledAssembly;
            foreach (var type in compiledAssembly.GetTypes())
                if (type.IsPublic && !type.IsAbstract)
                {
                    var baseType = type.BaseType;
                    if (baseType.FullName == "MCDzienny.Plugins.Plugin")
                    {
                        var newPlugin = new AvailablePlugin();
                        newPlugin.Instance =
                            (Plugin) Activator.CreateInstance(compiledAssembly.GetType(type.ToString()));
                        if (availablePlugins.Any(p => p.Instance.Name == newPlugin.Instance.Name))
                        {
                            if (DialogResult.Yes !=
                                MessageBox.Show("Override existing plugin named " + newPlugin.Instance.Name,
                                    "Duplicate", MessageBoxButtons.YesNo)) break;
                            RemovePluginByName(newPlugin.Instance.Name);
                        }

                        newPlugin.Instance.Initialize();
                        availablePlugins.Add(newPlugin);
                        var node = new TreeNode(newPlugin.Instance.Name);
                        Window.thisWindow.AddNodeToPluginList(node);
                    }
                }
        }

        // Token: 0x06000DAC RID: 3500 RVA: 0x0004D9EC File Offset: 0x0004BBEC
        private static void ShowErrorBox(StringBuilder sb)
        {
            if (!Server.mono && !Server.CLI && Window.thisWindow.WindowState != FormWindowState.Minimized)
                new PopUpMessage(sb.ToString(), "Error Box", "Compiler errors:").Show();
        }

        // Token: 0x06000DAD RID: 3501 RVA: 0x0004DA24 File Offset: 0x0004BC24
        private void AddPlugin(string FileName)
        {
            var assembly = Assembly.LoadFrom(FileName);
            foreach (var type in assembly.GetTypes())
                if (type.IsPublic && !type.IsAbstract)
                {
                    var @interface = type.GetInterface("MCDzienny.Plugin", true);
                    if (@interface != null)
                    {
                        var availablePlugin = new AvailablePlugin();
                        availablePlugin.AssemblyPath = FileName;
                        availablePlugin.Instance = (Plugin) Activator.CreateInstance(assembly.GetType(type.ToString()));
                        availablePlugin.Instance.Initialize();
                        availablePlugins.Add(availablePlugin);
                    }
                }
        }
    }
}