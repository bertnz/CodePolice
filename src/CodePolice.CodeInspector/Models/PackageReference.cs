using CodePolice.CodeInspector.Models;

namespace CodePolice.CodeInspector.Models
{
    public class PackageReference
    {
        public string PackageName { get; set; }
        public string PackageNameNormalised { get; set; }
        public PackageVersion Version { get; set; }
    }
}
