using System;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000224 RID: 548
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SettingsPathAttribute : Attribute
    {
        // Token: 0x04000878 RID: 2168

        // Token: 0x06000F15 RID: 3861 RVA: 0x000536F4 File Offset: 0x000518F4
        public SettingsPathAttribute(string path)
        {
            Path = path;
        }

        // Token: 0x1700054F RID: 1359
        // (get) Token: 0x06000F16 RID: 3862 RVA: 0x00053704 File Offset: 0x00051904
        public string Path { get; private set; }
    }
}