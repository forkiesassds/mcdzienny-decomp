using System;
using System.Collections.Generic;
using System.IO;

namespace MCDzienny
{
    // Token: 0x020000B1 RID: 177
    public sealed class Block
    {
        // Token: 0x04000267 RID: 615
        public const byte air = 0;

        // Token: 0x04000268 RID: 616
        public const byte rock = 1;

        // Token: 0x04000269 RID: 617
        public const byte grass = 2;

        // Token: 0x0400026A RID: 618
        public const byte dirt = 3;

        // Token: 0x0400026B RID: 619
        public const byte stone = 4;

        // Token: 0x0400026C RID: 620
        public const byte wood = 5;

        // Token: 0x0400026D RID: 621
        public const byte shrub = 6;

        // Token: 0x0400026E RID: 622
        public const byte blackrock = 7;

        // Token: 0x0400026F RID: 623
        public const byte water = 8;

        // Token: 0x04000270 RID: 624
        public const byte waterstill = 9;

        // Token: 0x04000271 RID: 625
        public const byte lava = 10;

        // Token: 0x04000272 RID: 626
        public const byte lavastill = 11;

        // Token: 0x04000273 RID: 627
        public const byte sand = 12;

        // Token: 0x04000274 RID: 628
        public const byte gravel = 13;

        // Token: 0x04000275 RID: 629
        public const byte goldrock = 14;

        // Token: 0x04000276 RID: 630
        public const byte ironrock = 15;

        // Token: 0x04000277 RID: 631
        public const byte coal = 16;

        // Token: 0x04000278 RID: 632
        public const byte trunk = 17;

        // Token: 0x04000279 RID: 633
        public const byte leaf = 18;

        // Token: 0x0400027A RID: 634
        public const byte sponge = 19;

        // Token: 0x0400027B RID: 635
        public const byte glass = 20;

        // Token: 0x0400027C RID: 636
        public const byte red = 21;

        // Token: 0x0400027D RID: 637
        public const byte orange = 22;

        // Token: 0x0400027E RID: 638
        public const byte yellow = 23;

        // Token: 0x0400027F RID: 639
        public const byte lightgreen = 24;

        // Token: 0x04000280 RID: 640
        public const byte green = 25;

        // Token: 0x04000281 RID: 641
        public const byte aquagreen = 26;

        // Token: 0x04000282 RID: 642
        public const byte cyan = 27;

        // Token: 0x04000283 RID: 643
        public const byte lightblue = 28;

        // Token: 0x04000284 RID: 644
        public const byte blue = 29;

        // Token: 0x04000285 RID: 645
        public const byte purple = 30;

        // Token: 0x04000286 RID: 646
        public const byte lightpurple = 31;

        // Token: 0x04000287 RID: 647
        public const byte pink = 32;

        // Token: 0x04000288 RID: 648
        public const byte darkpink = 33;

        // Token: 0x04000289 RID: 649
        public const byte darkgrey = 34;

        // Token: 0x0400028A RID: 650
        public const byte lightgrey = 35;

        // Token: 0x0400028B RID: 651
        public const byte white = 36;

        // Token: 0x0400028C RID: 652
        public const byte yellowflower = 37;

        // Token: 0x0400028D RID: 653
        public const byte redflower = 38;

        // Token: 0x0400028E RID: 654
        public const byte mushroom = 39;

        // Token: 0x0400028F RID: 655
        public const byte redmushroom = 40;

        // Token: 0x04000290 RID: 656
        public const byte goldsolid = 41;

        // Token: 0x04000291 RID: 657
        public const byte iron = 42;

        // Token: 0x04000292 RID: 658
        public const byte staircasefull = 43;

        // Token: 0x04000293 RID: 659
        public const byte staircasestep = 44;

        // Token: 0x04000294 RID: 660
        public const byte brick = 45;

        // Token: 0x04000295 RID: 661
        public const byte tnt = 46;

        // Token: 0x04000296 RID: 662
        public const byte bookcase = 47;

        // Token: 0x04000297 RID: 663
        public const byte stonevine = 48;

        // Token: 0x04000298 RID: 664
        public const byte obsidian = 49;

        // Token: 0x04000299 RID: 665
        public const byte Zero = 255;

        // Token: 0x0400029A RID: 666
        public const byte op_glass = 100;

        // Token: 0x0400029B RID: 667
        public const byte opsidian = 101;

        // Token: 0x0400029C RID: 668
        public const byte op_lava = 102;

        // Token: 0x0400029D RID: 669
        public const byte op_stone = 103;

        // Token: 0x0400029E RID: 670
        public const byte op_cobblestone = 104;

        // Token: 0x0400029F RID: 671
        public const byte op_air = 105;

        // Token: 0x040002A0 RID: 672
        public const byte op_water = 106;

        // Token: 0x040002A1 RID: 673
        public const byte wood_float = 110;

        // Token: 0x040002A2 RID: 674
        public const byte door = 111;

        // Token: 0x040002A3 RID: 675
        public const byte lava_fast = 112;

        // Token: 0x040002A4 RID: 676
        public const byte door2 = 113;

        // Token: 0x040002A5 RID: 677
        public const byte door3 = 114;

        // Token: 0x040002A6 RID: 678
        public const byte door4 = 115;

        // Token: 0x040002A7 RID: 679
        public const byte door5 = 116;

        // Token: 0x040002A8 RID: 680
        public const byte door6 = 117;

        // Token: 0x040002A9 RID: 681
        public const byte door7 = 118;

        // Token: 0x040002AA RID: 682
        public const byte door8 = 119;

        // Token: 0x040002AB RID: 683
        public const byte door9 = 120;

        // Token: 0x040002AC RID: 684
        public const byte door10 = 121;

        // Token: 0x040002AD RID: 685
        public const byte tdoor = 122;

        // Token: 0x040002AE RID: 686
        public const byte tdoor2 = 123;

        // Token: 0x040002AF RID: 687
        public const byte tdoor3 = 124;

        // Token: 0x040002B0 RID: 688
        public const byte tdoor4 = 125;

        // Token: 0x040002B1 RID: 689
        public const byte tdoor5 = 126;

        // Token: 0x040002B2 RID: 690
        public const byte tdoor6 = 127;

        // Token: 0x040002B3 RID: 691
        public const byte tdoor7 = 128;

        // Token: 0x040002B4 RID: 692
        public const byte tdoor8 = 129;

        // Token: 0x040002B5 RID: 693
        public const byte MsgWhite = 130;

        // Token: 0x040002B6 RID: 694
        public const byte MsgBlack = 131;

        // Token: 0x040002B7 RID: 695
        public const byte MsgAir = 132;

        // Token: 0x040002B8 RID: 696
        public const byte MsgWater = 133;

        // Token: 0x040002B9 RID: 697
        public const byte MsgLava = 134;

        // Token: 0x040002BA RID: 698
        public const byte tdoor9 = 135;

        // Token: 0x040002BB RID: 699
        public const byte tdoor10 = 136;

        // Token: 0x040002BC RID: 700
        public const byte tdoor11 = 137;

        // Token: 0x040002BD RID: 701
        public const byte tdoor12 = 138;

        // Token: 0x040002BE RID: 702
        public const byte tdoor13 = 139;

        // Token: 0x040002BF RID: 703
        public const byte WaterDown = 140;

        // Token: 0x040002C0 RID: 704
        public const byte LavaDown = 141;

        // Token: 0x040002C1 RID: 705
        public const byte WaterFaucet = 143;

        // Token: 0x040002C2 RID: 706
        public const byte LavaFaucet = 144;

        // Token: 0x040002C3 RID: 707
        public const byte finiteWater = 145;

        // Token: 0x040002C4 RID: 708
        public const byte finiteLava = 146;

        // Token: 0x040002C5 RID: 709
        public const byte finiteFaucet = 147;

        // Token: 0x040002C6 RID: 710
        public const byte odoor1 = 148;

        // Token: 0x040002C7 RID: 711
        public const byte odoor2 = 149;

        // Token: 0x040002C8 RID: 712
        public const byte odoor3 = 150;

        // Token: 0x040002C9 RID: 713
        public const byte odoor4 = 151;

        // Token: 0x040002CA RID: 714
        public const byte odoor5 = 152;

        // Token: 0x040002CB RID: 715
        public const byte odoor6 = 153;

        // Token: 0x040002CC RID: 716
        public const byte odoor7 = 154;

        // Token: 0x040002CD RID: 717
        public const byte odoor8 = 155;

        // Token: 0x040002CE RID: 718
        public const byte odoor9 = 156;

        // Token: 0x040002CF RID: 719
        public const byte odoor10 = 157;

        // Token: 0x040002D0 RID: 720
        public const byte odoor11 = 158;

        // Token: 0x040002D1 RID: 721
        public const byte odoor12 = 159;

        // Token: 0x040002D2 RID: 722
        public const byte air_portal = 160;

        // Token: 0x040002D3 RID: 723
        public const byte water_portal = 161;

        // Token: 0x040002D4 RID: 724
        public const byte lava_portal = 162;

        // Token: 0x040002D5 RID: 725
        public const byte air_door = 164;

        // Token: 0x040002D6 RID: 726
        public const byte air_switch = 165;

        // Token: 0x040002D7 RID: 727
        public const byte water_door = 166;

        // Token: 0x040002D8 RID: 728
        public const byte lava_door = 167;

        // Token: 0x040002D9 RID: 729
        public const byte odoor1_air = 168;

        // Token: 0x040002DA RID: 730
        public const byte odoor2_air = 169;

        // Token: 0x040002DB RID: 731
        public const byte odoor3_air = 170;

        // Token: 0x040002DC RID: 732
        public const byte odoor4_air = 171;

        // Token: 0x040002DD RID: 733
        public const byte odoor5_air = 172;

        // Token: 0x040002DE RID: 734
        public const byte odoor6_air = 173;

        // Token: 0x040002DF RID: 735
        public const byte odoor7_air = 174;

