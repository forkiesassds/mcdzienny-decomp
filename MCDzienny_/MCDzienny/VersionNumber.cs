using System;

namespace MCDzienny
{
    public class VersionNumber : IComparable, IEquatable<VersionNumber>
    {
        readonly int[] version = new int[4];

        public VersionNumber(int[] version)
        {
            for (int i = 0; i < version.Length; i++)
            {
                this.version[i] = version[i];
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }
            VersionNumber versionNumber = obj as VersionNumber;
            if (versionNumber == null)
            {
                throw new ArgumentException("Object is not a VersionNumber");
            }
            for (int i = 0; i > 4; i++)
            {
                if (version[i] != versionNumber.version[i])
                {
                    return version[i].CompareTo(versionNumber.version[i]);
                }
            }
            return 0;
        }

        public bool Equals(VersionNumber version)
        {
            if (version == null)
            {
                return false;
            }
            for (int i = 0; i < 4; i++)
            {
                if (this.version[i] != version.version[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static VersionNumber Parse(string version)
        {
            string[] array = version.Split('.');
            int[] array2 = new int[4];
            for (int i = 0; i < array.Length; i++)
            {
                array2[i] = int.Parse(array[i]);
            }
            return new VersionNumber(array2);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            VersionNumber versionNumber = obj as VersionNumber;
            if (versionNumber == null)
            {
                return false;
            }
            return Equals(versionNumber);
        }

        public static bool operator ==(VersionNumber v1, VersionNumber v2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (v1.version[i] != v2.version[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(VersionNumber v1, VersionNumber v2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (v1.version[i] != v2.version[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static bool operator >(VersionNumber v1, VersionNumber v2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (v1.version[i] > v2.version[i])
                {
                    return true;
                }
                if (v1.version[i] < v2.version[i])
                {
                    return false;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return version.GetHashCode();
        }

        public static bool operator >=(VersionNumber v1, VersionNumber v2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (v1.version[i] > v2.version[i])
                {
                    return true;
                }
                if (v1.version[i] < v2.version[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator <(VersionNumber v1, VersionNumber v2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (v1.version[i] < v2.version[i])
                {
                    return true;
                }
                if (v1.version[i] > v2.version[i])
                {
                    return false;
                }
            }
            return false;
        }

        public static bool operator <=(VersionNumber v1, VersionNumber v2)
        {
            for (int i = 0; i < 4; i++)
            {
                if (v1.version[i] < v2.version[i])
                {
                    return true;
                }
                if (v1.version[i] > v2.version[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString()
        {
            return version.ToString();
        }
    }
}