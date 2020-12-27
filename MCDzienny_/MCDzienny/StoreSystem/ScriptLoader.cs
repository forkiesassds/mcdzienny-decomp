using System;

namespace MCDzienny.StoreSystem
{
    // Token: 0x0200023A RID: 570
    internal static class ScriptLoader
    {
        // Token: 0x0200023B RID: 571
        [Flags]
        public enum LoadScriptResult
        {
            // Token: 0x04000896 RID: 2198
            TotalFailure = 0,

            // Token: 0x04000897 RID: 2199
            Compiled = 1,

            // Token: 0x04000898 RID: 2200
            Loaded = 2
        }

        // Token: 0x060010A9 RID: 4265 RVA: 0x0005610C File Offset: 0x0005430C
        public static LoadScriptResult LoadZombieScripts()
        {
            var loadScriptResult = LoadScriptResult.TotalFailure;
            var zombieScriptLoader = new ZombieScriptLoader();
            if (zombieScriptLoader.Compile()) loadScriptResult |= LoadScriptResult.Compiled;
            if (zombieScriptLoader.Load() != null) loadScriptResult |= LoadScriptResult.Loaded;
            return loadScriptResult;
        }
    }
}