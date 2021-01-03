using System;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000222 RID: 546
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DefaultValueAttribute : Attribute
    {
        // Token: 0x04000877 RID: 2167

        // Token: 0x06000F12 RID: 3858 RVA: 0x000536D4 File Offset: 0x000518D4
        public DefaultValueAttribute(string value)
        {
            Value = value;
        }

        // Token: 0x1700054E RID: 1358
        // (get) Token: 0x06000F13 RID: 3859 RVA: 0x000536E4 File Offset: 0x000518E4
        public string Value { get; private set; }
    }
}