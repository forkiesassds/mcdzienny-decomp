namespace MCDzienny.StoreSystem
{
    public static class Store
    {
        public static ZombieStore ZombieStore;

        public static void InitAll()
        {
            ZombieScriptLoader zombieScriptLoader = new ZombieScriptLoader();
            zombieScriptLoader.Compile();
            if (zombieScriptLoader.Load() != null)
            {
                ZombieStore.SetDefaultItems();
            }
            ZombieStore = new ZombieStore();
        }
    }
}