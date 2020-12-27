using System;
using System.Collections.Generic;

namespace MCDzienny.SettingsFrame
{
    // Token: 0x02000225 RID: 549
    public class SettingsProperty
    {
        // Token: 0x0400087B RID: 2171
        private Dictionary<object, object> attributes;

        // Token: 0x0400087A RID: 2170

        // Token: 0x04000879 RID: 2169
        private string name;

        // Token: 0x0400087C RID: 2172

        // Token: 0x06000F17 RID: 3863 RVA: 0x0005370C File Offset: 0x0005190C
        public SettingsProperty(string name)
        {
            this.name = name;
        }

        // Token: 0x17000550 RID: 1360
        // (get) Token: 0x06000F18 RID: 3864 RVA: 0x0005371C File Offset: 0x0005191C
        // (set) Token: 0x06000F19 RID: 3865 RVA: 0x00053724 File Offset: 0x00051924
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Token: 0x17000551 RID: 1361
        // (get) Token: 0x06000F1A RID: 3866 RVA: 0x00053730 File Offset: 0x00051930
        // (set) Token: 0x06000F1B RID: 3867 RVA: 0x00053738 File Offset: 0x00051938
        public virtual Type PropertyType { get; set; }

        // Token: 0x17000552 RID: 1362
        // (get) Token: 0x06000F1C RID: 3868 RVA: 0x00053744 File Offset: 0x00051944
        // (set) Token: 0x06000F1D RID: 3869 RVA: 0x0005374C File Offset: 0x0005194C
        public virtual object DefaultValue { get; set; }

        // Token: 0x17000553 RID: 1363
        // (get) Token: 0x06000F1E RID: 3870 RVA: 0x00053758 File Offset: 0x00051958
        // (set) Token: 0x06000F1F RID: 3871 RVA: 0x00053768 File Offset: 0x00051968
        public virtual Dictionary<object, object> Attributes
        {
            get { return new Dictionary<object, object>(attributes); }
            set { attributes = value; }
        }
    }
}