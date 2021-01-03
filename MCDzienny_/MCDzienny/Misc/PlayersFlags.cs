using System;
using System.Collections.Generic;

namespace MCDzienny.Misc
{
    // Token: 0x020001C6 RID: 454
    public class PlayersFlags : FlagsCollection32
    {
        // Token: 0x040006A4 RID: 1700
        private Dictionary<string, int> keyFlag;

        // Token: 0x06000CB9 RID: 3257 RVA: 0x0004991C File Offset: 0x00047B1C
        public PlayersFlags()
        {
            InitializeDictionary();
        }

        // Token: 0x06000CBA RID: 3258 RVA: 0x0004992C File Offset: 0x00047B2C
        public PlayersFlags(int flagContainer) : base(flagContainer)
        {
            InitializeDictionary();
        }

        // Token: 0x170004E6 RID: 1254
        public bool this[string flagName]
        {
            get
            {
                if (keyFlag == null) throw new NullReferenceException("keyFlag");
                if (!keyFlag.ContainsKey(flagName)) throw new KeyNotFoundException(flagName);
                return GetFlag(keyFlag[flagName]);
            }
            set
            {
                if (keyFlag == null) throw new NullReferenceException("keyFlag");
                if (!keyFlag.ContainsKey(flagName)) throw new KeyNotFoundException(flagName);
                SetFlag(keyFlag[flagName], value);
            }
        }

        // Token: 0x06000CBB RID: 3259 RVA: 0x0004993C File Offset: 0x00047B3C
        private void InitializeDictionary()
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