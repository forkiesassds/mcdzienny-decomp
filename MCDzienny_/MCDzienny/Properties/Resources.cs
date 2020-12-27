using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace MCDzienny.Properties
{
    // Token: 0x020001F7 RID: 503
    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [DebuggerNonUserCode]
    [CompilerGenerated]
    internal class Resources
    {
        // Token: 0x04000752 RID: 1874
        private static ResourceManager resourceMan;

        // Token: 0x04000753 RID: 1875
        private static CultureInfo resourceCulture;

        // Token: 0x06000DDD RID: 3549 RVA: 0x0004E0C8 File Offset: 0x0004C2C8

        // Token: 0x17000525 RID: 1317
        // (get) Token: 0x06000DDE RID: 3550 RVA: 0x0004E0D0 File Offset: 0x0004C2D0
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (ReferenceEquals(resourceMan, null))
                {
                    var resourceManager = new ResourceManager("MCDzienny_.MCDzienny.Properties.Resources",
                        typeof(Resources).Assembly);
                    resourceMan = resourceManager;
                }

                return resourceMan;
            }
        }

        // Token: 0x17000526 RID: 1318
        // (get) Token: 0x06000DDF RID: 3551 RVA: 0x0004E110 File Offset: 0x0004C310
        // (set) Token: 0x06000DE0 RID: 3552 RVA: 0x0004E118 File Offset: 0x0004C318
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get { return resourceCulture; }
            set { resourceCulture = value; }
        }

        // Token: 0x17000527 RID: 1319
        // (get) Token: 0x06000DE1 RID: 3553 RVA: 0x0004E120 File Offset: 0x0004C320
        internal static Bitmap splashScreen
        {
            get
            {
                var @object = ResourceManager.GetObject("splashScreen", resourceCulture);
                return (Bitmap) @object;
            }
        }

        // Token: 0x17000528 RID: 1320
        // (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0004E148 File Offset: 0x0004C348
        internal static Bitmap sprocket_dark
        {
            get
            {
                var @object = ResourceManager.GetObject("sprocket_dark", resourceCulture);
                return (Bitmap) @object;
            }
        }

        // Token: 0x17000529 RID: 1321
        // (get) Token: 0x06000DE3 RID: 3555 RVA: 0x0004E170 File Offset: 0x0004C370
        internal static string ZombieStoreItems
        {
            get { return ResourceManager.GetString("ZombieStoreItems", resourceCulture); }
        }
    }
}