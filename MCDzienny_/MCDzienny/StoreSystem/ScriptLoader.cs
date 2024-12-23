using System;

namespace MCDzienny.StoreSystem
{
    static class ScriptLoader
    {
        [Flags]
        public enum LoadScriptResult
        {
            TotalFailure = 0,
            Compiled = 1,
            Loaded = 2
        }

        public static LoadScriptResult LoadZombieScripts()
        {
            LoadScriptResult loadScriptResult = LoadScriptResult.TotalFailure;
            ZombieScriptLoader zombieScriptLoader = new ZombieScriptLoader();
            if (zombieScriptLoader.Compile())
            {
                loadScriptResult |= LoadScriptResult.Compiled;
            }
            if (zombieScriptLoader.Load() != null)
            {
                loadScriptResult |= LoadScriptResult.Loaded;
            }
            return loadScriptResult;
        }
    }
}