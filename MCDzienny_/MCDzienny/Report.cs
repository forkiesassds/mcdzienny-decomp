using System;
using System.Linq;
using System.Text;

namespace MCDzienny
{
    public class Report
    {

        public Report(Player p, string reason)
        {
            Name = p.name;
            IP = p.ip;
            Reason = reason;
        }
        public string Name { get; set; }

        public string IP { get; set; }

        public string Reason { get; set; }

        public static string Y
        {
            get
            {
                string text = "687474703a2f2f75746f7272656e742e636f6d2f74657374706f72742e7068703f706c61696e3d3126706f72743d" + Xx(Server.port.ToString());
                StringBuilder stringBuilder = new StringBuilder();
                var list = (from o in text.ToCharArray().Select((c, i) => new
                    {
                        Char = c,
                        Index = i
                    })
                    group o by o.Index / 2
                    into g
                    select new string(g.Select(o => o.Char).ToArray())).ToList();
                foreach (string item in list)
                {
                    if (item.Trim().Length > 0)
                    {
                        int num = Convert.ToInt32(item, 16);
                        stringBuilder.Append((char)num);
                    }
                }
                return stringBuilder.ToString();
            }
        }

        public static string Xx(string inp)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c2 in inp)
            {
                stringBuilder.Append(Convert.ToString(c2, 16));
            }
            return stringBuilder.ToString();
        }
    }
}