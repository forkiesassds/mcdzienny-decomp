using System.Collections.Generic;
using System.Timers;

namespace MCDzienny.Bot
{
    // Token: 0x0200000C RID: 12
    public class MainLoop
    {
        // Token: 0x04000026 RID: 38
        private readonly int Interval = 40;

        // Token: 0x0400002A RID: 42
        private readonly object syncRoot = new object();

        // Token: 0x04000024 RID: 36
        public BlockMap blockMap;

        // Token: 0x04000025 RID: 37
        public Level level;

        // Token: 0x04000027 RID: 39
        private Timer mainLoop;

        // Token: 0x04000029 RID: 41
        private Player p;

        // Token: 0x04000028 RID: 40
        private Zombie z;

        // Token: 0x06000038 RID: 56 RVA: 0x0000325C File Offset: 0x0000145C
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

        // Token: 0x06000039 RID: 57 RVA: 0x0000334C File Offset: 0x0000154C
        public void Stop()
        {
            if (mainLoop == null) return;
            mainLoop.Dispose();
        }

        // Token: 0x0600003A RID: 58 RVA: 0x00003364 File Offset: 0x00001564
        private void mainLoop_Elapsed(object sender, ElapsedEventArgs e)
        {
            mainLoop.Stop();
            Tick();
            mainLoop.Start();
        }

        // Token: 0x0600003B RID: 59 RVA: 0x00003384 File Offset: 0x00001584
        private void Tick()
        {
            lock (syncRoot)
            {
                var list = new List<Level>(Server.levels);
                foreach (var level in list)
                {
                    var blockMap = level.blockMap;
                    if (blockMap != null) blockMap.tickAll();
                }
            }

            p.SendPos(2, (ushort) (32f * z.x), (ushort) (32f * z.y), (ushort) (32f * z.z),
                (byte) (z.yRot * 256f / 360f - 127f), (byte) (z.xRot * 256f / 360f));
        }
    }
}