namespace MCDzienny.Games
{
    public abstract class GameBase
    {
        public abstract void OnPlayerJoin(Player player);

        public abstract void OnPlayerLeave(Player player);

        public abstract void Start();

        public abstract void End();
    }
}