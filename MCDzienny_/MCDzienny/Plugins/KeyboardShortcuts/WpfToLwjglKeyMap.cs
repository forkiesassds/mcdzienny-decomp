using System.Windows.Input;

namespace MCDzienny.Plugins.KeyboardShortcuts
{
    // Token: 0x0200004C RID: 76
    public class WpfToLwjglKeyMap
    {
        // Token: 0x060001B7 RID: 439 RVA: 0x00009ABC File Offset: 0x00007CBC
        public static CpeHotKeyInfo ToCpeHotKey(string shortcut)
        {
            byte b = 0;
            var text = shortcut.Replace(" ", "");
            text = text.ToLower();
            if (text.Contains("ctrl+"))
            {
                b |= CpeHotKeyInfo.Ctrl;
                text = text.Replace("ctrl+", "");
            }

            if (text.Contains("shift+"))
            {
                b |= CpeHotKeyInfo.Shift;
                text = text.Replace("shift+", "");
            }

            if (text.Contains("alt+"))
            {
                b |= CpeHotKeyInfo.Alt;
                text = text.Replace("alt+", "");
            }

            var keyConverter = new KeyConverter();
            var key = (Key) keyConverter.ConvertFrom(text);
            var keyCode = FromWpfToLwjgl(key);
            return new CpeHotKeyInfo
            {
                KeyCode = keyCode,
                KeyMod = b
            };
        }

        // Token: 0x060001B8 RID: 440 RVA: 0x00009B94 File Offset: 0x00007D94
        public static int FromWpfToLwjgl(Key key)
        {
            switch (key)
            {
                case Key.None:
                    return 0;
                case Key.Back:
                    return 14;
                case Key.Tab:
                    return 15;
                case Key.Return:
                    return 28;
                case Key.Pause:
                    return 197;
                case Key.Capital:
                    return 58;
                case Key.Escape:
                    return 1;
                case Key.Space:
                    return 57;
                case Key.Prior:
                    return 201;
                case Key.Next:
                    return 209;
                case Key.End:
                    return 207;
                case Key.Home:
                    return 199;
                case Key.Left:
                    return 203;
                case Key.Up:
                    return 200;
                case Key.Right:
                    return 205;
                case Key.Down:
                    return 208;
                case Key.Snapshot:
                    return 183;
                case Key.Insert:
                    return 210;
                case Key.Delete:
                    return 211;
                case Key.D0:
                    return 11;
                case Key.D1:
                    return 2;
                case Key.D2:
                    return 3;
                case Key.D3:
                    return 4;
                case Key.D4:
                    return 5;
                case Key.D5:
                    return 6;
                case Key.D6:
                    return 7;
                case Key.D7:
                    return 8;
                case Key.D8:
                    return 9;
                case Key.D9:
                    return 10;
                case Key.A:
                    return 30;
                case Key.B:
                    return 48;
                case Key.C:
                    return 46;
                case Key.D:
                    return 32;
                case Key.E:
                    return 18;
                case Key.F:
                    return 33;
                case Key.G:
                    return 34;
                case Key.H:
                    return 35;
                case Key.I:
                    return 23;
                case Key.J:
                    return 36;
                case Key.K:
                    return 37;
                case Key.L:
                    return 38;
                case Key.M:
                    return 50;
                case Key.N:
                    return 49;
                case Key.O:
                    return 24;
                case Key.P:
                    return 25;
                case Key.Q:
                    return 16;
                case Key.R:
                    return 19;
                case Key.S:
                    return 31;
                case Key.T:
                    return 20;
                case Key.U:
                    return 22;
                case Key.V:
                    return 47;
                case Key.W:
                    return 17;
                case Key.X:
                    return 45;
                case Key.Y:
                    return 21;
                case Key.Z:
                    return 44;
                case Key.LWin:
                    return 219;
                case Key.RWin:
                    return 220;
                case Key.Apps:
                    return 221;
                case Key.Sleep:
                    return 223;
                case Key.NumPad0:
                    return 82;
                case Key.NumPad1:
                    return 79;
                case Key.NumPad2:
                    return 80;
                case Key.NumPad3:
                    return 81;
                case Key.NumPad4:
                    return 75;
                case Key.NumPad5:
                    return 76;
                case Key.NumPad6:
                    return 77;
                case Key.NumPad7:
                    return 71;
                case Key.NumPad8:
                    return 72;
                case Key.NumPad9:
                    return 73;
                case Key.Multiply:
                    return 55;
                case Key.Add:
                    return 78;
                case Key.Separator:
                    return 0;
                case Key.Subtract:
                    return 74;
                case Key.Decimal:
                    return 83;
                case Key.Divide:
                    return 181;
                case Key.F1:
                    return 59;
                case Key.F2:
                    return 60;
                case Key.F3:
                    return 61;
                case Key.F4:
                    return 62;
                case Key.F5:
                    return 63;
                case Key.F6:
                    return 64;
                case Key.F7:
                    return 65;
                case Key.F8:
                    return 66;
                case Key.F9:
                    return 67;
                case Key.F10:
                    return 68;
                case Key.F11:
                    return 87;
                case Key.F12:
                    return 88;
                case Key.F13:
                    return 100;
                case Key.F14:
                    return 101;
                case Key.F15:
                    return 102;
                case Key.NumLock:
                    return 69;
                case Key.Scroll:
                    return 70;
                case Key.LeftShift:
                    return 42;
                case Key.RightShift:
                    return 54;
                case Key.LeftCtrl:
                    return 29;
                case Key.RightCtrl:
                    return 157;
                case Key.LeftAlt:
                    return 56;
                case Key.RightAlt:
                    return 184;
                case Key.Oem1:
                    return 39;
                case Key.OemPlus:
                    return 13;
                case Key.OemComma:
                    return 51;
                case Key.OemMinus:
                    return 12;
                case Key.OemPeriod:
                    return 52;
                case Key.Oem2:
                    return 53;
                case Key.Oem3:
                    return 41;
                case Key.Oem4:
                    return 26;
                case Key.Oem5:
                    return 43;
                case Key.Oem6:
                    return 27;
                case Key.Oem7:
                    return 40;
            }

            return -1;
        }
    }
}