using System.Collections.Generic;

namespace CodePolice.CodeInspector.Models
{
    public class NuGetSource
    {
        public List<NuGetPackage> Packages { get; set; }
        public Dictionary<string, NuGetPackage> LatestVersionLookup { get; set; }
    }
}
