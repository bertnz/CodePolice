using System.Collections.Generic;
using System.IO;

namespace CodePolice.CodeInspector.Models
{
    public class ProjectDirectory
    {
        public DirectoryInfo Directory { get; set; }
        public PackagesConfigFile PackagesFile { get; set; }
        public List<ProjectFile> ProjectFiles { get; set; }        
    }
}
