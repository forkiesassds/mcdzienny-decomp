using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MCDzienny
{
    public class BrowsedObject
    {

        PropertyGrid pg;

        string someProperty;

        public string SomeProperty
        {
            get { return someProperty; }
            set
            {
                var list = new List<Func<object>>();
                list.Add(ttt);
                list[0]();
                someProperty = value;
                OnPropertyValueChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler PropertyValueChanged;

        void OnPropertyValueChanged(object sender, EventArgs e)
        {
            //IL_000d: Unknown result type (might be due to invalid IL or missing references)
            //IL_0017: Expected O, but got Unknown
            pg.PropertyValueChanged += pg_PropertyValueChanged;
            if (PropertyValueChanged != null)
            {
                PropertyValueChanged(sender, e);
            }
        }

        void pg_PropertyValueChanged(object s, PropertyValueChangedEventArgs e) {}

        public object ttt()
        {
            return 0;
        }

        interface something
        {
            void TTT();
        }

        public class Globals
        {
            static readonly Globals instance = new Globals();

            protected Globals() {}

            public static Globals Instance { get { return instance; } }

            public void DoSomething() {}

            public void DoSomethingElse() {}
        }

        public class Globals2 : Globals
        {
            static readonly Globals2 instance = new Globals2();

            public new static Globals2 Instance { get { return instance; } }

            public void DoMoreStuff() {}
        }
    }
}