using System.IO;

namespace CodePolice.CodeInspector.Models
{
    public class DllReference
    {
        public string HintPath { get; set; }
        public FileInfo FileReferenced { get; set; }
    }
}
