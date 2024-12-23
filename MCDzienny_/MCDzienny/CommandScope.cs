using System;

namespace MCDzienny
{
    [Flags]
    public enum CommandScope
    {
        Freebuild = 1,
        Lava = 2,
        Zombie = 4,
        Home = 8,
        TntWars = 0x10,
        MyMap = 0x20,
        All = 0x3F
    }
}