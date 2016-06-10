using System.Collections.Generic;
using System.Linq;
using CodePolice.CodeInspector.Models;
using CodePolice.CodeInspector;

namespace CodePolice.Alerts.InvalidAssemblyReference
{
    public class InvalidAssemblyReferenceAlerter : ICodebaseAlerter
    {
        public List<ICodebaseAlert> GenerateAlerts(Codebase codebase)
        {
            var alerts = new List<CodebaseAlert>();
            foreach (var projectDirectory in codebase.ProjectDirectories)
            {
                foreach (var projectFile in projectDirectory.ProjectFiles)
                {
                    foreach (var dllReference in projectFile.ReferencedDlls)
                    {
                        if (!dllReference.IsNuGetDllReference())
                        {
                            var alert = new CodebaseAlert("Invalid Dll Reference Alert", CodebaseAlertPriority.Critical, projectDirectory)
                                .AddDetail("Reference to dll {0} should be replaced with a proper NuGet reference", dllReference.HintPath);
                            alerts.Add(alert);
                        }
                    }
                }
            }
            return alerts.ToList<ICodebaseAlert>();
        }
    }
}
