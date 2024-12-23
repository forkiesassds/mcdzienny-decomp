using System.Collections.Generic;

namespace MCDzienny.Levels.Effects
{
    public class Environment
    {

        public Environment()
        {
            Items = new List<EnvironmentItem>();
        }
        public List<EnvironmentItem> Items { get; set; }
    }
}