using System.Collections.Generic;
using System.IO;

namespace CodePolice.CodeInspector.Models
{
    public class ProjectFile
    {
        public FileInfo File { get; set; }
        public FileInfo PackagesConfigFile { get; set; }
        
        public List<FileInfo> ReferencedProjectFiles { get; set; }        
        public List<DllReference> ReferencedDlls { get; set; }
    }
}