        // Token: 0x040002E0 RID: 736
        public const byte blue_portal = 175;

        // Token: 0x040002E1 RID: 737
        public const byte orange_portal = 176;

        // Token: 0x040002E2 RID: 738
        public const byte odoor8_air = 177;

        // Token: 0x040002E3 RID: 739
        public const byte odoor9_air = 178;

        // Token: 0x040002E4 RID: 740
        public const byte odoor10_air = 179;

        // Token: 0x040002E5 RID: 741
        public const byte odoor11_air = 180;

        // Token: 0x040002E6 RID: 742
        public const byte odoor12_air = 181;

        // Token: 0x040002E7 RID: 743
        public const byte smalltnt = 182;

        // Token: 0x040002E8 RID: 744
        public const byte bigtnt = 183;

        // Token: 0x040002E9 RID: 745
        public const byte tntexplosion = 184;

        // Token: 0x040002EA RID: 746
        public const byte fire = 185;

        // Token: 0x040002EB RID: 747
        public const byte rocketstart = 187;

        // Token: 0x040002EC RID: 748
        public const byte rockethead = 188;

        // Token: 0x040002ED RID: 749
        public const byte firework = 189;

        // Token: 0x040002EE RID: 750
        public const byte deathlava = 190;

        // Token: 0x040002EF RID: 751
        public const byte deathwater = 191;

        // Token: 0x040002F0 RID: 752
        public const byte deathair = 192;

        // Token: 0x040002F1 RID: 753
        public const byte activedeathwater = 193;

        // Token: 0x040002F2 RID: 754
        public const byte activedeathlava = 194;

        // Token: 0x040002F3 RID: 755
        public const byte magma = 195;

        // Token: 0x040002F4 RID: 756
        public const byte geyser = 196;

        // Token: 0x040002F5 RID: 757
        public const byte air_flood = 200;

        // Token: 0x040002F6 RID: 758
        public const byte door_air = 201;

        // Token: 0x040002F7 RID: 759
        public const byte air_flood_layer = 202;

        // Token: 0x040002F8 RID: 760
        public const byte air_flood_down = 203;

        // Token: 0x040002F9 RID: 761
        public const byte air_flood_up = 204;

        // Token: 0x040002FA RID: 762
        public const byte door2_air = 205;

        // Token: 0x040002FB RID: 763
        public const byte door3_air = 206;

        // Token: 0x040002FC RID: 764
        public const byte door4_air = 207;

        // Token: 0x040002FD RID: 765
        public const byte door5_air = 208;

        // Token: 0x040002FE RID: 766
        public const byte door6_air = 209;

        // Token: 0x040002FF RID: 767
        public const byte door7_air = 210;

        // Token: 0x04000300 RID: 768
        public const byte door8_air = 211;

        // Token: 0x04000301 RID: 769
        public const byte door9_air = 212;

        // Token: 0x04000302 RID: 770
        public const byte door10_air = 213;

        // Token: 0x04000303 RID: 771
        public const byte door11_air = 214;

        // Token: 0x04000304 RID: 772
        public const byte door12_air = 215;

        // Token: 0x04000305 RID: 773
        public const byte door13_air = 216;

        // Token: 0x04000306 RID: 774
        public const byte door14_air = 217;

        // Token: 0x04000307 RID: 775
        public const byte door_iron = 220;

        // Token: 0x04000308 RID: 776
        public const byte door_dirt = 221;

        // Token: 0x04000309 RID: 777
        public const byte door_grass = 222;

        // Token: 0x0400030A RID: 778
        public const byte door_blue = 223;

        // Token: 0x0400030B RID: 779
        public const byte door_book = 224;

        // Token: 0x0400030C RID: 780
        public const byte door_iron_air = 225;

        // Token: 0x0400030D RID: 781
        public const byte door_dirt_air = 226;

        // Token: 0x0400030E RID: 782
        public const byte door_grass_air = 227;

        // Token: 0x0400030F RID: 783
        public const byte door_blue_air = 228;

        // Token: 0x04000310 RID: 784
        public const byte door_book_air = 229;

        // Token: 0x04000311 RID: 785
        public const byte train = 230;

        // Token: 0x04000312 RID: 786
        public const byte creeper = 231;

        // Token: 0x04000313 RID: 787
        public const byte zombiebody = 232;

        // Token: 0x04000314 RID: 788
        public const byte zombiehead = 233;

        // Token: 0x04000315 RID: 789
        public const byte birdwhite = 235;

        // Token: 0x04000316 RID: 790
        public const byte birdblack = 236;

        // Token: 0x04000317 RID: 791
        public const byte birdwater = 237;

        // Token: 0x04000318 RID: 792
        public const byte birdlava = 238;

        // Token: 0x04000319 RID: 793
        public const byte birdred = 239;

        // Token: 0x0400031A RID: 794
        public const byte birdblue = 240;

        // Token: 0x0400031B RID: 795
        public const byte birdkill = 242;

        // Token: 0x0400031C RID: 796
        public const byte fishgold = 245;

        // Token: 0x0400031D RID: 797
        public const byte fishsponge = 246;

        // Token: 0x0400031E RID: 798
        public const byte fishshark = 247;

        // Token: 0x0400031F RID: 799
        public const byte fishsalmon = 248;

        // Token: 0x04000320 RID: 800
        public const byte fishbetta = 249;

        // Token: 0x04000321 RID: 801
        public const byte fishlavashark = 250;

        // Token: 0x04000322 RID: 802
        public const byte snake = 251;

        // Token: 0x04000323 RID: 803
        public const byte snaketail = 252;

        // Token: 0x04000324 RID: 804
        public const byte universalsponge = 253;

        // Token: 0x04000325 RID: 805
        public const byte meltingglass = 254;

        // Token: 0x04000326 RID: 806
        public const byte cobbleSlab = 50;

        // Token: 0x04000327 RID: 807
        public const byte rope = 51;

        // Token: 0x04000328 RID: 808
        public const byte sandstone = 52;

        // Token: 0x04000329 RID: 809
        public const byte snow = 53;

        // Token: 0x0400032A RID: 810
        public const byte realFire = 54;

        // Token: 0x0400032B RID: 811
        public const byte lightPink = 55;

        // Token: 0x0400032C RID: 812
        public const byte forestGreen = 56;

        // Token: 0x0400032D RID: 813
        public const byte brown = 57;

        // Token: 0x0400032E RID: 814
        public const byte navy = 58;

        // Token: 0x0400032F RID: 815
        public const byte turquoise = 59;

        // Token: 0x04000330 RID: 816
        public const byte ice = 60;

        // Token: 0x04000331 RID: 817
        public const byte ceramicTile = 61;

        // Token: 0x04000332 RID: 818
        public const byte lavaObsidian = 62;

        // Token: 0x04000333 RID: 819
        public const byte pillar = 63;

        // Token: 0x04000334 RID: 820
        public const byte crate = 64;

        // Token: 0x04000335 RID: 821
        public const byte stoneBrick = 65;

        // Token: 0x04000336 RID: 822
        public const byte blue_gel = 66;

        // Token: 0x04000337 RID: 823
        public const byte red_gel = 67;

        // Token: 0x04000338 RID: 824
        public const byte blue_port = 68;

        // Token: 0x04000339 RID: 825
        public const byte orange_port = 69;

        // Token: 0x0400033A RID: 826
        public const byte meteor = 99;

        // Token: 0x0400033B RID: 827
        public const byte lavaup = 98;

        // Token: 0x0400033C RID: 828
        public const byte treasure = 97;

        // Token: 0x0400033D RID: 829
        public const byte dirtbomb = 96;

        // Token: 0x0400033E RID: 830
        public const byte flagbase = 70;

        // Token: 0x0400033F RID: 831
        public const byte sandstill = 71;

        // Token: 0x04000340 RID: 832
        public const byte door_adminium = 72;

        // Token: 0x04000341 RID: 833
        public const byte door_adminium_air = 73;

        // Token: 0x04000342 RID: 834
        public const byte waterUp = 74;

        // Token: 0x04000343 RID: 835
        public const byte weaktnt = 75;

        // Token: 0x04000344 RID: 836
        public const byte smog = 76;

        // Token: 0x04000345 RID: 837
        public const byte smogbomb = 77;

        // Token: 0x04000346 RID: 838
        public const byte psponge = 78;

        // Token: 0x04000347 RID: 839
        public const byte timedcoal = 79;

        // Token: 0x04000348 RID: 840
        public const byte lavatypea = 80;

        // Token: 0x04000349 RID: 841
        public const byte lavatypeb = 81;

        // Token: 0x0400034A RID: 842
        public const byte lavatypec = 82;

        // Token: 0x0400034B RID: 843
        public const byte lavatyped = 83;

        // Token: 0x0400034C RID: 844
        public static List<Blocks> BlockList = new List<Blocks>();

        // Token: 0x0400034D RID: 845
        public static Dictionary<byte, Blocks> BlocksPermissions = new Dictionary<byte, Blocks>();

        // Token: 0x0400034E RID: 846
        private static readonly byte[] conversion = new byte[256];

        // Token: 0x060005ED RID: 1517 RVA: 0x0001C160 File Offset: 0x0001A360
        public static byte ToMoving(byte block)
        {
            var b = Convert(block);
            if (b == 11) return 10;
            if (b == 9) return 8;
            return b;
        }

        // Token: 0x060005EE RID: 1518 RVA: 0x0001C184 File Offset: 0x0001A384
        public static AABB GetBounds(byte block)
        {
            var b = Convert(block);
            var b2 = b;
            if (b2 <= 44)
            {
                if (b2 == 6) return new AABB(0.1f, 0f, 0.1f, 0.9f, 0.8f, 0.9f);
                switch (b2)
                {
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        return new AABB(0.3f, 0f, 0.3f, 0.7f, 0.6f, 0.7f);
                    case 41:
                    case 42:
                    case 43:
                        goto IL_DF;
                    case 44:
                        break;
                    default:
                        goto IL_DF;
                }
            }
            else if (b2 != 50)
            {
                if (b2 != 53) goto IL_DF;
                return new AABB(0f, 0f, 0f, 1f, 0.2f, 1f);
            }

            return new AABB(0f, 0f, 0f, 1f, 0.5f, 1f);
            IL_DF:
            return new AABB(0f, 0f, 0f, 1f, 1f, 1f);
        }

