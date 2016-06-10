using System.Collections.Generic;
using System.IO;

namespace CodePolice.CodeInspector.Models
{
    public class Codebase
    {
        public DirectoryInfo RootSourceDirectory { get; set; }
        public List<ProjectDirectory> ProjectDirectories { get; set; }
        public NuGetSource NuGetSourceProduction { get; set; }
        public NuGetSource NuGetSourceQa { get; set; }
    }
}
