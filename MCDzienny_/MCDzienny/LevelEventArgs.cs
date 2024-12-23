using System;

namespace MCDzienny
{
    public class LevelEventArgs : EventArgs
    {
        public Level level;

        public LevelEventArgs(Level level)
        {
            this.level = level;
        }
    }
}