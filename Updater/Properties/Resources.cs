using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Updater.Properties
{
    // Token: 0x02000002 RID: 2
    [DebuggerNonUserCode]
    [CompilerGenerated]
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    internal class Resources
    {
        // Token: 0x04000001 RID: 1
        private static ResourceManager resourceMan;

        // Token: 0x04000002 RID: 2
        private static CultureInfo resourceCulture;

        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250

        // Token: 0x17000001 RID: 1
        // (get) Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    var resourceManager =
                        new ResourceManager("Updater.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = resourceManager;
                }

                return resourceMan;
            }
        }

        // Token: 0x17000002 RID: 2
        // (get) Token: 0x06000003 RID: 3 RVA: 0x000020A8 File Offset: 0x000002A8
        // (set) Token: 0x06000004 RID: 4 RVA: 0x000020BF File Offset: 0x000002BF
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }
    }
}