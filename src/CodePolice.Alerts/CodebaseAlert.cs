using System.Collections.Generic;
using CodePolice.CodeInspector.Models;

namespace CodePolice.Alerts
{
    public class CodebaseAlert : ICodebaseAlert
    {
        public CodebaseAlert(string alertTypeName, CodebaseAlertPriority priority, ProjectDirectory projectDirectory)
        {
            Priority = priority;
            AlertTypeName = alertTypeName;
            Project = projectDirectory;
        }

        public List<string> AlertDetails { get; set; }
      
        public string AlertTypeName { get; set; }

        public CodebaseAlertPriority Priority { get; set; }

        public ProjectDirectory Project { get; set; }

        public CodebaseAlert AddDetail(string detail)
        {
            return AddDetail(detail, null);
        }
        public CodebaseAlert AddDetail(string detailFormatted, params string[] parameters)
        {
            AlertDetails.Add(detailFormatted);
            return this;
        }
    }
}
