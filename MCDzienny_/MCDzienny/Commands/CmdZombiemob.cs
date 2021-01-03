using System;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x02000084 RID: 132
    public class CmdZombiemob : Command
    {
        // Token: 0x040001CD RID: 461
        public CatchPos bp;

        // Token: 0x040001D1 RID: 465
        public bool isRandom;

        // Token: 0x040001CE RID: 462
        public int thex;

        // Token: 0x040001CF RID: 463
        public int they;

        // Token: 0x040001D0 RID: 464
        public int thez;

        // Token: 0x040001CB RID: 459
        public int wavesLength;

        // Token: 0x040001CA RID: 458
        public int wavesNum;

        // Token: 0x040001CC RID: 460
        public int zombiesNum;

        // Token: 0x1700010D RID: 269
        // (get) Token: 0x0600037A RID: 890 RVA: 0x000128A0 File Offset: 0x00010AA0
        public override string name
        {
            get { return "zombiespawn"; }
        }

        // Token: 0x1700010E RID: 270
        // (get) Token: 0x0600037B RID: 891 RVA: 0x000128A8 File Offset: 0x00010AA8
        public override string shortcut
        {
            get { return "zs"; }
        }

        // Token: 0x1700010F RID: 271
        // (get) Token: 0x0600037C RID: 892 RVA: 0x000128B0 File Offset: 0x00010AB0
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x17000110 RID: 272
        // (get) Token: 0x0600037D RID: 893 RVA: 0x000128B8 File Offset: 0x00010AB8
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x17000111 RID: 273
        // (get) Token: 0x0600037E RID: 894 RVA: 0x000128BC File Offset: 0x00010ABC
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Operator; }
        }

        // Token: 0x0600037F RID: 895 RVA: 0x000128C0 File Offset: 0x00010AC0
        public override void Use(Player p, string message)
        {
            var num = message.Split(' ').Length;
            var array = message.Split(' ');
            if (num == 1 && array[0].ToLower() == "x")
            {
                if (p == null)
                    all.Find("replaceall").Use(p, "zombie air lava");
                else
                    all.Find("replaceall").Use(p, "zombie air");
                Player.SendMessage(p, "&aAll zombies have been destroyed.");
                return;
            }

            if (num != 4)
            {
                Help(p);
                return;
            }

            try
            {
                if (array[0].ToLower() == "r")
                {
                    isRandom = true;
                }
                else
                {
                    if (!(array[0].ToLower() == "d"))
                    {
                        Player.SendMessage(p, "Flag set must be 'r' or 'd'.");
                        return;
                    }

                    isRandom = false;
                }

                wavesNum = int.Parse(array[1]);
                wavesLength = int.Parse(array[2]);
                zombiesNum = int.Parse(array[3]);
                if (!isRandom)
                {
                    CatchPos catchPos;
                    catchPos.x = 0;
                    catchPos.y = 0;
                    catchPos.z = 0;
                    p.blockchangeObject = catchPos;
                    Player.SendMessage(p, "Place a block for center of zombie spawn.");
                    p.ClearBlockchange();
                    p.Blockchange += Blockchange1;
                }
                else
                {
                    var thread = new Thread(ZombieMob);
                    thread.Start(p);
                }
            }
            catch (FormatException)
            {
                Player.SendMessage(p, "&4All parameters must be numbers!");
            }
        }

        // Token: 0x06000380 RID: 896 RVA: 0x00012A64 File Offset: 0x00010C64
        public void ZombieMob(object player)
        {
            var xBegin = 0;
            var zBegin = 0;
            var p = (Player) player;
            if (zombiesNum % 2 == 0 && !isRandom)
            {
                xBegin = thex - zombiesNum / 2;
                zBegin = thez - zombiesNum / 2;
            }

            if (zombiesNum % 2 == 1 && !isRandom)
            {
                xBegin = thex - (zombiesNum - 1) / 2;
                zBegin = thez - (zombiesNum - 1) / 2;
            }

            all.Find("say").Use(p, "&aInitiating zombie attack!");
            all.Find("say").Use(p, "&a" + wavesNum + " wave(s)");
            all.Find("say").Use(p, "&a" + wavesLength + " second(s) each wave");
            for (var i = 1; i <= wavesNum; i++)
            {
                if (isRandom)
                    randomZombies(p);
                else
                    placedZombies(p, xBegin, zBegin);
                all.Find("say").Use(p, "&aZombie wave # " + i);
                Thread.Sleep(wavesLength * 1000);
            }

            all.Find("say").Use(p, "&aZombie attack is over.");
        }

        // Token: 0x06000381 RID: 897 RVA: 0x00012BD8 File Offset: 0x00010DD8
        public void randomZombies(Player p)
        {
            var random = new Random();
            for (var i = 0; i < zombiesNum; i++)
                if (p != null)
                {
                    var num = random.Next(0, p.level.width);
                    var num2 = random.Next(p.level.height / 2, p.level.height);
                    var num3 = random.Next(0, p.level.depth);
                    all.Find("place").Use(p, string.Concat("zombie ", num, " ", num2, " ", num3));
                }
                else
                {
                    var num = random.Next(0, LavaSystem.currentlvl.width);
                    var num2 = random.Next(LavaSystem.currentlvl.height / 2, LavaSystem.currentlvl.height);
                    var num3 = random.Next(0, LavaSystem.currentlvl.depth);
                    all.Find("place").Use(p, string.Concat("zombie ", num, " ", num2, " ", num3, " lava"));
                }
        }

        // Token: 0x06000382 RID: 898 RVA: 0x00012D60 File Offset: 0x00010F60
        public void placedZombies(Player p, int xBegin, int zBegin)
        {
            for (var i = xBegin; i < xBegin + zombiesNum; i++)
            for (var j = zBegin; j < zBegin + zombiesNum; j++)
                if (p != null)
                    all.Find("place").Use(p, string.Concat("zombie ", i, " ", they, " ", j));
                else
                    all.Find("place").Use(p, string.Concat("zombie ", i, " ", they, " ", j, " lava"));
        }

        // Token: 0x06000383 RID: 899 RVA: 0x00012E60 File Offset: 0x00011060
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            bp = (CatchPos) p.blockchangeObject;
            thex = x;
            they = y + 2;
            thez = z;
            p.blockchangeObject = bp;
            var thread = new Thread(ZombieMob);
            thread.Start(p);
        }

        // Token: 0x06000384 RID: 900 RVA: 0x00012EE4 File Offset: 0x000110E4
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zombiespawn <flag> <x> <y> <z> - Spawns waves of zombies.");
            Player.SendMessage(p, "<flag> - 'r' for random or 'd' for diameter");
            Player.SendMessage(p, "<x> - the number of waves");
            Player.SendMessage(p, "<y> - the length of the waves in seconds");
            Player.SendMessage(p, "<z> - the number of zombies spawned/diameter of spawn");
            Player.SendMessage(p, "/zombiespawn x - Destroys all zombies.");
        }

        // Token: 0x02000085 RID: 133
        public struct CatchPos
        {
            // Token: 0x040001D2 RID: 466
            public ushort x;

            // Token: 0x040001D3 RID: 467
            public ushort y;

            // Token: 0x040001D4 RID: 468
            public ushort z;
        }
    }
}