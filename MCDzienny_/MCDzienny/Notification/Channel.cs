namespace MCDzienny.Notification
{
    public sealed class Channel
    {

        public static readonly Channel General = new Channel("General", "0");

        public static readonly Channel Lava = new Channel("Lava", "1");

        public static readonly Channel Zombie = new Channel("Zombie", "2");

        public static readonly Channel Freebuild = new Channel("Freebuild", "3");

        readonly string id;
        readonly string name;

        Channel(string name, string id)
        {
            this.name = name;
            this.id = id;
        }

        public string Name { get { return name; } }

        public string ID { get { return id; } }
    }
}