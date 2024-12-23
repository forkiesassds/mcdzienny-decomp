using System.Collections.Generic;
using System.Timers;

namespace MCDzienny.Bot
{
    public class MainLoop
    {

        readonly int Interval = 40;

        readonly object syncRoot = new object();
        public BlockMap blockMap;

        public Level level;

        Timer mainLoop;

        Player p;

        Zombie z;

        public void Initialize(Player p)
        {
            this.p = p;
            level = p.level;
            blockMap = new BlockMap(level.width, level.height, level.depth);
            level.blockMap = blockMap;
            z = new Zombie(level, 30f, 63f, 30f);
            p.SendSpawn(2, "Zombie", 30, 63, 30, 0, 0);
            blockMap.insert(z);
            mainLoop = new Timer();
            mainLoop.Interval = 40.0;
            mainLoop.Elapsed += mainLoop_Elapsed;
            mainLoop.AutoReset = false;
            mainLoop.Start();
        }

        public void Stop()
        {
            if (mainLoop != null)
            {
                mainLoop.Dispose();
            }
        }

        void mainLoop_Elapsed(object sender, ElapsedEventArgs e)
        {
            mainLoop.Stop();
            Tick();
            mainLoop.Start();
        }

        void Tick()
        {
            lock (syncRoot)
            {
                var list = new List<Level>(Server.levels);
                foreach (Level item in list)
                {
                    if (item.blockMap != null)
                    {
                        item.blockMap.tickAll();
                    }
                }
            }
            p.SendPos(2, (ushort)(32f * z.x), (ushort)(32f * z.y), (ushort)(32f * z.z), (byte)(z.yRot * 256f / 360f - 127f), (byte)(z.xRot * 256f / 360f));
        }
    }
}