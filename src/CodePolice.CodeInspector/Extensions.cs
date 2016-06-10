using CodePolice.CodeInspector.Models;
using System.IO;

namespace CodePolice.CodeInspector
{
    public static class Extensions
    {
        public static bool IsNuGetDllReference(this DllReference dllReference)
        {
            return false;
        }

        public static string Normalised(this string s)
        {
            if (s == null)
            {
                return s;
            }
            return s.Trim().ToLowerInvariant();
        }

        public static string DisplayPath(this ProjectDirectory directory, DirectoryInfo sourceDirectory)
        {
            //TODO -relativeise this
            return directory.Directory.FullName;
        }


        public static string NameWithoutExtension(this FileInfo file)
        {
            //TODO -work this
            return file.Name;
        }
    }
}
