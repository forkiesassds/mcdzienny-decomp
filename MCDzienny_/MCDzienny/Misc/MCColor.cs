namespace MCDzienny.Misc
{
    public struct MCColor
    {
        readonly string code;

        readonly string name;

        public static readonly MCColor Black = new MCColor("Black", "%0");

        public static readonly MCColor DarkBlue = new MCColor("DarkBlue", "%1");

        public static MCColor DarkGreen { get { return new MCColor("DarkGreen", "%2"); } }

        public static MCColor DarkTeal { get { return new MCColor("DarkTeal", "%3"); } }

        public static MCColor DarkRed { get { return new MCColor("DarkRed", "%4"); } }

        public static MCColor Purple { get { return new MCColor("Purple", "%5"); } }

        public static MCColor Gold { get { return new MCColor("Gold", "%6"); } }

        public static MCColor Grey { get { return new MCColor("Grey", "%7"); } }

        public static MCColor DarkGray { get { return new MCColor("DarkGrey", "%8"); } }

        public static MCColor Blue { get { return new MCColor("Blue", "%9"); } }

        public static MCColor Lime { get { return new MCColor("Lime", "%a"); } }

        public static MCColor Teal { get { return new MCColor("Teal", "%b"); } }

        public static MCColor Red { get { return new MCColor("Red", "%c"); } }

        public static MCColor Pink { get { return new MCColor("Pink", "%d"); } }

        public static MCColor Yellow { get { return new MCColor("Yellow", "%e"); } }

        public static MCColor White { get { return new MCColor("White", "%f"); } }

        public string Name { get { return name; } }

        public string Code { get { return code; } }

        MCColor(string name, string code)
        {
            this.name = name;
            this.code = code;
        }

        public override string ToString()
        {
            return code;
        }

        public static implicit operator string(MCColor c)
        {
            return c.code;
        }
    }
}