namespace MCDzienny
{
    public static class Messages
    {
        public static void TooManyBlocks(Player p, int amount)
        {
            Player.SendMessage(p, "You tried to modify " + amount + " blocks.");
            Player.SendMessage(p, "But your limit equals to " + p.group.maxBlocks + "blocks.");
        }
    }
}