        // Token: 0x060005EF RID: 1519 RVA: 0x0001C294 File Offset: 0x0001A494
        private static bool xIntersects(AABB bounds, Vector3F var1)
        {
            return var1 != null && var1.Y >= bounds.y0 && var1.Y <= bounds.y1 && var1.Z >= bounds.z0 &&
                   var1.Z <= bounds.z1;
        }

        // Token: 0x060005F0 RID: 1520 RVA: 0x0001C2E4 File Offset: 0x0001A4E4
        private static bool yIntersects(AABB bounds, Vector3F var1)
        {
            return var1 != null && var1.X >= bounds.x0 && var1.X <= bounds.x1 && var1.Z >= bounds.z0 &&
                   var1.Z <= bounds.z1;
        }

        // Token: 0x060005F1 RID: 1521 RVA: 0x0001C334 File Offset: 0x0001A534
        private static bool zIntersects(AABB bounds, Vector3F var1)
        {
            return var1 != null && var1.X >= bounds.x0 && var1.X <= bounds.x1 && var1.Y >= bounds.y0 &&
                   var1.Y <= bounds.y1;
        }

        // Token: 0x060005F2 RID: 1522 RVA: 0x0001C384 File Offset: 0x0001A584
        public static MovingObjectPosition clip(byte block, int x, int y, int z, Vector3F a, Vector3F b)
        {
            a = a.add(-(float) x, -(float) y, -(float) z);
            b = b.add(-(float) x, -(float) y, -(float) z);
            var bounds = GetBounds(block);
            var vector3F = a.getXIntersection(b, bounds.x0);
            var vector3F2 = a.getXIntersection(b, bounds.x1);
            var vector3F3 = a.getYIntersection(b, bounds.y0);
            var vector3F4 = a.getYIntersection(b, bounds.y1);
            var vector3F5 = a.getZIntersection(b, bounds.z0);
            b = a.getZIntersection(b, bounds.z1);
            if (!xIntersects(bounds, vector3F)) vector3F = null;
            if (!xIntersects(bounds, vector3F2)) vector3F2 = null;
            if (!yIntersects(bounds, vector3F3)) vector3F3 = null;
            if (!yIntersects(bounds, vector3F4)) vector3F4 = null;
            if (!zIntersects(bounds, vector3F5)) vector3F5 = null;
            if (!zIntersects(bounds, b)) b = null;
            Vector3F vector3F6 = null;
            if (vector3F != null) vector3F6 = vector3F;
            if (vector3F2 != null && (vector3F6 == null || a.distance(vector3F2) < a.distance(vector3F6)))
                vector3F6 = vector3F2;
            if (vector3F3 != null && (vector3F6 == null || a.distance(vector3F3) < a.distance(vector3F6)))
                vector3F6 = vector3F3;
            if (vector3F4 != null && (vector3F6 == null || a.distance(vector3F4) < a.distance(vector3F6)))
                vector3F6 = vector3F4;
            if (vector3F5 != null && (vector3F6 == null || a.distance(vector3F5) < a.distance(vector3F6)))
                vector3F6 = vector3F5;
            if (b != null && (vector3F6 == null || a.distance(b) < a.distance(vector3F6))) vector3F6 = b;
            if (vector3F6 == null) return null;
            sbyte side = -1;
            if (vector3F6 == vector3F) side = 4;
            if (vector3F6 == vector3F2) side = 5;
            if (vector3F6 == vector3F3) side = 0;
            if (vector3F6 == vector3F4) side = 1;
            if (vector3F6 == vector3F5) side = 2;
            if (vector3F6 == b) side = 3;
            return new MovingObjectPosition(x, y, z, side, vector3F6.add(x, y, z));
        }

        // Token: 0x060005F3 RID: 1523 RVA: 0x0001C560 File Offset: 0x0001A760
        public static bool IsLiquid(byte block)
        {
            switch (Convert(block))
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return true;
                default:
                    return false;
            }
        }

        // Token: 0x060005F4 RID: 1524 RVA: 0x0001C594 File Offset: 0x0001A794
        public static AABB GetCollisionBox(byte block)
        {
            var b = Convert(block);
            var b2 = b;
            if (b2 > 11)
            {
                switch (b2)
                {
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        goto IL_A9;
                    case 41:
                    case 42:
                    case 43:
                        goto IL_AB;
                    case 44:
                        break;
                    default:
                        switch (b2)
                        {
                            case 50:
                                break;
                            case 51:
                            case 53:
                                goto IL_A9;
                            case 52:
                                goto IL_AB;
                            default:
                                if (b2 != 185) goto IL_AB;
                                goto IL_A9;
                        }

                        break;
                }

                return new AABB(0f, 0f, 0f, 1f, 0.5f, 1f);
            }

            if (b2 != 0)
                switch (b2)
                {
                    case 6:
                        break;
                    case 7:
                        goto IL_AB;
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                        return null;
                    default:
                        goto IL_AB;
                }

            IL_A9:
            return null;
            IL_AB:
            return new AABB(0f, 0f, 0f, 1f, 1f, 1f);
        }

        // Token: 0x060005F5 RID: 1525 RVA: 0x0001C670 File Offset: 0x0001A870
        public static AABB GetCollisionBox(byte block, int x, int y, int z)
        {
            var collisionBox = GetCollisionBox(block);
            if (collisionBox != null) collisionBox.move(x, y, z);
            return collisionBox;
        }

        // Token: 0x060005F6 RID: 1526 RVA: 0x0001C694 File Offset: 0x0001A894
        public static bool IsAir(byte tile)
        {
            return tile == 0 || tile == 105;
        }

        // Token: 0x060005F7 RID: 1527 RVA: 0x0001C6A4 File Offset: 0x0001A8A4
        public static void SetBlocks()
        {
            FillConversion();
            BlockList = new List<Blocks>();
            var blocks = new Blocks();
            blocks.lowestRank = LevelPermission.Guest;
            for (var i = 0; i < 256; i++)
            {
                blocks = new Blocks();
                blocks.type = (byte) i;
                BlockList.Add(blocks);
            }

            var list = new List<Blocks>();
            foreach (var blocks2 in BlockList)
            {
                blocks = new Blocks();
                blocks.type = blocks2.type;
                switch (blocks2.type)
                {
                    case 7:
                    case 70:
                    case 78:
                    case 100:
                    case 101:
                    case 102:
                    case 103:
                    case 104:
                    case 105:
                    case 106:
                    case 183:
                    case 187:
                    case 188:
                    case 200:
                    case 202:
                    case 203:
                    case 204:
                    case 231:
                    case 232:
                    case 233:
                    case 239:
                    case 240:
                    case 242:
                    case 245:
                    case 246:
                    case 247:
                    case 248:
                    case 249:
                    case 250:
                    case 251:
                    case 252:
                        blocks.lowestRank = LevelPermission.Operator;
                        break;
                    case 8:
                    case 10:
                    case 73:
                    case 74:
                    case 75:
                    case 76:
                    case 77:
                    case 80:
                    case 81:
                    case 82:
                    case 83:
                    case 98:
                    case 99:
                    case 110:
                    case 112:
                    case 130:
                    case 131:
                    case 132:
                    case 133:
                    case 134:
                    case 140:
                    case 141:
                    case 143:
                    case 144:
                    case 145:
                    case 146:
                    case 147:
                    case 160:
                    case 161:
                    case 162:
                    case 168:
                    case 169:
                    case 170:
                    case 171:
                    case 172:
                    case 173:
                    case 174:
                    case 175:
                    case 176:
                    case 177:
                    case 178:
                    case 179:
                    case 180:
                    case 181:
                    case 182:
                    case 184:
                    case 185:
                    case 189:
                    case 190:
                    case 191:
                    case 192:
                    case 193:
                    case 194:
                    case 195:
                    case 196:
                    case 201:
                    case 205:
                    case 206:
                    case 207:
                    case 208:
                    case 209:
                    case 210:
                    case 211:
                    case 212:
                    case 213:
                    case 214:
                    case 215:
                    case 216:
                    case 217:
                    case 225:
                    case 226:
                    case 227:
                    case 228:
                    case 229:
                    case 230:
                    case 235:
                    case 236:
                    case 237:
                    case 238:
                        blocks.lowestRank = LevelPermission.AdvBuilder;
                        break;
                    case 9:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 41:
                    case 42:
                    case 43:
                    case 44:
                    case 45:
                    case 46:
                    case 47:
                    case 48:
                    case 49:
                    case 50:
                    case 51:
                    case 52:
                    case 53:
                    case 54:
                    case 55:
                    case 56:
                    case 57:
                    case 58:
                    case 59:
                    case 60:
                    case 61:
                    case 62:
                    case 63:
                    case 64:
                    case 65:
                    case 66:
                    case 67:
                    case 68:
                    case 69:
                    case 71:
                    case 79:
                    case 84:
                    case 85:
                    case 86:
                    case 87:
                    case 88:
                    case 89:
                    case 90:
                    case 91:
                    case 92:
                    case 93:
                    case 94:
                    case 95:
                    case 107:
                    case 108:
                    case 109:
                    case 142:
                    case 163:
                    case 186:
                    case 197:
                    case 198:
                    case 199:
                    case 218:
                    case 219:
                    case 234:
                    case 241:
                    case 243:
                    case 244:
                    case 253:
                    case 254:
                        goto IL_495;
                    case 72:
                    case 96:
                    case 111:
                    case 113:
                    case 114:
                    case 115:
                    case 116:
                    case 117:
                    case 118:
                    case 119:
                    case 120:
                    case 121:
                    case 122:
                    case 123:
                    case 124:
                    case 125:
                    case 126:
                    case 127:
                    case 128:
                    case 129:
                    case 135:
                    case 136:
                    case 137:
                    case 138:
                    case 139:
                    case 148:
                    case 149:
                    case 150:
                    case 151:
                    case 152:
                    case 153:
                    case 154:
                    case 155:
                    case 156:
                    case 157:
                    case 158:
                    case 159:
                    case 164:
                    case 165:
                    case 166:
                    case 167:
                    case 220:
                    case 221:
                    case 222:
                    case 223:
                    case 224:
                        blocks.lowestRank = LevelPermission.Builder;
                        break;
                    case 97:
                    case 255:
                        blocks.lowestRank = LevelPermission.Admin;
                        break;
                    default:
                        goto IL_495;
                }

                IL_49D:
                list.Add(blocks);
                continue;
                IL_495:
                blocks.lowestRank = LevelPermission.Banned;
                goto IL_49D;
            }

            if (File.Exists("properties/block.properties"))
            {
                var array = File.ReadAllLines("properties/block.properties");
                if (array[0] == "#Version 2")
                {
                    string[] separator =
                    {
                        " : "
                    };
                    foreach (var text in array)
                    {
                        if (text != "" && text[0] != '#')
                        {
                            var array3 = text.Split(separator, StringSplitOptions.None);
                            var blocks3 = new Blocks();
                            if (Byte(array3[0]) != 255)
                            {
                                blocks3.type = Byte(array3[0]);
                                var array4 = new string[0];
                                if (array3[2] != "") array4 = array3[2].Split(',');
                                var array5 = new string[0];
                                if (array3[3] != "") array5 = array3[3].Split(',');
                                try
                                {
                                    blocks3.lowestRank = (LevelPermission) int.Parse(array3[1]);
                                    foreach (var s in array4) blocks3.disallow.Add((LevelPermission) int.Parse(s));
                                    foreach (var s2 in array5) blocks3.allow.Add((LevelPermission) int.Parse(s2));
                                }
                                catch
                                {
                                    Server.s.Log("Hit an error on the block " + text);
                                    goto IL_6B1;
                                }

                                var num = 0;
                                foreach (var blocks4 in list)
                                {
                                    if (blocks3.type == blocks4.type)
                                    {
                                        list[num] = blocks3;
                                        break;
                                    }

                                    num++;
                                }
                            }
                        }

                        IL_6B1: ;
                    }
                }
                else
                {
                    foreach (var text2 in array)
                        if (text2[0] != '#')
                            try
                            {
                                var newBlock = new Blocks();
                                newBlock.type = Byte(text2.Split(' ')[0]);
                                newBlock.lowestRank = Level.PermissionFromName(text2.Split(' ')[2]);
                                if (newBlock.lowestRank == LevelPermission.Null) throw new Exception();
                                list[list.FindIndex(sL => sL.type == newBlock.type)] = newBlock;
                            }
                            catch
                            {
                                Server.s.Log("Could not find the rank given on " + text2 + ". Using default");
                            }
                }
            }

            BlockList.Clear();
            BlockList.AddRange(list);
            SaveBlocks(BlockList);
        }

