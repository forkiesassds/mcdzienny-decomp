using System;
using System.Threading;

namespace MCDzienny
{
    public class CmdZombiemob : Command
    {

        public CatchPos bp;

        public bool isRandom;

        public int thex;

        public int they;

        public int thez;

        public int wavesLength;

        public int wavesNum;

        public int zombiesNum;

        public override string name { get { return "zombiespawn"; } }

        public override string shortcut { get { return "zs"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            int num = message.Split(' ').Length;
            string[] array = message.Split(' ');
            if (num == 1 && array[0].ToLower() == "x")
            {
                if (p == null)
                {
                    all.Find("replaceall").Use(p, "zombie air lava");
                }
                else
                {
                    all.Find("replaceall").Use(p, "zombie air");
                }
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
                    CatchPos catchPos = default(CatchPos);
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
                    Thread thread = new Thread(ZombieMob);
                    thread.Start(p);
                }
            }
            catch (FormatException)
            {
                Player.SendMessage(p, "&4All parameters must be numbers!");
            }
        }

        public void ZombieMob(object player)
        {
            int xBegin = 0;
            int zBegin = 0;
            Player p = (Player)player;
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
            for (int i = 1; i <= wavesNum; i++)
            {
                if (isRandom)
                {
                    randomZombies(p);
                }
                else
                {
                    placedZombies(p, xBegin, zBegin);
                }
                all.Find("say").Use(p, "&aZombie wave # " + i);
                Thread.Sleep(wavesLength * 1000);
            }
            all.Find("say").Use(p, "&aZombie attack is over.");
        }

        public void randomZombies(Player p)
        {
            Random random = new Random();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; i < zombiesNum; i++)
            {
                if (p != null)
                {
                    num = random.Next(0, p.level.width);
                    num2 = random.Next(p.level.height / 2, p.level.height);
                    num3 = random.Next(0, p.level.depth);
                    all.Find("place").Use(p, "zombie " + num + " " + num2 + " " + num3);
                }
                else
                {
                    num = random.Next(0, LavaSystem.currentlvl.width);
                    num2 = random.Next(LavaSystem.currentlvl.height / 2, LavaSystem.currentlvl.height);
                    num3 = random.Next(0, LavaSystem.currentlvl.depth);
                    all.Find("place").Use(p, "zombie " + num + " " + num2 + " " + num3 + " lava");
                }
            }
        }

        public void placedZombies(Player p, int xBegin, int zBegin)
        {
            for (int i = xBegin; i < xBegin + zombiesNum; i++)
            {
                for (int j = zBegin; j < zBegin + zombiesNum; j++)
                {
                    if (p != null)
                    {
                        all.Find("place").Use(p, "zombie " + i + " " + they + " " + j);
                    }
                    else
                    {
                        all.Find("place").Use(p, "zombie " + i + " " + they + " " + j + " lava");
                    }
                }
            }
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            bp = (CatchPos)p.blockchangeObject;
            thex = x;
            they = y + 2;
            thez = z;
            p.blockchangeObject = bp;
            Thread thread = new Thread(ZombieMob);
            thread.Start(p);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zombiespawn <flag> <x> <y> <z> - Spawns waves of zombies.");
            Player.SendMessage(p, "<flag> - 'r' for random or 'd' for diameter");
            Player.SendMessage(p, "<x> - the number of waves");
            Player.SendMessage(p, "<y> - the length of the waves in seconds");
            Player.SendMessage(p, "<z> - the number of zombies spawned/diameter of spawn");
            Player.SendMessage(p, "/zombiespawn x - Destroys all zombies.");
        }

        public struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}