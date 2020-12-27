using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MCDzienny
{
    // Token: 0x0200013D RID: 317
    public class BrowsedObject
    {
        // Token: 0x040003F2 RID: 1010
        private PropertyGrid pg;

        // Token: 0x040003F4 RID: 1012
        private string someProperty;

        // Token: 0x17000465 RID: 1125
        // (get) Token: 0x06000976 RID: 2422 RVA: 0x0002E8F0 File Offset: 0x0002CAF0
        // (set) Token: 0x06000977 RID: 2423 RVA: 0x0002E8F8 File Offset: 0x0002CAF8
        public string SomeProperty
        {
            get { return someProperty; }
            set
            {
                new List<Func<object>>
                {
                    ttt
                }[0]();
                someProperty = value;
                OnPropertyValueChanged(this, EventArgs.Empty);
            }
        }

        // Token: 0x14000001 RID: 1
        // (add) Token: 0x06000971 RID: 2417 RVA: 0x0002E83C File Offset: 0x0002CA3C
        // (remove) Token: 0x06000972 RID: 2418 RVA: 0x0002E874 File Offset: 0x0002CA74
        public event EventHandler PropertyValueChanged;

        // Token: 0x06000973 RID: 2419 RVA: 0x0002E8AC File Offset: 0x0002CAAC
        private void OnPropertyValueChanged(object sender, EventArgs e)
        {
            pg.PropertyValueChanged += pg_PropertyValueChanged;
            var propertyValueChanged = PropertyValueChanged;
            if (propertyValueChanged != null) propertyValueChanged(sender, e);
        }

        // Token: 0x06000974 RID: 2420 RVA: 0x0002E8E4 File Offset: 0x0002CAE4
        private void pg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
        }

        // Token: 0x06000975 RID: 2421 RVA: 0x0002E8E8 File Offset: 0x0002CAE8
        public object ttt()
        {
            return 0;
        }

        // Token: 0x0200013E RID: 318
        private interface something
        {
            // Token: 0x06000979 RID: 2425
            void TTT();
        }

        // Token: 0x0200013F RID: 319
        public class Globals
        {
            // Token: 0x040003F5 RID: 1013
            private static readonly Globals instance = new Globals();

            // Token: 0x0600097A RID: 2426 RVA: 0x0002E948 File Offset: 0x0002CB48
            protected Globals()
            {
            }

            // Token: 0x17000466 RID: 1126
            // (get) Token: 0x0600097B RID: 2427 RVA: 0x0002E950 File Offset: 0x0002CB50
            public static Globals Instance
            {
                get { return instance; }
            }

            // Token: 0x0600097C RID: 2428 RVA: 0x0002E958 File Offset: 0x0002CB58
            public void DoSomething()
            {
            }

            // Token: 0x0600097D RID: 2429 RVA: 0x0002E95C File Offset: 0x0002CB5C
            public void DoSomethingElse()
            {
            }
        }

        // Token: 0x02000140 RID: 320
        public class Globals2 : Globals
        {
            // Token: 0x040003F6 RID: 1014
            private static readonly Globals2 instance = new Globals2();

            // Token: 0x17000467 RID: 1127
            // (get) Token: 0x0600097F RID: 2431 RVA: 0x0002E96C File Offset: 0x0002CB6C
            public new static Globals2 Instance
            {
                get { return instance; }
            }

            // Token: 0x06000980 RID: 2432 RVA: 0x0002E974 File Offset: 0x0002CB74
            public void DoMoreStuff()
            {
            }
        }
    }
}