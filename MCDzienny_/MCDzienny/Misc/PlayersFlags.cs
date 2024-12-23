using System;
using System.Collections.Generic;

namespace MCDzienny.Misc
{
    public class PlayersFlags : FlagsCollection32
    {
        Dictionary<string, int> keyFlag;

        public PlayersFlags()
        {
            InitializeDictionary();
        }

        public PlayersFlags(int flagContainer)
            : base(flagContainer)
        {
            InitializeDictionary();
        }

        public bool this[string flagName]
        {
            get
            {
                if (keyFlag == null)
                {
                    throw new NullReferenceException("keyFlag");
                }
                if (!keyFlag.ContainsKey(flagName))
                {
                    throw new KeyNotFoundException(flagName);
                }
                return GetFlag(keyFlag[flagName]);
            }
            set
            {
                if (keyFlag == null)
                {
                    throw new NullReferenceException("keyFlag");
                }
                if (!keyFlag.ContainsKey(flagName))
                {
                    throw new KeyNotFoundException(flagName);
                }
                SetFlag(keyFlag[flagName], value);
            }
        }

        void InitializeDictionary()
        {
            keyFlag = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            keyFlag.Add("loggedCorrectly", 0);
            keyFlag.Add("silentJoin", 1);
            keyFlag.Add("useTextures", 2);
            keyFlag.Add("womBanned", 3);
            keyFlag.Add("allowHacks", 4);
            keyFlag.Add("rulesRead", 5);
            keyFlag.Add("acceptedRules", 6);
            keyFlag.Add("registered", 7);
        }
    }
}