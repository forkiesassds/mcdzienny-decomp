using System;

namespace MCDzienny.Misc
{
    public struct Ref<T>
    {
        readonly Func<T> getter;

        readonly Action<T> setter;

        public T Value { get { return getter(); } set { setter(value); } }

        public Ref(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}