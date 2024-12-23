namespace MCDzienny.ActionScripts
{
    public abstract class Action
    {
        public abstract string Name { get; set; }

        public abstract string Description { get; set; }

        public abstract void DoAction(Player p, string arguments, int blockX, int blockY, int blockZ);
    }
}