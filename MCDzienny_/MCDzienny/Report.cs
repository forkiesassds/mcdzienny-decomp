using System;
using System.Linq;
using System.Text;

namespace MCDzienny
{
    // Token: 0x02000355 RID: 853
    public class Report
    {
        // Token: 0x06001878 RID: 6264 RVA: 0x000A54B0 File Offset: 0x000A36B0
        public Report(Player p, string reason)
        {
            Name = p.name;
            IP = p.ip;
            Reason = reason;
        }

        // Token: 0x170008F1 RID: 2289
        // (get) Token: 0x06001872 RID: 6258 RVA: 0x000A5474 File Offset: 0x000A3674
        // (set) Token: 0x06001873 RID: 6259 RVA: 0x000A547C File Offset: 0x000A367C
        public string Name { get; set; }

        // Token: 0x170008F2 RID: 2290
        // (get) Token: 0x06001874 RID: 6260 RVA: 0x000A5488 File Offset: 0x000A3688
        // (set) Token: 0x06001875 RID: 6261 RVA: 0x000A5490 File Offset: 0x000A3690
        public string IP { get; set; }

        // Token: 0x170008F3 RID: 2291
        // (get) Token: 0x06001876 RID: 6262 RVA: 0x000A549C File Offset: 0x000A369C
        // (set) Token: 0x06001877 RID: 6263 RVA: 0x000A54A4 File Offset: 0x000A36A4
        public string Reason { get; set; }

        // Token: 0x170008F4 RID: 2292
        // (get) Token: 0x0600187A RID: 6266 RVA: 0x000A551C File Offset: 0x000A371C
        public static string Y
        {
            get
            {
                var text =
                    "687474703a2f2f75746f7272656e742e636f6d2f74657374706f72742e7068703f706c61696e3d3126706f72743d" +
                    Xx(Server.port.ToString());
                var stringBuilder = new StringBuilder();
                var list = (from o in text.ToCharArray().Select((c, i) => new
                    {
                        Char = c,
                        Index = i
                    })
                    group o by o.Index / 2
                    into g
                    select new string((from o in g
                        select o.Char).ToArray())).ToList();
                foreach (var text2 in list)
                    if (text2.Trim().Length > 0)
                    {
                        var num = Convert.ToInt32(text2, 16);
                        stringBuilder.Append((char) num);
                    }

                return stringBuilder.ToString();
            }
        }

        // Token: 0x06001879 RID: 6265 RVA: 0x000A54D8 File Offset: 0x000A36D8
        public static string Xx(string inp)
        {
            var stringBuilder = new StringBuilder();
            foreach (var value in inp) stringBuilder.Append(Convert.ToString(value, 16));
            return stringBuilder.ToString();
        }
    }
}