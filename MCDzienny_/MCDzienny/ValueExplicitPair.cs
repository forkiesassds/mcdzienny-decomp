using System;

namespace MCDzienny
{
    // Token: 0x020001AE RID: 430
    [Serializable]
    public struct ValueExplicitPair<TValue>
    {
        // Token: 0x06000C4E RID: 3150 RVA: 0x00047F0C File Offset: 0x0004610C
        public ValueExplicitPair(TValue value, bool isDefault)
        {
            if (value == null) throw new ArgumentNullException("value");
            this.value = value;
            isExplicit = isDefault;
        }

        // Token: 0x170004CD RID: 1229
        // (get) Token: 0x06000C4F RID: 3151 RVA: 0x00047F30 File Offset: 0x00046130
        // (set) Token: 0x06000C50 RID: 3152 RVA: 0x00047F38 File Offset: 0x00046138
        public TValue Value
        {
            get { return value; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                this.value = value;
            }
        }

        // Token: 0x170004CE RID: 1230
        // (get) Token: 0x06000C51 RID: 3153 RVA: 0x00047F54 File Offset: 0x00046154
        // (set) Token: 0x06000C52 RID: 3154 RVA: 0x00047F5C File Offset: 0x0004615C
        public bool IsExplicit
        {
            get { return isExplicit; }
            set { isExplicit = value; }
        }

        // Token: 0x04000670 RID: 1648
        private TValue value;

        // Token: 0x04000671 RID: 1649
        private bool isExplicit;
    }
}