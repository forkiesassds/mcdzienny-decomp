using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;

namespace MCDzienny
{
    // Token: 0x02000269 RID: 617
    public class CmdImageprint : Command
    {
        // Token: 0x04000901 RID: 2305
        private string bitmaplocation;

        // Token: 0x04000902 RID: 2306
        private bool layer;

        // Token: 0x04000903 RID: 2307
        private byte popType = 1;

        // Token: 0x17000677 RID: 1655
        // (get) Token: 0x060011BE RID: 4542 RVA: 0x00061DB4 File Offset: 0x0005FFB4
        public override string name
        {
            get { return "imageprint"; }
        }

        // Token: 0x17000678 RID: 1656
        // (get) Token: 0x060011BF RID: 4543 RVA: 0x00061DBC File Offset: 0x0005FFBC
        public override string shortcut
        {
            get { return "i"; }
        }

        // Token: 0x17000679 RID: 1657
        // (get) Token: 0x060011C0 RID: 4544 RVA: 0x00061DC4 File Offset: 0x0005FFC4
        public override string type
        {
            get { return "build"; }
        }

        // Token: 0x1700067A RID: 1658
        // (get) Token: 0x060011C1 RID: 4545 RVA: 0x00061DCC File Offset: 0x0005FFCC
        public override bool museumUsable
        {
            get { return false; }
        }

        // Token: 0x1700067B RID: 1659
        // (get) Token: 0x060011C2 RID: 4546 RVA: 0x00061DD0 File Offset: 0x0005FFD0
        public override LevelPermission defaultRank
        {
            get { return LevelPermission.Admin; }
        }

        // Token: 0x1700067C RID: 1660
        // (get) Token: 0x060011C3 RID: 4547 RVA: 0x00061DD4 File Offset: 0x0005FFD4
        public override bool ConsoleAccess
        {
            get { return false; }
        }

        // Token: 0x060011C5 RID: 4549 RVA: 0x00061DE8 File Offset: 0x0005FFE8
        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/images/")) Directory.CreateDirectory("extra/images/");
            layer = false;
            popType = 1;
            if (message == "")
            {
                Help(p);
                return;
            }

            if (p.IsPrinting)
            {
                Player.SendMessage(p, "Wait till the previous print is finished.");
                return;
            }

            if (message.IndexOf(' ') != -1)
            {
                var array = message.Split(' ');
                for (var i = 0; i < array.Length; i++)
                    if (array[i] == "layer" || array[i] == "l")
                        layer = true;
                    else if (array[i] == "1" || array[i] == "2color")
                        popType = 1;
                    else if (array[i] == "2" || array[i] == "1color")
                        popType = 2;
                    else if (array[i] == "3" || array[i] == "2gray")
                        popType = 3;
                    else if (array[i] == "4" || array[i] == "1gray")
                        popType = 4;
                    else if (array[i] == "5" || array[i] == "bw")
                        popType = 5;
                    else if (array[i] == "6" || array[i] == "gray") popType = 6;
                message = array[array.Length - 1];
            }

            if (message.IndexOf('/') == -1 && message.IndexOf('.') != -1)
                try
                {
                    var webClient = new WebClient();
                    Player.SendMessage(p,
                        string.Format("Downloading IMGUR file from: &fhttp://www.imgur.com/{0}", message));
                    webClient.DownloadFile("http://www.imgur.com/" + message,
                        "extra/images/tempImage_" + p.name + ".bmp");
                    webClient.Dispose();
                    Player.SendMessage(p, "Download complete.");
                    bitmaplocation = "tempImage_" + p.name;
                    message = bitmaplocation;
                    goto IL_2D7;
                }
                catch
                {
                    goto IL_2D7;
                }

            if (message.IndexOf('.') != -1)
                try
                {
                    var webClient2 = new WebClient();
                    if (message.Substring(0, 4) != "http") message = "http://" + message;
                    Player.SendMessage(p,
                        string.Format("Downloading file from: &f{0}, please wait.", message + Server.DefaultColor));
                    webClient2.DownloadFile(message, "extra/images/tempImage_" + p.name + ".bmp");
                    webClient2.Dispose();
                    Player.SendMessage(p, "Download complete.");
                    bitmaplocation = "tempImage_" + p.name;
                    goto IL_2D7;
                }
                catch
                {
                    goto IL_2D7;
                }

            bitmaplocation = message;
            IL_2D7:
            if (!File.Exists("extra/images/" + bitmaplocation + ".bmp"))
            {
                Player.SendMessage(p, "The URL entered was invalid!");
                return;
            }

