namespace MCDzienny.Database
{
    // Token: 0x02000091 RID: 145
    public class DBPlayerColumns
    {
        // Token: 0x040001E4 RID: 484
        public static readonly DBPlayerColumns RoundsOnZombie = new DBPlayerColumns("roundsOnZombie", "MEDIUMINT");

        // Token: 0x040001E2 RID: 482

        // Token: 0x040001E3 RID: 483

        // Token: 0x060003DC RID: 988 RVA: 0x00014544 File Offset: 0x00012744
        private DBPlayerColumns(string name, string type)
        {
            Name = name;
            Type = type;
        }

        // Token: 0x1700012C RID: 300
        // (get) Token: 0x060003DA RID: 986 RVA: 0x00014534 File Offset: 0x00012734
        public string Name { get; private set; }

        // Token: 0x1700012D RID: 301
        // (get) Token: 0x060003DB RID: 987 RVA: 0x0001453C File Offset: 0x0001273C
        public string Type { get; private set; }
    }
}