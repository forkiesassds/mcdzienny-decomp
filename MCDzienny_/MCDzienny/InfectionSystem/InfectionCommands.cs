using System.Threading;

namespace MCDzienny.InfectionSystem
{
    // Token: 0x0200009D RID: 157
    public class InfectionCommands
    {
        // Token: 0x0600042B RID: 1067 RVA: 0x00018200 File Offset: 0x00016400
        private static int SortInfectionCommandByDelay(InfectionCommand x, InfectionCommand y)
        {
            if (x.Delay > y.Delay) return 1;
            if (x.Delay < y.Delay) return -1;
            return 0;
        }

        // Token: 0x0600042C RID: 1068 RVA: 0x00018224 File Offset: 0x00016424
        public static void StartInfectionCommands(object infectionMap)
        {
            var infectionMap2 = (InfectionMaps.InfectionMap) infectionMap;
            infectionMap2.InfectionCommands.Sort(SortInfectionCommandByDelay);
            foreach (var infectionCommand in infectionMap2.InfectionCommands)
            {
                Thread.Sleep(infectionCommand.Delay * 1000);
                if (!InfectionSystem.phase2holder) break;
                DoInfectionCommand(infectionCommand);
            }
        }

        // Token: 0x0600042D RID: 1069 RVA: 0x000182B0 File Offset: 0x000164B0
        public static void DoInfectionCommand(InfectionCommand iCommand)
        {
            var text = iCommand.Command.TrimStart('/').Trim();
            var name = text.Split(' ')[0].ToLower();
            var message = text.Substring(text.IndexOf(' '));
            Command.all.Find(name).Use(null, message);
        }

        // Token: 0x0200009E RID: 158
        public class InfectionCommand
        {
            // Token: 0x1700012F RID: 303
            // (get) Token: 0x0600042F RID: 1071 RVA: 0x00018324 File Offset: 0x00016524
            // (set) Token: 0x06000430 RID: 1072 RVA: 0x0001832C File Offset: 0x0001652C
            public string Command { get; set; }

            // Token: 0x17000130 RID: 304
            // (get) Token: 0x06000431 RID: 1073 RVA: 0x00018338 File Offset: 0x00016538
            // (set) Token: 0x06000432 RID: 1074 RVA: 0x00018340 File Offset: 0x00016540
            public int Delay { get; set; }
        }
    }
}