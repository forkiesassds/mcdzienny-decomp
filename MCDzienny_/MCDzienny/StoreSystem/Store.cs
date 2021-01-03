namespace MCDzienny.StoreSystem
{
    // Token: 0x0200023C RID: 572
    public static class Store
    {
        // Token: 0x04000899 RID: 2201
        public static ZombieStore ZombieStore;

        // Token: 0x060010AA RID: 4266 RVA: 0x0005613C File Offset: 0x0005433C
        public static void InitAll()
        {
            var zombieScriptLoader = new ZombieScriptLoader();
            zombieScriptLoader.Compile();
            if (zombieScriptLoader.Load() != null) ZombieStore.SetDefaultItems();
            ZombieStore = new ZombieStore();
        }
    }
}