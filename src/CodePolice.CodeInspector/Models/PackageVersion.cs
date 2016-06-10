using System;
using System.Collections.Generic;
using System.Linq;

namespace CodePolice.CodeInspector.Models
{
    public class PackageVersion
    {
        internal List<int> Parts;
        private string _rawString;

        public PackageVersion(string versionString)
        {
            if (versionString == null)
            {
                throw new ArgumentNullException("versionString");
            }
            _rawString = versionString;
            var partStrings = versionString.Trim().Split(".".ToCharArray()).ToList();
            Parts = new List<int>();
            foreach (var partString in partStrings)
            {
                int part;
                if (Int32.TryParse(partString, out part))
                {
                    Parts.Add(part);
                }
            }
        }
        
        public static bool operator <(PackageVersion version1, PackageVersion version2)
        {

            return Comparison(version1, version2) < 0;

        }

        public static bool operator >(PackageVersion version1, PackageVersion version2)
        {

            return Comparison(version1, version2) > 0;

        }

        public static bool operator ==(PackageVersion version1, PackageVersion version2)
        {

            return Comparison(version1, version2) == 0;

        }

        public static bool operator !=(PackageVersion version1, PackageVersion version2)
        {

            return Comparison(version1, version2) != 0;

        }

        public override bool Equals(object obj)
        {

            if (!(obj is PackageVersion)) return false;

            return this == (PackageVersion)obj;

        }

        public override string ToString()
        {
            return _rawString;
        }

        public override int GetHashCode()
        {
            return _rawString.GetHashCode();
        }

        public static bool operator <=(PackageVersion version1, PackageVersion version2)
        {

            return Comparison(version1, version2) <= 0;

        }

        public static bool operator >=(PackageVersion version1, PackageVersion version2)
        {

            return Comparison(version1, version2) >= 0;

        }

        private static int Comparison(PackageVersion version1, PackageVersion version2)
        {
            for (int i = 0; i < version1.Parts.Count; i++)
            {
                //Second version has less parts - must be smaller
                if (version2.Parts.Count < i+1)
                {
                    return 1;
                }

                if (version1.Parts[i] > version2.Parts[i])
                {
                    return 1;
                }
                if (version1.Parts[i] < version2.Parts[i])
                {
                    return -1;
                }
            }
            return 0;            
        }
    }
}
