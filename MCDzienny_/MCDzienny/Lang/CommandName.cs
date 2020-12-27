using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MCDzienny.Lang
{
    // Token: 0x020000A9 RID: 169
    [DebuggerNonUserCode]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [CompilerGenerated]
    internal class CommandName
    {
        // Token: 0x04000257 RID: 599
        private static ResourceManager resourceMan;

        // Token: 0x04000258 RID: 600
        private static CultureInfo resourceCulture;

        // Token: 0x06000550 RID: 1360 RVA: 0x0001B364 File Offset: 0x00019564

        // Token: 0x17000214 RID: 532
        // (get) Token: 0x06000551 RID: 1361 RVA: 0x0001B36C File Offset: 0x0001956C
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    var resourceManager = new ResourceManager("MCDzienny_.MCDzienny.Lang.CommandName",
                        typeof(CommandName).Assembly);
                    resourceMan = resourceManager;
                }

                return resourceMan;
            }
        }

        // Token: 0x17000215 RID: 533
        // (get) Token: 0x06000552 RID: 1362 RVA: 0x0001B3AC File Offset: 0x000195AC
        // (set) Token: 0x06000553 RID: 1363 RVA: 0x0001B3B4 File Offset: 0x000195B4
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }
    }
}