            CatchPos catchPos;
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine direction.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        // Token: 0x060011C6 RID: 4550 RVA: 0x00062158 File Offset: 0x00060358
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var catchPos = (CatchPos) p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        // Token: 0x060011C7 RID: 4551 RVA: 0x000621CC File Offset: 0x000603CC
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            var tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            var bitmap = new Bitmap("extra/images/" + bitmaplocation + ".bmp");
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);
            var cpos = (CatchPos) p.blockchangeObject;
            if (x == cpos.x && z == cpos.z)
            {
                Player.SendMessage(p, "No direction was selected");
                return;
            }

            int num;
            if (Math.Abs(cpos.x - x) > Math.Abs(cpos.z - z))
            {
                num = 0;
                if (x <= cpos.x) num = 1;
            }
            else
            {
                num = 2;
                if (z <= cpos.z) num = 3;
            }

            if (layer)
            {
                if (popType == 1)
                    popType = 2;
                else if (popType == 3) popType = 4;
            }

            var refCol = FindReference.popRefCol(popType);
            p.SendMessage(string.Concat(num));
            var thread = new Thread(Print);
            thread.Start(new PrintObjects(p, refCol, cpos, bitmap, num));
        }

        // Token: 0x060011C8 RID: 4552 RVA: 0x000622F4 File Offset: 0x000604F4
        private void Print(object printObjects)
        {
            var printObjects2 = (PrintObjects) printObjects;
            var p = printObjects2.p;
            var refCol = printObjects2.refCol;
            var cpos = printObjects2.cpos;
            var myBitmap = printObjects2.myBitmap;
            var direction = printObjects2.direction;
            try
            {
                p.IsPrinting = true;
                var array = new double[refCol.Count];
                for (var i = 0; i < myBitmap.Width; i++)
                for (var j = 0; j < myBitmap.Height; j++)
                {
                    FindReference.ColorBlock colorBlock;
                    if (layer)
                    {
                        colorBlock.y = cpos.y;
                        if (direction <= 1)
                        {
                            if (direction == 0)
                            {
                                colorBlock.x = (ushort) (cpos.x + i);
                                colorBlock.z = (ushort) (cpos.z - j);
                            }
                            else
                            {
                                colorBlock.x = (ushort) (cpos.x - i);
                                colorBlock.z = (ushort) (cpos.z + j);
                            }
                        }
                        else if (direction == 2)
                        {
                            colorBlock.z = (ushort) (cpos.z + i);
                            colorBlock.x = (ushort) (cpos.x + j);
                        }
                        else
                        {
                            colorBlock.z = (ushort) (cpos.z - i);
                            colorBlock.x = (ushort) (cpos.x - j);
                        }
                    }
                    else
                    {
                        colorBlock.y = (ushort) (cpos.y + j);
                        if (direction <= 1)
                        {
                            if (direction == 0)
                                colorBlock.x = (ushort) (cpos.x + i);
                            else
                                colorBlock.x = (ushort) (cpos.x - i);
                            colorBlock.z = cpos.z;
                        }
                        else
                        {
                            if (direction == 2)
                                colorBlock.z = (ushort) (cpos.z + i);
                            else
                                colorBlock.z = (ushort) (cpos.z - i);
                            colorBlock.x = cpos.x;
                        }
                    }

                    colorBlock.r = myBitmap.GetPixel(i, j).R;
                    colorBlock.g = myBitmap.GetPixel(i, j).G;
                    colorBlock.b = myBitmap.GetPixel(i, j).B;
                    colorBlock.a = myBitmap.GetPixel(i, j).A;
                    if (popType == 6)
                    {
                        if ((colorBlock.r + colorBlock.g + colorBlock.b) / 3 < 64)
                            colorBlock.type = 49;
                        else if ((colorBlock.r + colorBlock.g + colorBlock.b) / 3 >= 64 &&
                                 (colorBlock.r + colorBlock.g + colorBlock.b) / 3 < 128)
                            colorBlock.type = 34;
                        else if ((colorBlock.r + colorBlock.g + colorBlock.b) / 3 >= 128 &&
                                 (colorBlock.r + colorBlock.g + colorBlock.b) / 3 < 192)
                            colorBlock.type = 35;
                        else
                            colorBlock.type = 36;
                    }
                    else
                    {
                        for (var k = 0; k < array.Length; k++)
                            array[k] = Math.Sqrt(Math.Pow(colorBlock.r - refCol[k].r, 2.0) +
                                                 Math.Pow(colorBlock.b - refCol[k].b, 2.0) +
                                                 Math.Pow(colorBlock.g - refCol[k].g, 2.0));
                        var num = 0;
                        var num2 = array[0];
                        for (var l = 1; l < array.Length; l++)
                            if (array[l] < num2)
                            {
                                num2 = array[l];
                                num = l;
                            }

                        colorBlock.type = refCol[num].type;
                        if (popType == 1)
                        {
                            if (num <= 20)
                            {
                                if (direction == 0)
                                    colorBlock.z += 1;
                                else if (direction == 2)
                                    colorBlock.x -= 1;
                                else if (direction == 1)
                                    colorBlock.z -= 1;
                                else if (direction == 3) colorBlock.x += 1;
                            }
                        }
                        else if (popType == 3 && num <= 3)
                        {
                            if (direction == 0)
                                colorBlock.z += 1;
                            else if (direction == 2)
                                colorBlock.x -= 1;
                            else if (direction == 1)
                                colorBlock.z -= 1;
                            else if (direction == 3) colorBlock.x += 1;
                        }
                    }

                    if (colorBlock.a < 20) colorBlock.type = 0;
                    FindReference.placeBlock(p.level, p, colorBlock.x, colorBlock.y, colorBlock.z, colorBlock.type);
                }

                if (bitmaplocation == "tempImage_" + p.name) File.Delete("extra/images/tempImage_" + p.name + ".bmp");
                string arg;
                switch (popType)
                {
                    case 1:
                        arg = "2-layer color";
                        break;
                    case 2:
                        arg = "1-layer color";
                        break;
                    case 3:
                        arg = "2-layer grayscale";
                        break;
                    case 4:
                        arg = "1-layer grayscale";
                        break;
                    case 5:
                        arg = "Black and White";
                        break;
                    case 6:
                        arg = "Mathematical grayscale";
                        break;
                    default:
                        arg = "Something unknown";
                        break;
                }

                Player.SendMessage(p, string.Format("Finished printing image using {0}", arg));
            }
            catch (Exception)
            {
                if (p != null) p.IsPrinting = false;
                throw;
            }

            p.IsPrinting = false;
        }

        // Token: 0x060011C9 RID: 4553 RVA: 0x000628F0 File Offset: 0x00060AF0
        public override void Help(Player p)
        {
            Player.SendMessage(p,
                "/imageprint <switch> <localfile> - Print local file in extra/images/ folder.  Must be type .bmp, type filename without extension.");
            Player.SendMessage(p,
                "/imageprint <switch> <imgurfile.extension> - Print IMGUR stored file.  Example: /i piCCm.gif will print www.imgur.com/piCCm.gif. Case-sensitive");
            Player.SendMessage(p,
                "/imageprint <switch> <webfile> - Print web file in format domain.com/folder/image.jpg. Does not need http:// or www.");
            Player.SendMessage(p,
                string.Format(
                    "Available switches: (&f1{0}) 2-Layer Color image, (&f2{0}) 1-Layer Color Image, (&f3{0}) 2-Layer Grayscale, (&f4{0}) 1-Layer Grayscale, (%f5{0}) Black and White, (&f6{0}) Mathematical Grayscale",
                    Server.DefaultColor));
            Player.SendMessage(p,
                "Local filetypes: .bmp.   Remote Filetypes: .gif .png .jpg .bmp.  PNG and GIF may use transparency");
            Player.SendMessage(p,
                string.Format("Use switch (&flayer{0}) or (&fl{0}) to print horizontally.", Server.DefaultColor));
        }

        // Token: 0x0200026A RID: 618
        private class PrintObjects
        {
            // Token: 0x04000906 RID: 2310
            public readonly CatchPos cpos;

            // Token: 0x04000908 RID: 2312
            public readonly int direction;

            // Token: 0x04000907 RID: 2311
            public readonly Bitmap myBitmap;

            // Token: 0x04000904 RID: 2308
            public readonly Player p;

            // Token: 0x04000905 RID: 2309
            public readonly List<FindReference.ColorBlock> refCol;

            // Token: 0x060011CA RID: 4554 RVA: 0x00062954 File Offset: 0x00060B54
            private PrintObjects()
            {
            }

            // Token: 0x060011CB RID: 4555 RVA: 0x0006295C File Offset: 0x00060B5C
            public PrintObjects(Player p, List<FindReference.ColorBlock> refCol, CatchPos cpos, Bitmap myBitmap,
                int direction)
            {
                this.p = p;
                this.refCol = refCol;
                this.cpos = cpos;
                this.myBitmap = myBitmap;
                this.direction = direction;
            }
        }

        // Token: 0x0200026B RID: 619
        public struct CatchPos
        {
            // Token: 0x04000909 RID: 2313
            public ushort x;

            // Token: 0x0400090A RID: 2314
            public ushort y;

            // Token: 0x0400090B RID: 2315
            public ushort z;
        }
    }
}