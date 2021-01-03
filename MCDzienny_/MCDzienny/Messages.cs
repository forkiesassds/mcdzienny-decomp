namespace MCDzienny
{
    // Token: 0x020001AF RID: 431
    public static class Messages
    {
        // Token: 0x06000C53 RID: 3155 RVA: 0x00047F68 File Offset: 0x00046168
        public static void TooManyBlocks(Player p, int amount)
        {
            Player.SendMessage(p, "You tried to modify " + amount + " blocks.");
            Player.SendMessage(p, "But your limit equals to " + p.group.maxBlocks + "blocks.");
        }
    }
}