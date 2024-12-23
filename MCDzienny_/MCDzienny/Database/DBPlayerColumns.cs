namespace MCDzienny.Database
{
    public class DBPlayerColumns
    {

        public static readonly DBPlayerColumns RoundsOnZombie = new DBPlayerColumns("roundsOnZombie", "MEDIUMINT");
        readonly string name;

        readonly string type;

        DBPlayerColumns(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

        public string Name { get { return name; } }

        public string Type { get { return type; } }
    }
}