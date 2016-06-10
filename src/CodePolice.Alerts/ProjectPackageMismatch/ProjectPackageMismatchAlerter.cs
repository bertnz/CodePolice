using CodePolice.CodeInspector;
using CodePolice.CodeInspector.Models;
using System.Collections.Generic;
using System.Linq;

namespace CodePolice.Alerts.ProjectPackageMismatch
{
    public class ProjectPackageMismatchAlerter : ICodebaseAlerter
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
                        //We only care about NuGet packages here
                        if (!dllReference.IsNuGetDllReference())
                        {
                            continue;
                        }

                        //TODO - we need to make this configurable a bit somehow
                        var expectedPackageName = dllReference.FileReferenced.NameWithoutExtension().Normalised();

                        if (projectDirectory.PackagesFile == null)
                        {
                            var alert = new CodebaseAlert("Missing Packages.config Alert", CodebaseAlertPriority.Bad, projectDirectory)
                                .AddDetail("Packages.config file missing - expected for dll reference {0}", dllReference.HintPath);
                            alerts.Add(alert);
                        }
                        else
                        {
                            var packageReference = projectDirectory.PackagesFile.PackageReferences.FirstOrDefault(p => p.PackageNameNormalised == expectedPackageName);
                            if (packageReference == null)
                            {
                                var alert = new CodebaseAlert("Missing Packages.config Reference Alert", CodebaseAlertPriority.Bad, projectDirectory)
                                    .AddDetail("Reference to NuGet package {0} should be in packages.config for dll refernece {1}", expectedPackageName, dllReference.HintPath);
                                alerts.Add(alert);
                            }
                        } 
                    }
                }
            }
            return alerts.ToList<ICodebaseAlert>();
        }
    }
}
