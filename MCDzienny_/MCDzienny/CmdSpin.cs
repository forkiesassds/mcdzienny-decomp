using System.Collections.Generic;

namespace MCDzienny
{
    public class CmdSpin : Command
    {
        public override string name { get { return "spin"; } }

        public override string shortcut { get { return ""; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 1)
            {
                Help(p);
                return;
            }
            if (message == "")
            {
                message = "90";
            }
            var newBuffer = new List<Player.CopyPos>();
            int TotalLoop = 0;
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
                        Pos.x = (ushort)(p.CopyBuffer[TotalLoop].y - 2 * p.CopyBuffer[TotalLoop].y);
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
                        Pos.z = (ushort)(p.CopyBuffer[TotalLoop].y - 2 * p.CopyBuffer[TotalLoop].y);
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

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spin <90/180/mirror/upsidedown> - Spins the copied object.");
            Player.SendMessage(p, "Shotcuts: m for mirror, u for upside down, x for spin 90 on x, z for spin 90 on z.");
        }
    }
}