namespace MCDzienny.Games
{
    // Token: 0x02000029 RID: 41
    public abstract class GameBase
    {
        // Token: 0x060000F9 RID: 249
        public abstract void OnPlayerJoin(Player player);

        // Token: 0x060000FA RID: 250
        public abstract void OnPlayerLeave(Player player);

        // Token: 0x060000FB RID: 251
        public abstract void Start();

        // Token: 0x060000FC RID: 252
        public abstract void End();
    }
}