using System.Collections.Generic;

namespace MCDzienny
{
    // Token: 0x02000244 RID: 580
    public static class FindReference
    {
        // Token: 0x060010D6 RID: 4310 RVA: 0x00057F64 File Offset: 0x00056164
        public static ushort writeLetter(Player p, char c, ushort x, ushort y, ushort z, byte b, int direction)
        {
            return writeLetter(p.level, p, c, x, y, z, b, direction);
        }

        // Token: 0x060010D7 RID: 4311 RVA: 0x00057F7C File Offset: 0x0005617C
        public static ushort writeLetter(Level l, Player p, char c, ushort x, ushort y, ushort z, byte b,
            int directionToGo)
        {
            switch (c)
            {
                case 'A':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'B':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'C':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'D':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'E':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'F':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'G':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'H':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'I':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'J':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'K':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'L':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    break;
                case 'M':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'N':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'O':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'P':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'Q':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'R':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case 'S':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'T':
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'U':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'V':
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'W':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'X':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'Y':
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case 'Z':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '0':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case '1':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    break;
                case '2':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '3':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '4':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    break;
                case '5':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '6':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '7':
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '8':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '9':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case ':':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case '\\':
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    break;
                case '/':
                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case '.':
                    placeBlock(l, p, x, y, z, b);
                    break;
                case '!':
                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    break;
                case ',':
                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    break;
                case '\'':
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case '"':
                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 4), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                case '+':
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    break;
                case '-':
                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 2), z, b);
                    break;
                case '_':
                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, y, z, b);
                    break;
                case '=':
                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    placeBlock(l, p, x, (ushort) (y + 1), z, b);
                    placeBlock(l, p, x, (ushort) (y + 3), z, b);
                    break;
                default:
                    Player.SendMessage(p, "\"" + c + "\" is currently not supported. Space left");
                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    switch (directionToGo)
                    {
                        case 0:
                            x = (ushort) (x + 1);
                            break;
                        case 1:
                            x = (ushort) (x - 1);
                            break;
                        case 2:
                            z = (ushort) (z + 1);
                            break;
                        default:
                            z = (ushort) (z - 1);
                            break;
                    }

                    break;
                case ' ':
                    break;
            }

            switch (directionToGo)
            {
                case 0:
                    return (ushort) (x + 2);
                case 1:
                    return (ushort) (x - 2);
                case 2:
                    return (ushort) (z + 2);
                default:
                    return (ushort) (z - 2);
            }
        }

        // Token: 0x060010D8 RID: 4312 RVA: 0x0005AFEC File Offset: 0x000591EC
        public static List<ColorBlock> popRefCol(byte popType)
        {
            var item = default(ColorBlock);
            var list = new List<ColorBlock>();
            list.Clear();
            if (popType == 1)
            {
                item.r = 128;
                item.g = 86;
                item.b = 57;
                item.type = 3;
                list.Add(item);
                item.r = 162;
                item.g = 129;
                item.b = 75;
                item.type = 5;
                list.Add(item);
                item.r = 244;
                item.g = 237;
                item.b = 174;
                item.type = 12;
                list.Add(item);
                item.r = 226;
                item.g = 31;
                item.b = 38;
                item.type = 21;
                list.Add(item);
                item.r = 223;
                item.g = 135;
                item.b = 37;
                item.type = 22;
                list.Add(item);
                item.r = 230;
                item.g = 241;
                item.b = 25;
                item.type = 23;
                list.Add(item);
                item.r = 127;
                item.g = 234;
                item.b = 26;
                item.type = 24;
                list.Add(item);
                item.r = 25;
                item.g = 234;
                item.b = 20;
                item.type = 25;
                list.Add(item);
                item.r = 31;
                item.g = 234;
                item.b = 122;
                item.type = 26;
                list.Add(item);
                item.r = 27;
                item.g = 239;
                item.b = 225;
                item.type = 27;
                list.Add(item);
                item.r = 99;
                item.g = 166;
                item.b = 226;
                item.type = 28;
                list.Add(item);
                item.r = 111;
                item.g = 124;
                item.b = 235;
                item.type = 29;
                list.Add(item);
                item.r = 126;
                item.g = 34;
                item.b = 218;
                item.type = 30;
                list.Add(item);
                item.r = 170;
                item.g = 71;
                item.b = 219;
                item.type = 31;
                list.Add(item);
                item.r = 227;
                item.g = 39;
                item.b = 225;
                item.type = 32;
                list.Add(item);
                item.r = 234;
                item.g = 39;
                item.b = 121;
                item.type = 33;
                list.Add(item);
                item.r = 46;
                item.g = 68;
                item.b = 47;
                item.type = 34;
                list.Add(item);
                item.r = 135;
                item.g = 145;
                item.b = 130;
                item.type = 35;
                list.Add(item);
                item.r = 230;
                item.g = 240;
                item.b = 225;
                item.type = 36;
                list.Add(item);
                item.r = 57;
                item.g = 38;
                item.b = 25;
                item.type = 3;
                list.Add(item);
                item.r = 72;
                item.g = 57;
                item.b = 33;
                item.type = 5;
                list.Add(item);
                item.r = 109;
                item.g = 105;
                item.b = 77;
                item.type = 12;
                list.Add(item);
                item.r = 41;
                item.g = 31;
                item.b = 16;
                item.type = 17;
                list.Add(item);
                item.r = 101;
                item.g = 13;
                item.b = 16;
                item.type = 21;
                list.Add(item);
                item.r = 99;
                item.g = 60;
                item.b = 16;
                item.type = 22;
                list.Add(item);
                item.r = 102;
                item.g = 107;
                item.b = 11;
                item.type = 23;
                list.Add(item);
                item.r = 56;
                item.g = 104;
                item.b = 11;
                item.type = 24;
                list.Add(item);
                item.r = 11;
                item.g = 104;
                item.b = 8;
                item.type = 25;
                list.Add(item);
                item.r = 13;
                item.g = 104;
                item.b = 54;
                item.type = 26;
                list.Add(item);
                item.r = 12;
                item.g = 106;
                item.b = 100;
                item.type = 27;
                list.Add(item);
                item.r = 44;
                item.g = 74;
                item.b = 101;
                item.type = 28;
                list.Add(item);
                item.r = 49;
                item.g = 55;
                item.b = 105;
                item.type = 29;
                list.Add(item);
                item.r = 56;
                item.g = 15;
                item.b = 97;
                item.type = 30;
                list.Add(item);
                item.r = 75;
                item.g = 31;
                item.b = 97;
                item.type = 31;
                list.Add(item);
                item.r = 101;
                item.g = 17;
                item.b = 100;
                item.type = 32;
                list.Add(item);
                item.r = 104;
                item.g = 17;
                item.b = 54;
                item.type = 33;
                list.Add(item);
                item.r = 20;
                item.g = 30;
                item.b = 21;
                item.type = 34;
                list.Add(item);
                item.r = 60;
                item.g = 64;
                item.b = 58;
                item.type = 35;
                list.Add(item);
                item.r = 102;
                item.g = 107;
                item.b = 100;
                item.type = 36;
                list.Add(item);
                item.r = 0;
                item.g = 0;
                item.b = 0;
                item.type = 49;
                list.Add(item);
            }
            else if (popType == 2)
            {
                item.r = 128;
                item.g = 86;
                item.b = 57;
                item.type = 3;
                list.Add(item);
                item.r = 162;
                item.g = 129;
                item.b = 75;
                item.type = 5;
                list.Add(item);
                item.r = 244;
                item.g = 237;
                item.b = 174;
                item.type = 12;
                list.Add(item);
                item.r = 93;
                item.g = 70;
                item.b = 38;
                item.type = 17;
                list.Add(item);
                item.r = 226;
                item.g = 31;
                item.b = 38;
                item.type = 21;
                list.Add(item);
                item.r = 223;
                item.g = 135;
                item.b = 37;
                item.type = 22;
                list.Add(item);
                item.r = 230;
                item.g = 241;
                item.b = 25;
                item.type = 23;
                list.Add(item);
                item.r = 127;
                item.g = 234;
                item.b = 26;
                item.type = 24;
                list.Add(item);
                item.r = 25;
                item.g = 234;
                item.b = 20;
                item.type = 25;
                list.Add(item);
                item.r = 31;
                item.g = 234;
                item.b = 122;
                item.type = 26;
                list.Add(item);
                item.r = 27;
                item.g = 239;
                item.b = 225;
                item.type = 27;
                list.Add(item);
                item.r = 99;
                item.g = 166;
                item.b = 226;
                item.type = 28;
                list.Add(item);
                item.r = 111;
                item.g = 124;
                item.b = 235;
                item.type = 29;
                list.Add(item);
                item.r = 126;
                item.g = 34;
                item.b = 218;
                item.type = 30;
                list.Add(item);
                item.r = 170;
                item.g = 71;
                item.b = 219;
                item.type = 31;
                list.Add(item);
                item.r = 227;
                item.g = 39;
                item.b = 225;
                item.type = 32;
                list.Add(item);
                item.r = 234;
                item.g = 39;
                item.b = 121;
                item.type = 33;
                list.Add(item);
                item.r = 46;
                item.g = 68;
                item.b = 47;
                item.type = 34;
                list.Add(item);
                item.r = 135;
                item.g = 145;
                item.b = 130;
                item.type = 35;
                list.Add(item);
                item.r = 230;
                item.g = 240;
                item.b = 225;
                item.type = 36;
                list.Add(item);
                item.r = 0;
                item.g = 0;
                item.b = 0;
                item.type = 49;
                list.Add(item);
            }
            else if (popType == 3)
            {
                item.r = 46;
                item.g = 68;
                item.b = 47;
                item.type = 34;
                list.Add(item);
                item.r = 135;
                item.g = 145;
                item.b = 130;
                item.type = 35;
                list.Add(item);
                item.r = 230;
                item.g = 240;
                item.b = 225;
                item.type = 36;
                list.Add(item);
                item.r = 20;
                item.g = 30;
                item.b = 21;
                item.type = 34;
                list.Add(item);
                item.r = 60;
                item.g = 64;
                item.b = 58;
                item.type = 35;
                list.Add(item);
                item.r = 102;
                item.g = 107;
                item.b = 100;
                item.type = 36;
                list.Add(item);
                item.r = 0;
                item.g = 0;
                item.b = 0;
                item.type = 49;
                list.Add(item);
            }
            else if (popType == 4)
            {
                item.r = 46;
                item.g = 68;
                item.b = 47;
                item.type = 34;
                list.Add(item);
                item.r = 135;
                item.g = 145;
                item.b = 130;
                item.type = 35;
                list.Add(item);
                item.r = 230;
                item.g = 240;
                item.b = 225;
                item.type = 36;
                list.Add(item);
                item.r = 0;
                item.g = 0;
                item.b = 0;
                item.type = 49;
                list.Add(item);
            }
            else if (popType == 5)
            {
                item.r = byte.MaxValue;
                item.g = byte.MaxValue;
                item.b = byte.MaxValue;
                item.type = 36;
                list.Add(item);
                item.r = 0;
                item.g = 0;
                item.b = 0;
                item.type = 49;
                list.Add(item);
            }

            return list;
        }

        // Token: 0x060010D9 RID: 4313 RVA: 0x0005BD80 File Offset: 0x00059F80
        public static void placeBlock(Level l, Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (p == null)
            {
                l.Blockchange(x, y, z, type);
                return;
            }

            l.Blockchange(p, x, y, z, type);
        }

        // Token: 0x02000245 RID: 581
        public struct ColorBlock
        {
            // Token: 0x040008BA RID: 2234
            public ushort x;

            // Token: 0x040008BB RID: 2235
            public ushort y;

            // Token: 0x040008BC RID: 2236
            public ushort z;

            // Token: 0x040008BD RID: 2237
            public byte type;

            // Token: 0x040008BE RID: 2238
            public byte r;

            // Token: 0x040008BF RID: 2239
            public byte g;

            // Token: 0x040008C0 RID: 2240
            public byte b;

            // Token: 0x040008C1 RID: 2241
            public byte a;
        }
    }
}