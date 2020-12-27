using System;

namespace MCDzienny
{
    // Token: 0x0200039A RID: 922
    public class VersionNumber : IComparable, IEquatable<VersionNumber>
    {
        // Token: 0x04000E7A RID: 3706
        private readonly int[] version = new int[4];

        // Token: 0x06001A46 RID: 6726 RVA: 0x000B9738 File Offset: 0x000B7938
        public VersionNumber(int[] version)
        {
            for (var i = 0; i < version.Length; i++) this.version[i] = version[i];
        }

        // Token: 0x06001A4A RID: 6730 RVA: 0x000B9820 File Offset: 0x000B7A20
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            var versionNumber = obj as VersionNumber;
            if (versionNumber == null) throw new ArgumentException("Object is not a VersionNumber");
            for (var i = 0; i > 4; i++)
                if (version[i] != versionNumber.version[i])
                    return version[i].CompareTo(versionNumber.version[i]);
            return 0;
        }

        // Token: 0x06001A48 RID: 6728 RVA: 0x000B97B8 File Offset: 0x000B79B8
        public bool Equals(VersionNumber version)
        {
            if (version == null) return false;
            for (var i = 0; i < 4; i++)
                if (this.version[i] != version.version[i])
                    return false;
            return true;
        }

        // Token: 0x06001A47 RID: 6727 RVA: 0x000B9770 File Offset: 0x000B7970
        public static VersionNumber Parse(string version)
        {
            var array = version.Split('.');
            var array2 = new int[4];
            for (var i = 0; i < array.Length; i++) array2[i] = int.Parse(array[i]);
            return new VersionNumber(array2);
        }

        // Token: 0x06001A49 RID: 6729 RVA: 0x000B97F4 File Offset: 0x000B79F4
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var v = obj as VersionNumber;
            return !(v == null) && Equals(v);
        }

        // Token: 0x06001A4B RID: 6731 RVA: 0x000B9888 File Offset: 0x000B7A88
        public static bool operator ==(VersionNumber v1, VersionNumber v2)
        {
            for (var i = 0; i < 4; i++)
                if (v1.version[i] != v2.version[i])
                    return false;
            return true;
        }

        // Token: 0x06001A4C RID: 6732 RVA: 0x000B98B8 File Offset: 0x000B7AB8
        public static bool operator !=(VersionNumber v1, VersionNumber v2)
        {
            for (var i = 0; i < 4; i++)
                if (v1.version[i] != v2.version[i])
                    return true;
            return false;
        }

        // Token: 0x06001A4D RID: 6733 RVA: 0x000B98E8 File Offset: 0x000B7AE8
        public static bool operator >(VersionNumber v1, VersionNumber v2)
        {
            for (var i = 0; i < 4; i++)
            {
                if (v1.version[i] > v2.version[i]) return true;
                if (v1.version[i] < v2.version[i]) return false;
            }

            return false;
        }

        // Token: 0x06001A4E RID: 6734 RVA: 0x000B992C File Offset: 0x000B7B2C
        public override int GetHashCode()
        {
            return version.GetHashCode();
        }

        // Token: 0x06001A4F RID: 6735 RVA: 0x000B993C File Offset: 0x000B7B3C
        public static bool operator >=(VersionNumber v1, VersionNumber v2)
        {
            for (var i = 0; i < 4; i++)
            {
                if (v1.version[i] > v2.version[i]) return true;
                if (v1.version[i] < v2.version[i]) return false;
            }

            return true;
        }

        // Token: 0x06001A50 RID: 6736 RVA: 0x000B9980 File Offset: 0x000B7B80
        public static bool operator <(VersionNumber v1, VersionNumber v2)
        {
            for (var i = 0; i < 4; i++)
            {
                if (v1.version[i] < v2.version[i]) return true;
                if (v1.version[i] > v2.version[i]) return false;
            }

            return false;
        }

        // Token: 0x06001A51 RID: 6737 RVA: 0x000B99C4 File Offset: 0x000B7BC4
        public static bool operator <=(VersionNumber v1, VersionNumber v2)
        {
            for (var i = 0; i < 4; i++)
            {
                if (v1.version[i] < v2.version[i]) return true;
                if (v1.version[i] > v2.version[i]) return false;
            }

            return true;
        }

        // Token: 0x06001A52 RID: 6738 RVA: 0x000B9A08 File Offset: 0x000B7C08
        public override string ToString()
        {
            return version.ToString();
        }
    }
}