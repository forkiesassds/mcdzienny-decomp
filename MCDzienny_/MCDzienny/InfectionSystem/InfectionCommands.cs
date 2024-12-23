using System.Threading;

namespace MCDzienny.InfectionSystem
{
    public class InfectionCommands
    {

        static int SortInfectionCommandByDelay(InfectionCommand x, InfectionCommand y)
        {
            if (x.Delay > y.Delay)
            {
                return 1;
            }
            if (x.Delay < y.Delay)
            {
                return -1;
            }
            return 0;
        }

        public static void StartInfectionCommands(object infectionMap)
        {
            InfectionMaps.InfectionMap infectionMap2 = (InfectionMaps.InfectionMap)infectionMap;
            infectionMap2.InfectionCommands.Sort(SortInfectionCommandByDelay);
            foreach (InfectionCommand infectionCommand in infectionMap2.InfectionCommands)
            {
                Thread.Sleep(infectionCommand.Delay * 1000);
                if (!InfectionSystem.phase2holder)
                {
                    break;
                }
                DoInfectionCommand(infectionCommand);
            }
        }

        public static void DoInfectionCommand(InfectionCommand iCommand)
        {
            string text = iCommand.Command.TrimStart('/').Trim();
            string name = text.Split(' ')[0].ToLower();
            string message = text.Substring(text.IndexOf(' '));
            Command.all.Find(name).Use(null, message);
        }

        public class InfectionCommand
        {
            public string Command { get; set; }

            public int Delay { get; set; }
        }
    }
}