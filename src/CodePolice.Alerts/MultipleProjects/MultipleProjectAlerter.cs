using System.Collections.Generic;
using CodePolice.CodeInspector.Models;
using System.Linq;

namespace CodePolice.Alerts.MultipleProjects
{
    public class MultipleProjectAlerter : ICodebaseAlerter
    {
        public List<ICodebaseAlert> GenerateAlerts(Codebase codebase)
        {
            var alerts = new List<CodebaseAlert>();
            foreach (var projectDirectory in codebase.ProjectDirectories)
            {
                if (projectDirectory.ProjectFiles.Count > 1)
                {
                    var alert = new CodebaseAlert("MultipleProjectAlert", CodebaseAlertPriority.Warning, projectDirectory)
                        .AddDetail("Multiple project files detected in directory");                    
                }
            }
            return alerts.ToList<ICodebaseAlert>();
        }
    }
}