        // Token: 0x060005F8 RID: 1528 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
        public static void SaveBlocks(List<Blocks> givenList)
        {
            try
            {
                using (var streamWriter = new StreamWriter(File.Create("properties/block.properties")))
                {
                    streamWriter.WriteLine("#Version 2");
                    streamWriter.WriteLine("#   This file dictates what levels may use what blocks");
                    streamWriter.WriteLine(
                        "#   If someone has royally screwed up the ranks, just delete this file and let the server restart");
                    streamWriter.WriteLine("#   Allowed ranks: " + Group.concatList(false, false, true));
                    streamWriter.WriteLine(
                        "#   Disallow and allow can be left empty, just make sure there's 2 spaces between the colons");
                    streamWriter.WriteLine(
                        "#   This works entirely on permission values, not names. Do not enter a rank name. Use it's permission value");
                    streamWriter.WriteLine("#   BlockName : LowestRank : Disallow : Allow");
                    streamWriter.WriteLine("#   lava : 60 : 80,67 : 40,41,55");
                    streamWriter.WriteLine("");
                    foreach (var blocks in givenList)
                        if (Name(blocks.type).ToLower() != "unknown")
                        {
                            var value = string.Concat(Name(blocks.type), " : ", (int) blocks.lowestRank, " : ",
                                GrpCommands.getInts(blocks.disallow), " : ", GrpCommands.getInts(blocks.allow));
                            streamWriter.WriteLine(value);
                        }
                }
            }
            catch (Exception ex)
            {
                Server.ErrorLog(ex);
            }
        }

        // Token: 0x060005F9 RID: 1529 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
        public static bool canPlace(Player p, byte b)
        {
            return canPlace(p.group.Permission, b);
        }

        // Token: 0x060005FA RID: 1530 RVA: 0x0001D0B4 File Offset: 0x0001B2B4
        public static bool canPlace(LevelPermission givenPerm, byte givenBlock)
        {
            foreach (var blocks in BlockList)
                if (givenBlock == blocks.type)
                {
                    if (blocks.lowestRank <= givenPerm && !blocks.disallow.Contains(givenPerm) ||
                        blocks.allow.Contains(givenPerm)) return true;
                    return false;
                }

            return false;
        }

        // Token: 0x060005FB RID: 1531 RVA: 0x0001D138 File Offset: 0x0001B338
        public static byte ConvertFromBlockmension(byte block)
        {
            switch (block)
            {
                case 66:
                    return 29;
                case 67:
                    return 21;
                case 68:
                    return 8;
                case 69:
                    return 10;
                default:
                    return block;
            }
        }

        // Token: 0x060005FC RID: 1532 RVA: 0x0001D170 File Offset: 0x0001B370
        public static byte ConvertExtended(byte block)
        {
            switch (block)
            {
                case 50:
                    return 44;
                case 51:
                    return 39;
                case 52:
                    return 12;
                case 53:
                    return 0;
                case 54:
                    return 10;
                case 55:
                    return 32;
                case 56:
                    return 25;
                case 57:
                    return 3;
                case 58:
                    return 29;
                case 59:
                    return 27;
                case 60:
                    return 20;
                case 61:
                    return 42;
                case 62:
                    return 49;
                case 63:
                    return 36;
                case 64:
                    return 5;
                case 65:
                    return 4;
                case 66:
                    return 29;
                case 67:
                    return 21;
                case 68:
                    return 8;
                case 69:
                    return 10;
                default:
                    return block;
            }
        }

        // Token: 0x060005FD RID: 1533 RVA: 0x0001D214 File Offset: 0x0001B414
        public static bool Walkthrough(byte type)
        {
            type = Convert(type);
            var b = type;
            if (b <= 11)
            {
                if (b != 0)
                    switch (b)
                    {
                        case 6:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            break;
                        case 7:
                            return false;
                        default:
                            return false;
                    }
            }
            else
            {
                switch (b)
                {
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        break;
                    default:
                        switch (b)
                        {
                            case 51:
                            case 53:
                            case 54:
                                break;
                            case 52:
                                return false;
                            default:
                                return false;
                        }

                        break;
                }
            }

            return true;
        }

        // Token: 0x060005FE RID: 1534 RVA: 0x0001D290 File Offset: 0x0001B490
        public static bool IsDoor(byte type)
        {
            switch (type)
            {
                case 72:
                case 73:
                    break;
                default:
                    switch (type)
                    {
                        case 111:
                        case 113:
                        case 114:
                        case 115:
                        case 116:
                        case 117:
                        case 118:
                        case 119:
                        case 120:
                        case 121:
                        case 122:
                        case 123:
                        case 124:
                        case 125:
                        case 126:
                        case 127:
                        case 128:
                        case 129:
                        case 135:
                        case 136:
                        case 137:
                        case 138:
                        case 139:
                        case 148:
                        case 149:
                        case 150:
                        case 151:
                        case 152:
                        case 153:
                        case 154:
                        case 155:
                        case 156:
                        case 157:
                        case 158:
                        case 159:
                        case 164:
                        case 165:
                        case 166:
                        case 167:
                        case 168:
                        case 169:
                        case 170:
                        case 171:
                        case 172:
                        case 173:
                        case 174:
                        case 177:
                        case 178:
                        case 179:
                        case 180:
                        case 181:
                        case 201:
                        case 205:
                        case 206:
                        case 207:
                        case 208:
                        case 209:
                        case 210:
                        case 211:
                        case 212:
                        case 213:
                        case 214:
                        case 215:
                        case 216:
                        case 217:
                        case 220:
                        case 221:
                        case 222:
                        case 223:
                        case 224:
                        case 225:
                        case 226:
                        case 227:
                        case 228:
                        case 229:
                            return true;
                    }

                    return false;
            }

            return true;
        }

        // Token: 0x060005FF RID: 1535 RVA: 0x0001D49C File Offset: 0x0001B69C
        public static bool Standable(byte type)
        {
            if (type == 255) return true;
            type = Convert(type);
            var b = type;
            if (b <= 11)
            {
                if (b != 0)
                    switch (b)
                    {
                        case 6:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            break;
                        case 7:
                            return false;
                        default:
                            return false;
                    }
            }
            else
            {
                switch (b)
                {
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        break;
                    default:
                        switch (b)
                        {
                            case 51:
                            case 53:
                                break;
                            case 52:
                                return false;
                            default:
                                if (b != 185) return false;
                                break;
                        }

                        break;
                }
            }

            return true;
        }

        // Token: 0x06000600 RID: 1536 RVA: 0x0001D524 File Offset: 0x0001B724
        public static bool AnyBuild(byte type)
        {
            switch (type)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 71:
                    return true;
            }

