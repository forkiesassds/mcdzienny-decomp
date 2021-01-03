using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x0200029D RID: 669
    public class CmdSpin : Command
    {
        // Token: 0x17000736 RID: 1846
        // (get) Token: 0x06001324 RID: 4900 RVA: 0x00069944 File Offset: 0x00067B44
        public override string name
        {
            get { return "spin"; }
        }

        // Token: 0x17000737 RID: 1847
        // (get) Token: 0x06001325 RID: 4901 RVA: 0x0006994C File Offset: 0x00067B4C
        public override string shortcut
        {
            get { return ""; }
        }

        // Token: 0x17000738 RID: 1848
        // (get) Token: 0x06001326 RID: 4902 RVA: 0x00069954 File Offset: 0x00067B54
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000739 RID: 1849
        // (get) Token: 0x06001327 RID: 4903 RVA: 0x0006995C File Offset: 0x00067B5C
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700073A RID: 1850
        // (get) Token: 0x06001328 RID: 4904 RVA: 0x00069960 File Offset: 0x00067B60
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.AdvBuilder; }
        }

        // Token: 0x1700073B RID: 1851
        // (get) Token: 0x06001329 RID: 4905 RVA: 0x00069964 File Offset: 0x00067B64
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x0600132B RID: 4907 RVA: 0x00069970 File Offset: 0x00067B70
        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 1)
            {
                Help(p);
                return;
            }

            if (message == "") message = "90";
            var newBuffer = new List<Player.CopyPos>();
            var TotalLoop = 0;
            newBuffer.Clear();
            switch (message)
            {
                case "90":
                {
                    ushort temp;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        temp = Pos.z;
                        Pos.z = Pos.x;
                        Pos.x = temp;
                        p.CopyBuffer[TotalLoop] = Pos;
                        TotalLoop++;
                    });
                    goto case "mirror";
                }
                case "180":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop--;
                        Pos.x = p.CopyBuffer[TotalLoop].x;
                        Pos.z = p.CopyBuffer[TotalLoop].z;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "upsidedown":
                case "u":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop--;
                        Pos.y = p.CopyBuffer[TotalLoop].y;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "mirror":
                case "m":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop--;
                        Pos.x = p.CopyBuffer[TotalLoop].x;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "z":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop--;
                        Pos.x = (ushort) (p.CopyBuffer[TotalLoop].y - 2 * p.CopyBuffer[TotalLoop].y);
                        Pos.y = p.CopyBuffer[TotalLoop].x;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                case "x":
                    TotalLoop = p.CopyBuffer.Count;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos Pos)
                    {
                        TotalLoop--;
                        Pos.z = (ushort) (p.CopyBuffer[TotalLoop].y - 2 * p.CopyBuffer[TotalLoop].y);
                        Pos.y = p.CopyBuffer[TotalLoop].z;
                        newBuffer.Add(Pos);
                    });
                    p.CopyBuffer.Clear();
                    p.CopyBuffer = newBuffer;
                    break;
                default:
                    Player.SendMessage(p, "Incorrect syntax");
                    Help(p);
                    return;
            }

            Player.SendMessage(p, string.Format("Spun: &b{0}", message));
        }

        // Token: 0x0600132C RID: 4908 RVA: 0x00069D0C File Offset: 0x00067F0C
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spin <90/180/mirror/upsidedown> - Spins the copied object.");
            Player.SendMessage(p, "Shotcuts: m for mirror, u for upside down, x for spin 90 on x, z for spin 90 on z.");
        }
    }
}