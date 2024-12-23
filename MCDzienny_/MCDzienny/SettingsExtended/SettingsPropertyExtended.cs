using System;
using System.Collections.Generic;

namespace MCDzienny.SettingsExtended
{
    public class SettingsPropertyExtended
    {

        Dictionary<object, object> attributes;

        string name;

        public SettingsPropertyExtended(string name)
        {
            this.name = name;
        }

        public virtual string Name { get { return name; } set { name = value; } }

        public virtual Type PropertyType { get; set; }

        public virtual object DefaultValue { get; set; }

        public virtual string Description { get; set; }

        public virtual Dictionary<object, object> Attributes { get { return new Dictionary<object, object>(attributes); } set { attributes = value; } }
    }
}