            return false;
        }

        // Token: 0x06000601 RID: 1537 RVA: 0x0001D660 File Offset: 0x0001B860
        public static bool AllowBreak(byte type)
        {
            if (type <= 131)
            {
                if (type <= 76)
                {
                    if (type != 72 && type != 76) return false;
                }
                else if (type != 97)
                {
                    switch (type)
                    {
                        case 111:
                        case 113:
                        case 114:
                        case 115:
                        case 116:
                        case 117:
                        case 118:
                        case 119:
                        case 120:
                        case 121:
                        case 130:
                        case 131:
                            break;
                        case 112:
                        case 122:
                        case 123:
                        case 124:
                        case 125:
                        case 126:
                        case 127:
                        case 128:
                        case 129:
                            return false;
                        default:
                            return false;
                    }
                }
            }
            else if (type <= 183)
            {
                switch (type)
                {
                    case 175:
                    case 176:
                        break;
                    default:
                        switch (type)
                        {
                            case 182:
                            case 183:
                                break;
                            default:
                                return false;
                        }

                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case 187:
                    case 189:
                        break;
                    case 188:
                        return false;
                    default:
                        switch (type)
                        {
                            case 220:
                            case 221:
                            case 222:
                            case 223:
                            case 224:
                            case 231:
                            case 232:
                            case 233:
                                break;
                            case 225:
                            case 226:
                            case 227:
                            case 228:
                            case 229:
                            case 230:
                                return false;
                            default:
                                return false;
                        }

                        break;
                }
            }

            return true;
        }

        // Token: 0x06000602 RID: 1538 RVA: 0x0001D794 File Offset: 0x0001B994
        public static bool Placable(byte type)
        {
            switch (type)
            {
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                    return false;
                default:
                    return type <= 49;
            }
        }

        // Token: 0x06000603 RID: 1539 RVA: 0x0001D7CC File Offset: 0x0001B9CC
        public static bool RightClick(byte type, bool countAir = false)
        {
            if (countAir && type == 0) return true;
            switch (type)
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return true;
                default:
                    return false;
            }
        }

        // Token: 0x06000604 RID: 1540 RVA: 0x0001D800 File Offset: 0x0001BA00
        public static bool OPBlocks(byte type)
        {
            if (type <= 106)
            {
                if (type != 7)
                    switch (type)
                    {
                        case 100:
                        case 101:
                        case 102:
                        case 103:
                        case 104:
                        case 105:
                        case 106:
                            break;
                        default:
                            return false;
                    }
            }
            else if (type != 187 && type != 255)
            {
                return false;
            }

            return true;
        }

        // Token: 0x06000605 RID: 1541 RVA: 0x0001D854 File Offset: 0x0001BA54
        public static bool Death(byte type)
        {
            if (type <= 98)
            {
                if (type != 74)
                    switch (type)
                    {
                        case 80:
                        case 81:
                        case 82:
                        case 83:
                            break;
                        default:
                            if (type != 98) return false;
                            break;
                    }
            }
            else if (type <= 232)
            {
                switch (type)
                {
                    case 184:
                    case 185:
                    case 188:
                    case 190:
                    case 191:
                    case 192:
                    case 193:
                    case 194:
                    case 195:
                    case 196:
                        break;
                    case 186:
                    case 187:
                    case 189:
                        return false;
                    default:
                        switch (type)
                        {
                            case 230:
                            case 231:
                            case 232:
                                break;
                            default:
                                return false;
                        }

                        break;
                }
            }
            else if (type != 242)
            {
                switch (type)
                {
                    case 247:
                    case 250:
                    case 251:
                        break;
                    case 248:
                    case 249:
                        return false;
                    default:
                        return false;
                }
            }

            return true;
        }

        // Token: 0x06000606 RID: 1542 RVA: 0x0001D928 File Offset: 0x0001BB28
        public static bool BuildIn(byte type)
        {
            if (type == 106 || portal(type) || mb(type)) return false;
            switch (Convert(type))
            {
                case 8:
                case 9:
                case 10:
                case 11:
                    return true;
                default:
                    return false;
            }
        }

        // Token: 0x06000607 RID: 1543 RVA: 0x0001D970 File Offset: 0x0001BB70
        public static bool Mover(byte type)
        {
            if (type != 70)
                switch (type)
                {
                    case 132:
                    case 133:
                    case 134:
                        break;
                    default:
                        switch (type)
                        {
                            case 160:
                            case 161:
                            case 162:
                            case 165:
                            case 166:
                            case 167:
                                return true;
                        }

                        return false;
                }

            return true;
        }

        // Token: 0x06000608 RID: 1544 RVA: 0x0001D9D0 File Offset: 0x0001BBD0
        public static bool LavaKill(byte type)
        {
            switch (type)
            {
                case 5:
                case 6:
                case 17:
                case 18:
                case 19:
                case 21:
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 47:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 68:
                case 69:
                    return true;
            }

            return false;
        }

        // Token: 0x06000609 RID: 1545 RVA: 0x0001DAF0 File Offset: 0x0001BCF0
        public static bool WaterKill(byte type)
        {
            if (type <= 6)
            {
                if (type != 0 && type != 6) return false;
            }
            else if (type != 18)
            {
                switch (type)
                {
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        break;
                    default:
                        switch (type)
                        {
                            case 53:
                            case 54:
                                break;
                            default:
                                return false;
                        }

                        break;
                }
            }

            return true;
        }

        // Token: 0x0600060A RID: 1546 RVA: 0x0001DB44 File Offset: 0x0001BD44
        public static bool LightPass(byte type)
        {
            var b = Convert(type);
            if (b <= 20)
            {
                if (b != 0 && b != 6)
                    switch (b)
                    {
                        case 18:
                        case 20:
                            break;
                        case 19:
                            return false;
                        default:
                            return false;
                    }
            }
            else
            {
                switch (b)
                {
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                        break;
                    default:
                        if (b != 51 && b != 60) return false;
                        break;
                }
            }

            return true;
        }

        // Token: 0x0600060B RID: 1547 RVA: 0x0001DBA4 File Offset: 0x0001BDA4
        public static bool NeedRestart(byte type)
        {
            if (type != 76)
            {
                switch (type)
                {
                    case 184:
                    case 185:
                    case 188:
                    case 189:
                        return true;
                    case 186:
                    case 187:
                        break;
                    default:
                        switch (type)
                        {
                            case 230:
                            case 231:
                            case 232:
                            case 233:
                            case 235:
                            case 236:
                            case 237:
                            case 238:
                            case 239:
                            case 240:
                            case 242:
                            case 245:
                            case 246:
                            case 247:
                            case 248:
                            case 249:
                            case 250:
                            case 251:
                            case 252:
                                return true;
                        }

                        break;
                }

                return false;
            }

            return true;
        }

        // Token: 0x0600060C RID: 1548 RVA: 0x0001DC4C File Offset: 0x0001BE4C
        public static bool portal(byte type)
        {
            switch (type)
            {
                case 160:
                case 161:
                case 162:
                    break;
                default:
                    switch (type)
                    {
                        case 175:
                        case 176:
                            break;
                        default:
                            return false;
                    }

                    break;
            }

            return true;
        }

        // Token: 0x0600060D RID: 1549 RVA: 0x0001DC8C File Offset: 0x0001BE8C
        public static bool mb(byte type)
        {
            switch (type)
            {
                case 130:
                case 131:
                case 132:
                case 133:
                case 134:
                    return true;
                default:
                    return false;
            }
        }

        // Token: 0x0600060E RID: 1550 RVA: 0x0001DCC0 File Offset: 0x0001BEC0
        public static bool Physics(byte type)
        {
            if (type <= 139)
                switch (type)
                {
                    case 1:
                    case 4:
                    case 7:
                    case 9:
                    case 11:
                    case 14:
                    case 15:
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                    case 36:
                    case 41:
                    case 42:
                    case 43:
                    case 45:
                    case 46:
                    case 48:
                    case 49:
                        break;
                    case 2:
                    case 3:
                    case 5:
                    case 6:
                    case 8:
                    case 10:
                    case 12:
                    case 13:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                    case 37:
                    case 38:
                    case 39:
                    case 40:
                    case 44:
                    case 47:
                        return true;
                    default:
                        switch (type)
                        {
                            case 70:
                            case 72:
                            case 100:
                            case 101:
                            case 102:
                            case 103:
                            case 104:
                            case 105:
                            case 106:
                            case 111:
                            case 113:
                            case 114:
                            case 115:
                            case 116:
                            case 117:
                            case 118:
                            case 119:
                            case 120:
                            case 121:
                            case 122:
                            case 123:
                            case 124:
                            case 125:
                            case 126:
                            case 127:
                            case 128:
                            case 129:
                            case 130:
                            case 131:
                            case 132:
                            case 133:
                            case 134:
                            case 135:
                            case 136:
                            case 137:
                            case 138:
                            case 139:
                                break;
                            case 71:
                            case 73:
                            case 74:
                            case 75:
                            case 76:
                            case 77:
                            case 78:
                            case 79:
                            case 80:
                            case 81:
                            case 82:
                            case 83:
                            case 84:
                            case 85:
                            case 86:
                            case 87:
                            case 88:
                            case 89:
                            case 90:
                            case 91:
                            case 92:
                            case 93:
                            case 94:
                            case 95:
                            case 96:
                            case 97:
                            case 98:
                            case 99:
                            case 107:
                            case 108:
                            case 109:
                            case 110:
                            case 112:
                                return true;
                            default:
                                return true;
                        }

                        break;
                }
            else
                switch (type)
                {
                    case 160:
                    case 161:
                    case 162:
                    case 164:
                    case 165:
                    case 166:
                    case 167:
                    case 175:
                    case 176:
                        break;
                    case 163:
                    case 168:
                    case 169:
                    case 170:
                    case 171:
                    case 172:
                    case 173:
                    case 174:
                        return true;
                    default:
                        switch (type)
                        {
                            case 190:
                            case 191:
                            case 192:
                                break;
                            default:
                                switch (type)
                                {
                                    case 220:
                                    case 221:
                                    case 222:
                                    case 223:
                                    case 224:
                                        break;
                                    default:
                                        return true;
                                }

                                break;
                        }

                        break;
                }

            return false;
        }

        // Token: 0x0600060F RID: 1551 RVA: 0x0001DF5C File Offset: 0x0001C15C
        public static string Name(byte type)
        {
            switch (type)
            {
                case 0:
                    return "air";
                case 1:
                    return "stone";
                case 2:
                    return "grass";
                case 3:
                    return "dirt";
                case 4:
                    return "cobblestone";
                case 5:
                    return "wood";
                case 6:
                    return "plant";
                case 7:
                    return "adminium";
                case 8:
                    return "active_water";
                case 9:
                    return "water";
                case 10:
                    return "active_lava";
                case 11:
                    return "lava";
                case 12:
                    return "sand";
                case 13:
                    return "gravel";
                case 14:
                    return "gold_ore";
                case 15:
                    return "iron_ore";
                case 16:
                    return "coal";
                case 17:
                    return "tree";
                case 18:
                    return "leaves";
                case 19:
                    return "sponge";
                case 20:
                    return "glass";
                case 21:
                    return "red";
                case 22:
                    return "orange";
                case 23:
                    return "yellow";
                case 24:
                    return "greenyellow";
                case 25:
                    return "green";
                case 26:
                    return "springgreen";
                case 27:
                    return "cyan";
                case 28:
                    return "blue";
                case 29:
                    return "blueviolet";
                case 30:
                    return "indigo";
                case 31:
                    return "purple";
                case 32:
                    return "magenta";
                case 33:
                    return "pink";
                case 34:
                    return "black";
                case 35:
                    return "gray";
                case 36:
                    return "white";
                case 37:
                    return "yellow_flower";
                case 38:
                    return "red_flower";
                case 39:
                    return "brown_shroom";
                case 40:
                    return "red_shroom";
                case 41:
                    return "gold";
                case 42:
                    return "iron";
                case 43:
                    return "double_stair";
                case 44:
                    return "stair";
                case 45:
                    return "brick";
                case 46:
                    return "tnt";
                case 47:
                    return "bookcase";
                case 48:
                    return "mossy_cobblestone";
                case 49:
                    return "obsidian";
                case 50:
                    return "cobble_slab";
                case 51:
                    return "rope";
                case 52:
                    return "sandstone";
                case 53:
                    return "snow";
                case 54:
                    return "fire";
                case 55:
                    return "light_pink";
                case 56:
                    return "forest_green";
                case 57:
                    return "brown";
                case 58:
                    return "navy";
                case 59:
                    return "turquoise";
                case 60:
                    return "ice";
                case 61:
                    return "ceramic_tile";
                case 62:
                    return "lava_obsidian";
                case 63:
                    return "pillar";
                case 64:
                    return "crate";
                case 65:
                    return "stone_brick";
                case 66:
                    return "blue_gel";
                case 67:
                    return "red_gel";
                case 68:
                    return "portal_blue";
                case 69:
                    return "portal_orange";
                case 70:
                    return "flagbase";
                case 71:
                    return "still_sand";
                case 72:
                    return "door_adminium";
                case 73:
                    return "door_adminium_air";
                case 74:
                    return "waterup";
                case 75:
                    return "weak_tnt";
                case 77:
                    return "smog_bomb";
                case 78:
                    return "psponge";
                case 80:
                    return "lava_type_a";
                case 81:
                    return "lava_type_b";
                case 82:
                    return "lava_type_c";
                case 83:
                    return "lava_type_d";
                case 96:
                    return "dirt_bomb";
                case 97:
                    return "treasure";
                case 98:
                    return "lavaup";
                case 99:
                    return "meteor";
                case 100:
                    return "op_glass";
                case 101:
                    return "opsidian";
                case 102:
                    return "op_lava";
                case 103:
                    return "op_stone";
                case 104:
                    return "op_cobblestone";
                case 105:
                    return "op_air";
                case 106:
                    return "op_water";
                case 110:
                    return "wood_float";
                case 111:
                    return "door_tree";
                case 112:
                    return "lava_fast";
                case 113:
                    return "door_obsidian";
                case 114:
                    return "door_glass";
                case 115:
                    return "door_stone";
                case 116:
                    return "door_leaves";
                case 117:
                    return "door_sand";
                case 118:
                    return "door_wood";
                case 119:
                    return "door_green";
                case 120:
                    return "door_tnt";
                case 121:
                    return "door_stair";
                case 122:
                    return "tdoor_tree";
                case 123:
                    return "tdoor_obsidian";
                case 124:
                    return "tdoor_glass";
                case 125:
                    return "tdoor_stone";
                case 126:
                    return "tdoor_leaves";
                case 127:
                    return "tdoor_sand";
                case 128:
                    return "tdoor_wood";
                case 129:
                    return "tdoor_green";
                case 130:
                    return "white_message";
                case 131:
                    return "black_message";
                case 132:
                    return "air_message";
                case 133:
                    return "water_message";
                case 134:
                    return "lava_message";
                case 135:
                    return "tdoor_tnt";
                case 136:
                    return "tdoor_stair";
                case 137:
                    return "tdoor_air";
                case 138:
                    return "tdoor_water";
                case 139:
                    return "tdoor_lava";
                case 140:
                    return "waterfall";
                case 141:
                    return "lavafall";
                case 143:
                    return "water_faucet";
                case 144:
                    return "lava_faucet";
                case 145:
                    return "finite_water";
                case 146:
                    return "finite_lava";
                case 147:
                    return "finite_faucet";
                case 148:
                    return "odoor_tree";
                case 149:
                    return "odoor_obsidian";
                case 150:
                    return "odoor_glass";
                case 151:
                    return "odoor_stone";
                case 152:
                    return "odoor_leaves";
                case 153:
                    return "odoor_sand";
                case 154:
                    return "odoor_wood";
                case 155:
                    return "odoor_green";
                case 156:
                    return "odoor_tnt";
                case 157:
                    return "odoor_stair";
                case 158:
                    return "odoor_lava";
                case 159:
                    return "odoor_water";
                case 160:
                    return "air_portal";
                case 161:
                    return "water_portal";
                case 162:
                    return "lava_portal";
                case 164:
                    return "air_door";
                case 165:
                    return "air_switch";
                case 166:
                    return "door_water";
                case 167:
                    return "door_lava";
                case 168:
                    return "odoor_tree_air";
                case 169:
                    return "odoor_obsidian_air";
                case 170:
                    return "odoor_glass_air";
                case 171:
                    return "odoor_stone_air";
                case 172:
                    return "odoor_leaves_air";
                case 173:
                    return "odoor_sand_air";
                case 174:
                    return "odoor_wood_air";
                case 175:
                    return "blue_portal";
                case 176:
                    return "orange_portal";
                case 177:
                    return "odoor_red";
                case 178:
                    return "odoor_tnt_air";
                case 179:
                    return "odoor_stair_air";
                case 180:
                    return "odoor_lava_air";
                case 181:
                    return "odoor_water_air";
                case 182:
                    return "small_tnt";
                case 183:
                    return "big_tnt";
                case 184:
                    return "tnt_explosion";
                case 185:
                    return "firelava";
                case 187:
                    return "rocketstart";
                case 188:
                    return "rockethead";
                case 189:
                    return "firework";
                case 190:
                    return "hot_lava";
                case 191:
                    return "cold_water";
                case 192:
                    return "nerve_gas";
                case 193:
                    return "active_cold_water";
                case 194:
                    return "active_hot_lava";
                case 195:
                    return "magma";
                case 196:
                    return "geyser";
                case 200:
                    return "air_flood";
                case 201:
                    return "door_air";
                case 202:
                    return "air_flood_layer";
                case 203:
                    return "air_flood_down";
                case 204:
                    return "air_flood_up";
                case 205:
                    return "door2_air";
                case 206:
                    return "door3_air";
                case 207:
                    return "door4_air";
                case 208:
                    return "door5_air";
                case 209:
                    return "door6_air";
                case 210:
                    return "door7_air";
                case 211:
                    return "door8_air";
                case 212:
                    return "door9_air";
                case 213:
                    return "door10_air";
                case 214:
                    return "door11_air";
                case 215:
                    return "door12_air";
                case 216:
                    return "door13_air";
                case 217:
                    return "door14_air";
                case 220:
                    return "door_iron";
                case 221:
                    return "door_dirt";
                case 222:
                    return "door_grass";
                case 223:
                    return "door_blue";
                case 224:
                    return "door_book";
                case 225:
                    return "door_iron_air";
                case 226:
                    return "door_dirt_air";
                case 227:
                    return "door_grass_air";
                case 228:
                    return "door_blue_air";
                case 229:
                    return "door_book_air";
                case 230:
                    return "train";
                case 231:
                    return "creeper";
                case 232:
                    return "zombie";
                case 233:
                    return "zombie_head";
                case 235:
                    return "dove";
                case 236:
                    return "pidgeon";
                case 237:
                    return "duck";
                case 238:
                    return "phoenix";
                case 239:
                    return "red_robin";
                case 240:
                    return "blue_bird";
                case 242:
                    return "killer_phoenix";
                case 245:
                    return "goldfish";
                case 246:
                    return "sea_sponge";
                case 247:
                    return "shark";
                case 248:
                    return "salmon";
                case 249:
                    return "betta_fish";
                case 250:
                    return "lava_shark";
                case 251:
                    return "snake";
                case 252:
                    return "snake_tail";
                case 254:
                    return "melting_glass";
            }

            return "unknown";
        }

        // Token: 0x06000610 RID: 1552 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
        public static byte Parse(string type)
        {
            return Byte(type);
        }

        // Token: 0x06000611 RID: 1553 RVA: 0x0001E8C8 File Offset: 0x0001CAC8
        public static byte Byte(string type)
        {
            string key;
            switch (key = type.ToLower())
            {
                case "air":
                    return 0;
                case "rock":
                case "stone":
                    return 1;
                case "grass":
                    return 2;
                case "ground":
                case "dirt":
                    return 3;
                case "cobble_stone":
                case "cobblestone":
                    return 4;
                case "wood":
                    return 5;
                case "sapling":
                case "plant":
                    return 6;
                case "bedrock":
                case "solid":
                case "admintite":
                case "blackrock":
                case "adminium":
                    return 7;
                case "activewater":
                case "active_water":
                    return 8;
                case "water":
                    return 9;
                case "activelava":
                case "active_lava":
                    return 10;
                case "lava":
                    return 11;
                case "sand":
                    return 12;
                case "gravel":
                    return 13;
                case "gold_ore":
                    return 14;
                case "iron_ore":
                    return 15;
                case "coal":
                    return 16;
                case "trunk":
                case "tree":
                    return 17;
                case "leaves":
                    return 18;
                case "sponge":
                    return 19;
                case "glass":
                    return 20;
                case "red":
                    return 21;
                case "orange":
                    return 22;
                case "yellow":
                    return 23;
                case "greenyellow":
                    return 24;
                case "green":
                    return 25;
                case "springgreen":
                    return 26;
                case "cyan":
                    return 27;
                case "blue":
                    return 28;
                case "blueviolet":
                    return 29;
                case "indigo":
                    return 30;
                case "purple":
                    return 31;
                case "magenta":
                    return 32;
                case "pink":
                    return 33;
                case "black":
                    return 34;
                case "gray":
                    return 35;
                case "white":
                    return 36;
                case "yellow_flower":
                    return 37;
                case "red_flower":
                    return 38;
                case "brown_shroom":
                    return 39;
                case "red_shroom":
                    return 40;
                case "gold":
                    return 41;
                case "iron":
                    return 42;
                case "double_stair":
                    return 43;
                case "stair":
                    return 44;
                case "brick":
                    return 45;
                case "tnt":
                    return 46;
                case "bookcase":
                    return 47;
                case "mossy_cobblestone":
                    return 48;
                case "obsidian":
                    return 49;
                case "cobble_slab":
                    return 50;
                case "rope":
                    return 51;
                case "sandstone":
                    return 52;
                case "snow":
                    return 53;
                case "fire":
                    return 54;
                case "light_pink":
                    return 55;
                case "forest_green":
                    return 56;
                case "brown":
                    return 57;
                case "navy":
                    return 58;
                case "turquoise":
                    return 59;
                case "ice":
                    return 60;
                case "blue_gel":
                    return 66;
                case "red_gel":
                    return 67;
                case "portal_blue":
                    return 68;
                case "portal_orange":
                    return 69;
                case "ceramic_tile":
                    return 61;
                case "lava_obsidian":
                    return 62;
                case "pillar":
                    return 63;
                case "crate":
                    return 64;
                case "stone_brick":
                    return 65;
                case "psponge":
                    return 78;
                case "op_glass":
                    return 100;
                case "opsidian":
                    return 101;
                case "op_lava":
                    return 102;
                case "op_stone":
                    return 103;
                case "op_cobblestone":
                    return 104;
                case "op_air":
                    return 105;
                case "op_water":
                    return 106;
                case "wood_float":
                    return 110;
                case "lava_fast":
                    return 112;
                case "door_tree":
                    return 111;
                case "door":
                    return 220;
                case "door_obsidian":
                case "door2":
                    return 113;
                case "door_adminium":
                case "door_bedrock":
                case "door_solid":
                    return 72;
                case "door_glass":
                case "door3":
                    return 114;
                case "door_stone":
                case "door4":
                    return 115;
                case "door_leaves":
                case "door5":
                    return 116;
                case "door_sand":
                case "door6":
                    return 117;
                case "door_wood":
                case "door7":
                    return 118;
                case "door_green":
                case "door8":
                    return 119;
                case "door_tnt":
                case "door9":
                    return 120;
                case "door_stair":
                case "door10":
                    return 121;
                case "door11":
                case "door_iron":
                    return 220;
                case "door12":
                case "door_dirt":
                    return 221;
                case "door13":
                case "door_grass":
                    return 222;
                case "door14":
                case "door_blue":
                    return 223;
                case "door15":
                case "door_book":
                    return 224;
                case "tdoor_tree":
                case "tdoor":
                    return 122;
                case "tdoor_obsidian":
                case "tdoor2":
                    return 123;
                case "tdoor_glass":
                case "tdoor3":
                    return 124;
                case "tdoor_stone":
                case "tdoor4":
                    return 125;
                case "tdoor_leaves":
                case "tdoor5":
                    return 126;
                case "tdoor_sand":
                case "tdoor6":
                    return 127;
                case "tdoor_wood":
                case "tdoor7":
                    return 128;
                case "tdoor_green":
                case "tdoor8":
                    return 129;
                case "tdoor_tnt":
                case "tdoor9":
                    return 135;
                case "tdoor_stair":
                case "tdoor10":
                    return 136;
                case "tair_switch":
                case "tdoor11":
                    return 137;
                case "tdoor_water":
                case "tdoor12":
                    return 138;
                case "tdoor_lava":
                case "tdoor13":
                    return 139;
                case "odoor_tree":
                case "odoor":
                    return 148;
                case "odoor_obsidian":
                case "odoor2":
                    return 149;
                case "odoor_glass":
                case "odoor3":
                    return 150;
                case "odoor_stone":
                case "odoor4":
                    return 151;
                case "odoor_leaves":
                case "odoor5":
                    return 152;
                case "odoor_sand":
                case "odoor6":
                    return 153;
                case "odoor_wood":
                case "odoor7":
                    return 154;
                case "odoor_green":
                case "odoor8":
                    return 155;
                case "odoor_tnt":
                case "odoor9":
                    return 156;
                case "odoor_stair":
                case "odoor10":
                    return 157;
                case "odoor_lava":
                case "odoor11":
                    return 158;
                case "odoor_water":
                case "odoor12":
                    return 159;
                case "odoor_red":
                    return 177;
                case "white_message":
                    return 130;
                case "black_message":
                    return 131;
                case "air_message":
                    return 132;
                case "water_message":
                    return 133;
                case "lava_message":
                    return 134;
                case "waterfall":
                    return 140;
                case "lavafall":
                    return 141;
                case "water_faucet":
                    return 143;
                case "lava_faucet":
                    return 144;
                case "finite_water":
                    return 145;
                case "finite_lava":
                    return 146;
                case "finite_faucet":
                    return 147;
                case "air_portal":
                    return 160;
                case "water_portal":
                    return 161;
                case "lava_portal":
                    return 162;
                case "air_door":
                    return 164;
                case "air_switch":
                    return 165;
                case "door_water":
                case "water_door":
                    return 166;
                case "door_lava":
                case "lava_door":
                    return 167;
                case "blue_portal":
                    return 175;
                case "orange_portal":
                    return 176;
                case "small_tnt":
                    return 182;
                case "big_tnt":
                    return 183;
                case "tnt_explosion":
                    return 184;
                case "lava_fire":
                    return 185;
                case "rocketstart":
                    return 187;
                case "rockethead":
                    return 188;
                case "firework":
                    return 189;
                case "sb":
                case "smog_bomb":
                    return 77;
                case "hot_lava":
                    return 190;
                case "cold_water":
                    return 191;
                case "nerve_gas":
                    return 192;
                case "acw":
                case "active_cold_water":
                    return 193;
                case "lta":
                case "lava_type_a":
                    return 80;
                case "ltb":
                case "lava_type_b":
                    return 81;
                case "ltc":
                case "lava_type_c":
                    return 82;
                case "ltd":
                case "lava_type_d":
                    return 83;
                case "ahl":
                case "active_hot_lava":
                    return 194;
                case "lavaup":
                    return 98;
                case "waterup":
                    return 74;
                case "meteor":
                    return 99;
                case "ztnt":
                case "weak_tnt":
                    return 75;
                case "db":
                case "dirt_bomb":
                    return 96;
                case "treasure":
                    return 97;
                case "magma":
                    return 195;
                case "geyser":
                    return 196;
                case "air_flood":
                    return 200;
                case "air_flood_layer":
                    return 202;
                case "air_flood_down":
                    return 203;
                case "air_flood_up":
                    return 204;
                case "door_adminium_air":
                    return 73;
                case "door_air":
                    return 201;
                case "door2_air":
                    return 205;
                case "door3_air":
                    return 206;
                case "door4_air":
                    return 207;
                case "door5_air":
                    return 208;
                case "door6_air":
                    return 209;
                case "door7_air":
                    return 210;
                case "door8_air":
                    return 211;
                case "door9_air":
                    return 212;
                case "door10_air":
                    return 213;
                case "door11_air":
                    return 214;
                case "door12_air":
                    return 215;
                case "door13_air":
                    return 216;
                case "door14_air":
                    return 217;
                case "door_iron_air":
                    return 225;
                case "door_dirt_air":
                    return 226;
                case "door_grass_air":
                    return 227;
                case "door_blue_air":
                    return 228;
                case "door_book_air":
                    return 229;
                case "train":
                    return 230;
                case "still_sand":
                    return 71;
                case "snake":
                    return 251;
                case "snake_tail":
                    return 252;
                case "creeper":
                    return 231;
                case "zombie":
                    return 232;
                case "zombie_head":
                    return 233;
                case "blue_bird":
                    return 240;
                case "red_robin":
                    return 239;
                case "dove":
                    return 235;
                case "pidgeon":
                    return 236;
                case "duck":
                    return 237;
                case "phoenix":
                    return 238;
                case "killer_phoenix":
                    return 242;
                case "betta_fish":
                    return 249;
                case "goldfish":
                    return 245;
                case "salmon":
                    return 248;
                case "shark":
                    return 247;
                case "sea_sponge":
                    return 246;
                case "lava_shark":
                    return 250;
            }

            return byte.MaxValue;
        }

        // Token: 0x06000612 RID: 1554 RVA: 0x000200D8 File Offset: 0x0001E2D8
        private static void FillConversion()
        {
            for (var i = 0; i < 70; i++) conversion[i] = (byte) i;
            for (var j = 70; j < 256; j++) conversion[j] = 22;
            AddConversion(79, 16);
            AddConversion(78, 19);
            AddConversion(254, 20);
            AddConversion(70, 39);
            AddConversion(100, 20);
            AddConversion(101, 49);
            AddConversion(102, 11);
            AddConversion(103, 1);
            AddConversion(104, 4);
            AddConversion(105, 0);
            AddConversion(106, 9);
            AddConversion(110, 5);
            AddConversion(112, 10);
            AddConversion(72, 7);
            AddConversion(111, 17);
            AddConversion(113, 49);
            AddConversion(114, 20);
            AddConversion(115, 1);
            AddConversion(116, 18);
            AddConversion(117, 12);
            AddConversion(118, 5);
            AddConversion(119, 25);
            AddConversion(120, 46);
            AddConversion(121, 44);
            AddConversion(220, 42);
            AddConversion(221, 3);
            AddConversion(222, 2);
            AddConversion(223, 29);
            AddConversion(224, 47);
            AddConversion(122, 17);
            AddConversion(123, 49);
            AddConversion(124, 20);
            AddConversion(125, 1);
            AddConversion(126, 18);
            AddConversion(127, 12);
            AddConversion(128, 5);
            AddConversion(129, 25);
            AddConversion(135, 46);
            AddConversion(136, 44);
            AddConversion(137, 0);
            AddConversion(138, 9);
            AddConversion(139, 11);
            AddConversion(148, 17);
            AddConversion(149, 49);
            AddConversion(150, 20);
            AddConversion(151, 1);
            AddConversion(152, 18);
            AddConversion(153, 12);
            AddConversion(154, 5);
            AddConversion(155, 25);
            AddConversion(156, 46);
            AddConversion(157, 44);
            AddConversion(158, 11);
            AddConversion(159, 9);
            AddConversion(130, 36);
            AddConversion(131, 34);
            AddConversion(132, 0);
            AddConversion(133, 9);
            AddConversion(134, 11);
            AddConversion(140, 8);
            AddConversion(141, 10);
            AddConversion(143, 27);
            AddConversion(144, 22);
            AddConversion(140, 8);
            AddConversion(141, 10);
            AddConversion(143, 27);
            AddConversion(144, 22);
            AddConversion(145, 8);
            AddConversion(146, 10);
            AddConversion(147, 28);
            AddConversion(160, 0);
            AddConversion(161, 9);
            AddConversion(162, 11);
            AddConversion(164, 0);
            AddConversion(165, 0);
            AddConversion(166, 9);
            AddConversion(167, 11);
            AddConversion(175, 28);
            AddConversion(176, 22);
            AddConversion(182, 46);
            AddConversion(183, 46);
            AddConversion(184, 10);
            AddConversion(185, 10);
            AddConversion(187, 20);
            AddConversion(188, 41);
            AddConversion(189, 42);
            AddConversion(191, 9);
            AddConversion(190, 11);
            AddConversion(192, 0);
            AddConversion(193, 8);
            AddConversion(80, 10);
            AddConversion(81, 10);
            AddConversion(82, 10);
            AddConversion(83, 10);
            AddConversion(194, 10);
            AddConversion(99, 7);
            AddConversion(195, 10);
            AddConversion(196, 8);
            AddConversion(97, 14);
            AddConversion(98, 10);
            AddConversion(74, 8);
            AddConversion(96, 46);
            AddConversion(75, 46);
            AddConversion(77, 46);
            AddConversion(76, 36);
            AddConversion(71, 12);
            AddConversion(200, 0);
            AddConversion(201, 0);
            AddConversion(202, 0);
            AddConversion(203, 0);
            AddConversion(204, 0);
            AddConversion(205, 0);
            AddConversion(73, 0);
            AddConversion(206, 0);
            AddConversion(207, 0);
            AddConversion(208, 0);
            AddConversion(209, 0);
            AddConversion(210, 0);
            AddConversion(213, 0);
            AddConversion(214, 0);
            AddConversion(215, 0);
            AddConversion(216, 0);
            AddConversion(217, 0);
            AddConversion(225, 0);
            AddConversion(226, 0);
            AddConversion(227, 0);
            AddConversion(228, 0);
            AddConversion(229, 0);
            AddConversion(212, 10);
            AddConversion(211, 21);
            AddConversion(168, 0);
            AddConversion(169, 0);
            AddConversion(170, 0);
            AddConversion(171, 0);
            AddConversion(172, 0);
            AddConversion(173, 0);
            AddConversion(174, 0);
            AddConversion(179, 0);
            AddConversion(180, 0);
            AddConversion(181, 0);
            AddConversion(177, 21);
            AddConversion(178, 11);
            AddConversion(230, 27);
            AddConversion(251, 34);
            AddConversion(252, 16);
            AddConversion(231, 46);
            AddConversion(232, 48);
            AddConversion(233, 24);
            AddConversion(235, 36);
            AddConversion(236, 34);
            AddConversion(238, 10);
            AddConversion(239, 21);
            AddConversion(237, 8);
            AddConversion(240, 29);
            AddConversion(242, 10);
            AddConversion(249, 29);
            AddConversion(245, 41);
            AddConversion(248, 21);
            AddConversion(247, 35);
            AddConversion(246, 19);
            AddConversion(250, 49);
        }

        // Token: 0x06000613 RID: 1555 RVA: 0x000207E8 File Offset: 0x0001E9E8
        private static void AddConversion(byte blockA, byte blockB)
        {
            conversion[blockA] = blockB;
        }

        // Token: 0x06000614 RID: 1556 RVA: 0x000207F4 File Offset: 0x0001E9F4
        public static byte Convert(byte b)
        {
            return conversion[b];
        }

        // Token: 0x06000615 RID: 1557 RVA: 0x00020800 File Offset: 0x0001EA00
        public static byte SaveConvert(byte b)
        {
            if (b != 73)
            {
                switch (b)
                {
                    case 168:
                    case 169:
                    case 170:
                    case 171:
                    case 172:
                    case 173:
                    case 174:
                    case 177:
                    case 178:
                    case 179:
                    case 180:
                    case 181:
                        return odoor(b);
                    case 200:
                    case 202:
                    case 203:
                    case 204:
                        return 0;
                    case 201:
                        return 111;
                    case 205:
                        return 113;
                    case 206:
                        return 114;
                    case 207:
                        return 115;
                    case 208:
                        return 116;
                    case 209:
                        return 117;
                    case 210:
                        return 118;
                    case 211:
                        return 119;
                    case 212:
                        return 120;
                    case 213:
                        return 121;
                    case 214:
                        return 165;
                    case 215:
                        return 166;
                    case 216:
                        return 167;
                    case 217:
                        return 164;
                    case 225:
                        return 220;
                    case 226:
                        return 221;
                    case 227:
                        return 222;
                    case 228:
                        return 223;
                    case 229:
                        return 224;
                }

                return b;
            }

            return 72;
        }

        // Token: 0x06000616 RID: 1558 RVA: 0x00020980 File Offset: 0x0001EB80
        public static byte DoorAirs(byte b)
        {
            if (b <= 121)
            {
                if (b == 72) return 73;
                switch (b)
                {
                    case 111:
                        return 201;
                    case 113:
                        return 205;
                    case 114:
                        return 206;
                    case 115:
                        return 207;
                    case 116:
                        return 208;
                    case 117:
                        return 209;
                    case 118:
                        return 210;
                    case 119:
                        return 211;
                    case 120:
                        return 212;
                    case 121:
                        return 213;
                }
            }
            else
            {
                switch (b)
                {
                    case 164:
                        return 217;
                    case 165:
                        return 214;
                    case 166:
                        return 215;
                    case 167:
                        return 216;
                    default:
                        switch (b)
                        {
                            case 220:
                                return 225;
                            case 221:
                                return 226;
                            case 222:
                                return 227;
                            case 223:
                                return 228;
                            case 224:
                                return 229;
                        }

                        break;
                }
            }

            return 0;
        }

        // Token: 0x06000617 RID: 1559 RVA: 0x00020A88 File Offset: 0x0001EC88
        public static bool tDoor(byte b)
        {
            switch (b)
            {
                case 122:
                case 123:
                case 124:
                case 125:
                case 126:
                case 127:
                case 128:
                case 129:
                case 135:
                case 136:
                case 137:
                case 138:
                case 139:
                    return true;
            }

            return false;
        }

        // Token: 0x06000618 RID: 1560 RVA: 0x00020AF0 File Offset: 0x0001ECF0
        public static byte odoor(byte b)
        {
            switch (b)
            {
                case 148:
                    return 168;
                case 149:
                    return 169;
                case 150:
                    return 170;
                case 151:
                    return 171;
                case 152:
                    return 172;
                case 153:
                    return 173;
                case 154:
                    return 174;
                case 155:
                    return 177;
                case 156:
                    return 178;
                case 157:
                    return 179;
                case 158:
                    return 180;
                case 159:
                    return 181;
                case 168:
                    return 148;
                case 169:
                    return 149;
                case 170:
                    return 150;
                case 171:
                    return 151;
                case 172:
                    return 152;
                case 173:
                    return 153;
                case 174:
                    return 154;
                case 177:
                    return 155;
                case 178:
                    return 156;
                case 179:
                    return 157;
                case 180:
                    return 158;
                case 181:
                    return 159;
            }

            return byte.MaxValue;
        }

        // Token: 0x06000619 RID: 1561 RVA: 0x00020C30 File Offset: 0x0001EE30
        internal static bool IsWater(byte block)
        {
            return block == 8 || block == 9;
        }

        // Token: 0x020000B2 RID: 178
        public class Blocks
        {
            // Token: 0x04000352 RID: 850
            public List<LevelPermission> allow = new List<LevelPermission>();

            // Token: 0x04000351 RID: 849
            public List<LevelPermission> disallow = new List<LevelPermission>();

            // Token: 0x04000350 RID: 848
            public LevelPermission lowestRank;

            // Token: 0x0400034F RID: 847
            public byte type;
        }
    }
}