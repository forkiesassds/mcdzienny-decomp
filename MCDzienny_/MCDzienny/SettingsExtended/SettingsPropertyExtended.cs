using System;
using System.Collections.Generic;

namespace MCDzienny.SettingsExtended
{
    // Token: 0x0200021A RID: 538
    public class SettingsPropertyExtended
    {
        // Token: 0x04000869 RID: 2153
        private Dictionary<object, object> attributes;

        // Token: 0x04000867 RID: 2151

        // Token: 0x04000868 RID: 2152

        // Token: 0x04000866 RID: 2150
        private string name;

        // Token: 0x0400086A RID: 2154

        // Token: 0x06000ED9 RID: 3801 RVA: 0x00052EA4 File Offset: 0x000510A4
        public SettingsPropertyExtended(string name)
        {
            this.name = name;
        }

        // Token: 0x1700053C RID: 1340
        // (get) Token: 0x06000EDA RID: 3802 RVA: 0x00052EB4 File Offset: 0x000510B4
        // (set) Token: 0x06000EDB RID: 3803 RVA: 0x00052EBC File Offset: 0x000510BC
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }

        // Token: 0x1700053D RID: 1341
        // (get) Token: 0x06000EDC RID: 3804 RVA: 0x00052EC8 File Offset: 0x000510C8
        // (set) Token: 0x06000EDD RID: 3805 RVA: 0x00052ED0 File Offset: 0x000510D0
        public virtual Type PropertyType { get; set; }

        // Token: 0x1700053E RID: 1342
        // (get) Token: 0x06000EDE RID: 3806 RVA: 0x00052EDC File Offset: 0x000510DC
        // (set) Token: 0x06000EDF RID: 3807 RVA: 0x00052EE4 File Offset: 0x000510E4
        public virtual object DefaultValue { get; set; }

        // Token: 0x1700053F RID: 1343
        // (get) Token: 0x06000EE0 RID: 3808 RVA: 0x00052EF0 File Offset: 0x000510F0
        // (set) Token: 0x06000EE1 RID: 3809 RVA: 0x00052EF8 File Offset: 0x000510F8
        public virtual string Description { get; set; }

        // Token: 0x17000540 RID: 1344
        // (get) Token: 0x06000EE2 RID: 3810 RVA: 0x00052F04 File Offset: 0x00051104
        // (set) Token: 0x06000EE3 RID: 3811 RVA: 0x00052F14 File Offset: 0x00051114
        public virtual Dictionary<object, object> Attributes
        {
            get { return new Dictionary<object, object>(attributes); }
            set { attributes = value; }
        }
    }
}