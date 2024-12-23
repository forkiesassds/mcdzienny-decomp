using System;

namespace MCDzienny
{
    [Serializable]
    public struct ValueExplicitPair<TValue>
    {
        TValue value;

        bool isExplicit;

        public TValue Value
        {
            get { return value; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this.value = value;
            }
        }

        public bool IsExplicit { get { return isExplicit; } set { isExplicit = value; } }

        public ValueExplicitPair(TValue value, bool isDefault)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.value = value;
            isExplicit = isDefault;
        }
    }
}