using System;

namespace MCDzienny.Misc
{
    // Token: 0x0200003E RID: 62
    public struct Ref<T>
    {
        // Token: 0x06000155 RID: 341 RVA: 0x0000895C File Offset: 0x00006B5C
        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        // Token: 0x17000056 RID: 86
        // (get) Token: 0x06000156 RID: 342 RVA: 0x0000896C File Offset: 0x00006B6C
        // (set) Token: 0x06000157 RID: 343 RVA: 0x0000897C File Offset: 0x00006B7C
        public T Value
        {
            get { return getter(); }
            set { setter(value); }
        }

        // Token: 0x040000D4 RID: 212
        private readonly Func<T> getter;

        // Token: 0x040000D5 RID: 213
        private readonly Action<T> setter;
    }
}