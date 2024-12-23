using System.Windows.Input;

namespace MCDzienny.Plugins.KeyboardShortcuts
{
    public class WpfToLwjglKeyMap
    {
        public static CpeHotKeyInfo ToCpeHotKey(string shortcut)
        {
            //IL_008f: Unknown result type (might be due to invalid IL or missing references)
            //IL_0095: Expected O, but got Unknown
            //IL_009c: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a1: Unknown result type (might be due to invalid IL or missing references)
            //IL_00a2: Unknown result type (might be due to invalid IL or missing references)
            byte b = 0;
            string text = shortcut.Replace(" ", "");
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
            KeyConverter val = new KeyConverter();
            Key key = (Key)val.ConvertFrom(text);
            int keyCode = FromWpfToLwjgl(key);
            CpeHotKeyInfo cpeHotKeyInfo = new CpeHotKeyInfo();
            cpeHotKeyInfo.KeyCode = keyCode;
            cpeHotKeyInfo.KeyMod = b;
            return cpeHotKeyInfo;
        }

        public static int FromWpfToLwjgl(Key key)
        {
            //IL_0000: Unknown result type (might be due to invalid IL or missing references)
            //IL_0001: Unknown result type (might be due to invalid IL or missing references)
            //IL_0002: Unknown result type (might be due to invalid IL or missing references)
            //IL_026c: Expected I4, but got Unknown
            switch ((int)key)
            {
                case 44:
                    return 30;
                case 85:
                    return 78;
                case 72:
                    return 221;
                case 45:
                    return 48;
                case 2:
                    return 14;
                case 46:
                    return 46;
                case 8:
                    return 58;
                case 47:
                    return 32;
                case 34:
                    return 11;
                case 35:
                    return 2;
                case 36:
                    return 3;
                case 37:
                    return 4;
                case 38:
                    return 5;
                case 39:
                    return 6;
                case 40:
                    return 7;
                case 41:
                    return 8;
                case 42:
                    return 9;
                case 43:
                    return 10;
                case 88:
                    return 83;
                case 32:
                    return 211;
                case 89:
                    return 181;
                case 26:
                    return 208;
                case 48:
                    return 18;
                case 21:
                    return 207;
                case 6:
                    return 28;
                case 13:
                    return 1;
                case 49:
                    return 33;
                case 90:
                    return 59;
                case 99:
                    return 68;
                case 100:
                    return 87;
                case 101:
                    return 88;
                case 102:
                    return 100;
                case 103:
                    return 101;
                case 104:
                    return 102;
                case 91:
                    return 60;
                case 92:
                    return 61;
                case 93:
                    return 62;
                case 94:
                    return 63;
                case 95:
                    return 64;
                case 96:
                    return 65;
                case 97:
                    return 66;
                case 98:
                    return 67;
                case 50:
                    return 34;
                case 51:
                    return 35;
                case 22:
                    return 199;
                case 52:
                    return 23;
                case 31:
                    return 210;
                case 53:
                    return 36;
                case 54:
                    return 37;
                case 55:
                    return 38;
                case 23:
                    return 203;
                case 120:
                    return 56;
                case 118:
                    return 29;
                case 116:
                    return 42;
                case 70:
                    return 219;
                case 56:
                    return 50;
                case 84:
                    return 55;
                case 57:
                    return 49;
                case 0:
                    return 0;
                case 114:
                    return 69;
                case 74:
                    return 82;
                case 75:
                    return 79;
                case 76:
                    return 80;
                case 77:
                    return 81;
                case 78:
                    return 75;
                case 79:
                    return 76;
                case 80:
                    return 77;
                case 81:
                    return 71;
                case 82:
                    return 72;
                case 83:
                    return 73;
                case 58:
                    return 24;
                case 151:
                    return 27;
                case 142:
                    return 51;
                case 143:
                    return 12;
                case 149:
                    return 26;
                case 144:
                    return 52;
                case 150:
                    return 43;
                case 141:
                    return 13;
                case 145:
                    return 53;
                case 152:
                    return 40;
                case 140:
                    return 39;
                case 146:
                    return 41;
                case 59:
                    return 25;
                case 20:
                    return 209;
                case 19:
                    return 201;
                case 7:
                    return 197;
                case 30:
                    return 183;
                case 60:
                    return 16;
                case 61:
                    return 19;
                case 25:
                    return 205;
                case 121:
                    return 184;
                case 119:
                    return 157;
                case 117:
                    return 54;
                case 71:
                    return 220;
                case 62:
                    return 31;
                case 115:
                    return 70;
                case 86:
                    return 0;
                case 73:
                    return 223;
                case 18:
                    return 57;
                case 87:
                    return 74;
                case 63:
                    return 20;
                case 3:
                    return 15;
                case 64:
                    return 22;
                case 24:
                    return 200;
                case 65:
                    return 47;
                case 66:
                    return 17;
                case 67:
                    return 45;
                case 68:
                    return 21;
                case 69:
                    return 44;
                default:
                    return -1;
            }
        }
    }
}