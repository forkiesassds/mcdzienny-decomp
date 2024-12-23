using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading;

namespace MCDzienny
{
    public class CmdImageprint : Command
    {

        string bitmaplocation;

        bool layer;

        byte popType = 1;

        public override string name { get { return "imageprint"; } }

        public override string shortcut { get { return "i"; } }

        public override string type { get { return "build"; } }

        public override bool museumUsable { get { return false; } }

        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override bool ConsoleAccess { get { return false; } }

        public override void Use(Player p, string message)
        {
            if (!Directory.Exists("extra/images/"))
            {
                Directory.CreateDirectory("extra/images/");
            }
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
                string[] array = message.Split(' ');
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == "layer" || array[i] == "l")
                    {
                        layer = true;
                    }
                    else if (array[i] == "1" || array[i] == "2color")
                    {
                        popType = 1;
                    }
                    else if (array[i] == "2" || array[i] == "1color")
                    {
                        popType = 2;
                    }
                    else if (array[i] == "3" || array[i] == "2gray")
                    {
                        popType = 3;
                    }
                    else if (array[i] == "4" || array[i] == "1gray")
                    {
                        popType = 4;
                    }
                    else if (array[i] == "5" || array[i] == "bw")
                    {
                        popType = 5;
                    }
                    else if (array[i] == "6" || array[i] == "gray")
                    {
                        popType = 6;
                    }
                }
                message = array[array.Length - 1];
            }
            if (message.IndexOf('/') == -1 && message.IndexOf('.') != -1)
            {
                try
                {
                    WebClient webClient = new WebClient();
                    Player.SendMessage(p, string.Format("Downloading IMGUR file from: &fhttp://www.imgur.com/{0}", message));
                    webClient.DownloadFile("http://www.imgur.com/" + message, "extra/images/tempImage_" + p.name + ".bmp");
                    webClient.Dispose();
                    Player.SendMessage(p, "Download complete.");
                    bitmaplocation = "tempImage_" + p.name;
                    message = bitmaplocation;
                }
                catch {}
            }
            else if (message.IndexOf('.') != -1)
            {
                try
                {
                    WebClient webClient2 = new WebClient();
                    if (message.Substring(0, 4) != "http")
                    {
                        message = "http://" + message;
                    }
                    Player.SendMessage(p, string.Format("Downloading file from: &f{0}, please wait.", message + Server.DefaultColor));
                    webClient2.DownloadFile(message, "extra/images/tempImage_" + p.name + ".bmp");
                    webClient2.Dispose();
                    Player.SendMessage(p, "Download complete.");
                    bitmaplocation = "tempImage_" + p.name;
                }
                catch {}
            }
            else
            {
                bitmaplocation = message;
            }
            if (!File.Exists("extra/images/" + bitmaplocation + ".bmp"))
            {
                Player.SendMessage(p, "The URL entered was invalid!");
                return;
            }
            CatchPos catchPos = default(CatchPos);
            catchPos.x = 0;
            catchPos.y = 0;
            catchPos.z = 0;
            p.blockchangeObject = catchPos;
            Player.SendMessage(p, "Place two blocks to determine direction.");
            p.ClearBlockchange();
            p.Blockchange += Blockchange1;
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            CatchPos catchPos = (CatchPos)p.blockchangeObject;
            catchPos.x = x;
            catchPos.y = y;
            catchPos.z = z;
            p.blockchangeObject = catchPos;
            p.Blockchange += Blockchange2;
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            //IL_0036: Unknown result type (might be due to invalid IL or missing references)
            //IL_003c: Expected O, but got Unknown
            p.ClearBlockchange();
            byte tile = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, tile);
            Bitmap val = new Bitmap("extra/images/" + bitmaplocation + ".bmp");
            val.RotateFlip((RotateFlipType)6);
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            if (x == cpos.x && z == cpos.z)
            {
                Player.SendMessage(p, "No direction was selected");
                return;
            }
            int num;
            if (Math.Abs(cpos.x - x) > Math.Abs(cpos.z - z))
            {
                num = 0;
                if (x <= cpos.x)
                {
                    num = 1;
                }
            }
            else
            {
                num = 2;
                if (z <= cpos.z)
                {
                    num = 3;
                }
            }
            if (layer)
            {
                if (popType == 1)
                {
                    popType = 2;
                }
                else if (popType == 3)
                {
                    popType = 4;
                }
            }
            List<FindReference.ColorBlock> refCol = FindReference.popRefCol(popType);
            p.SendMessage(string.Concat(num));
            Thread thread = new Thread(Print);
            thread.Start(new PrintObjects(p, refCol, cpos, val, num));
        }

        void Print(object printObjects)
        {
            PrintObjects printObjects2 = (PrintObjects)printObjects;
            Player p = printObjects2.p;
            List<FindReference.ColorBlock> refCol = printObjects2.refCol;
            CatchPos cpos = printObjects2.cpos;
            Bitmap myBitmap = printObjects2.myBitmap;
            int direction = printObjects2.direction;
            try
            {
                p.IsPrinting = true;
                double[] array = new double[refCol.Count];
                FindReference.ColorBlock colorBlock = default(FindReference.ColorBlock);
                for (int i = 0; i < myBitmap.Width; i++)
                {
                    for (int j = 0; j < myBitmap.Height; j++)
                    {
                        if (layer)
                        {
                            colorBlock.y = cpos.y;
                            if (direction <= 1)
                            {
                                if (direction == 0)
                                {
                                    colorBlock.x = (ushort)(cpos.x + i);
                                    colorBlock.z = (ushort)(cpos.z - j);
                                }
                                else
                                {
                                    colorBlock.x = (ushort)(cpos.x - i);
                                    colorBlock.z = (ushort)(cpos.z + j);
                                }
                            }
                            else if (direction == 2)
                            {
                                colorBlock.z = (ushort)(cpos.z + i);
                                colorBlock.x = (ushort)(cpos.x + j);
                            }
                            else
                            {
                                colorBlock.z = (ushort)(cpos.z - i);
                                colorBlock.x = (ushort)(cpos.x - j);
                            }
                        }
                        else
                        {
                            colorBlock.y = (ushort)(cpos.y + j);
                            if (direction <= 1)
                            {
                                if (direction == 0)
                                {
                                    colorBlock.x = (ushort)(cpos.x + i);
                                }
                                else
                                {
                                    colorBlock.x = (ushort)(cpos.x - i);
                                }
                                colorBlock.z = cpos.z;
                            }
                            else
                            {
                                if (direction == 2)
                                {
                                    colorBlock.z = (ushort)(cpos.z + i);
                                }
                                else
                                {
                                    colorBlock.z = (ushort)(cpos.z - i);
                                }
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
                            {
                                colorBlock.type = 49;
                            }
                            else if ((colorBlock.r + colorBlock.g + colorBlock.b) / 3 >= 64 && (colorBlock.r + colorBlock.g + colorBlock.b) / 3 < 128)
                            {
                                colorBlock.type = 34;
                            }
                            else if ((colorBlock.r + colorBlock.g + colorBlock.b) / 3 >= 128 && (colorBlock.r + colorBlock.g + colorBlock.b) / 3 < 192)
                            {
                                colorBlock.type = 35;
                            }
                            else
                            {
                                colorBlock.type = 36;
                            }
                        }
                        else
                        {
                            for (int k = 0; k < array.Length; k++)
                            {
                                array[k] = Math.Sqrt(Math.Pow(colorBlock.r - refCol[k].r, 2.0) + Math.Pow(colorBlock.b - refCol[k].b, 2.0) +
                                                     Math.Pow(colorBlock.g - refCol[k].g, 2.0));
                            }
                            int num = 0;
                            double num2 = array[0];
                            for (int l = 1; l < array.Length; l++)
                            {
                                if (array[l] < num2)
                                {
                                    num2 = array[l];
                                    num = l;
                                }
                            }
                            colorBlock.type = refCol[num].type;
                            if (popType == 1)
                            {
                                if (num <= 20)
                                {
                                    switch (direction)
                                    {
                                        case 0:
                                            colorBlock.z++;
                                            break;
                                        case 2:
                                            colorBlock.x--;
                                            break;
                                        case 1:
                                            colorBlock.z--;
                                            break;
                                        case 3:
                                            colorBlock.x++;
                                            break;
                                    }
                                }
                            }
                            else if (popType == 3 && num <= 3)
                            {
                                switch (direction)
                                {
                                    case 0:
                                        colorBlock.z++;
                                        break;
                                    case 2:
                                        colorBlock.x--;
                                        break;
                                    case 1:
                                        colorBlock.z--;
                                        break;
                                    case 3:
                                        colorBlock.x++;
                                        break;
                                }
                            }
                        }
                        if (colorBlock.a < 20)
                        {
                            colorBlock.type = 0;
                        }
                        FindReference.placeBlock(p.level, p, colorBlock.x, colorBlock.y, colorBlock.z, colorBlock.type);
                    }
                }
                if (bitmaplocation == "tempImage_" + p.name)
                {
                    File.Delete("extra/images/tempImage_" + p.name + ".bmp");
                }
                Player.SendMessage(p, string.Format("Finished printing image using {0}", popType == 1 ? "2-layer color" :
                                        popType == 2 ? "1-layer color" :
                                        popType == 3 ? "2-layer grayscale" :
                                        popType == 4 ? "1-layer grayscale" :
                                        popType == 5 ? "Black and White" :
                                        popType == 6 ? "Mathematical grayscale" : "Something unknown"));
            }
            catch (Exception)
            {
                if (p != null)
                {
                    p.IsPrinting = false;
                }
                throw;
            }
            p.IsPrinting = false;
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/imageprint <switch> <localfile> - Print local file in extra/images/ folder.  Must be type .bmp, type filename without extension.");
            Player.SendMessage(
                p, "/imageprint <switch> <imgurfile.extension> - Print IMGUR stored file.  Example: /i piCCm.gif will print www.imgur.com/piCCm.gif. Case-sensitive");
            Player.SendMessage(p, "/imageprint <switch> <webfile> - Print web file in format domain.com/folder/image.jpg. Does not need http:// or www.");
            Player.SendMessage(
                p,
                string.Format(
                    "Available switches: (&f1{0}) 2-Layer Color image, (&f2{0}) 1-Layer Color Image, (&f3{0}) 2-Layer Grayscale, (&f4{0}) 1-Layer Grayscale, (%f5{0}) Black and White, (&f6{0}) Mathematical Grayscale",
                    Server.DefaultColor));
            Player.SendMessage(p, "Local filetypes: .bmp.   Remote Filetypes: .gif .png .jpg .bmp.  PNG and GIF may use transparency");
            Player.SendMessage(p, string.Format("Use switch (&flayer{0}) or (&fl{0}) to print horizontally.", Server.DefaultColor));
        }

        class PrintObjects
        {

            public readonly CatchPos cpos;

            public readonly int direction;

            public readonly Bitmap myBitmap;
            public readonly Player p;

            public readonly List<FindReference.ColorBlock> refCol;

            PrintObjects() {}

            public PrintObjects(Player p, List<FindReference.ColorBlock> refCol, CatchPos cpos, Bitmap myBitmap, int direction)
            {
                this.p = p;
                this.refCol = refCol;
                this.cpos = cpos;
                this.myBitmap = myBitmap;
                this.direction = direction;
            }
        }

        public struct CatchPos
        {
            public ushort x;

            public ushort y;

            public ushort z;
        }
    }
}