using System.Collections.Generic;
using CodePolice.CodeInspector.Models;
using System.Linq;
using CodePolice.CodeInspector;

namespace CodePolice.Alerts.AgedQaPackage
{
    public class PackageVersioningAlerter : ICodebaseAlerter
    {
        public List<ICodebaseAlert> GenerateAlerts(Codebase codebase)
        {
            var alerts = new List<CodebaseAlert>();
            foreach (var projectDirectory in codebase.ProjectDirectories)
            {
                Dictionary<string, List<ProjectDirectory>> qaOnlyPackages = new Dictionary<string, List<ProjectDirectory>>();
                Dictionary<string, List<ProjectDirectory>> versionMismatchPackages = new Dictionary<string, List<ProjectDirectory>>();
                Dictionary<string, string> packagesUsingProdVersion = new Dictionary<string, string>();
                Dictionary<string, string> notFoundPackages = new Dictionary<string, string>();

                if (projectDirectory.PackagesFile == null)
                {
                    continue;
                }
                                
                foreach (var packageReference in projectDirectory.PackagesFile.PackageReferences)
                {
                    if (packageReference.PackageName == null)
                    {
                        continue;
                    }

                    var packageNameNormalised = packageReference.PackageName.Normalised();
                    var packageNameAndVersionNormalised = packageNameNormalised + "_" + packageReference.Version;
        

                    //Already checked as in prod - OK
                    if (packagesUsingProdVersion.ContainsKey(packageNameAndVersionNormalised))
                    {
                        continue;
                    }
                    if (notFoundPackages.ContainsKey(packageNameAndVersionNormalised))
                    {
                        continue;
                    }


                    //We are not the first project directory to use this QA package
                    List<ProjectDirectory> directoriesWithQaPackageAlready = new List<ProjectDirectory>();
                    if (qaOnlyPackages.TryGetValue(packageNameAndVersionNormalised, out directoriesWithQaPackageAlready))
                    {
                        directoriesWithQaPackageAlready.Add(projectDirectory);
                        continue;
                    }

                    //We are the first to check this package essentially
                    NuGetPackage latestInProd;
                    NuGetPackage latestInQa;
                    codebase.NuGetSourceProduction.LatestVersionLookup.TryGetValue(packageNameNormalised, out latestInProd);
                    codebase.NuGetSourceQa.LatestVersionLookup.TryGetValue(packageNameNormalised, out latestInQa);

                    //Not in either feed! Better log this
                    if (latestInProd == null && latestInQa == null)
                    {
                        notFoundPackages.Add(packageNameAndVersionNormalised, packageNameAndVersionNormalised);
                        continue;
                    }

                    //If it is in prod - add to that index
                    if (latestInProd != null  && packageReference.Version <= latestInProd.Version)
                    {
                        if (!packagesUsingProdVersion.ContainsKey(packageNameAndVersionNormalised))
                        {
                            packagesUsingProdVersion.Add(packageNameAndVersionNormalised, packageNameAndVersionNormalised);
                        }
                        continue;
                    }

                    directoriesWithQaPackageAlready.Add(projectDirectory);
                    qaOnlyPackages.Add(packageNameAndVersionNormalised, directoriesWithQaPackageAlready);
                }

                foreach (var notFoundPackage in notFoundPackages.Keys.OrderBy(k => k))
                {
                    var alert = new CodebaseAlert("Phantom NuGet Package Reference Alert", CodebaseAlertPriority.Critical, null)
                        .AddDetail("Package {0} is not found in either QA or Production Feeds", notFoundPackage);
                    alerts.Add(alert);
                }

                foreach (var qaOnlyPackage in qaOnlyPackages.Keys.OrderBy(k => k))
                {
                    var alert = new CodebaseAlert("QA NuGet Package Reference Alert", CodebaseAlertPriority.Warning, null)
                        .AddDetail("Package {0} is currently only found in QA feed", qaOnlyPackage);
                    alerts.Add(alert);
                }

                foreach (var versionMismatchPackage in versionMismatchPackages.Keys.OrderBy(k => k))
                {
                    PackageVersion expectedVersion = null;
                    var alert = new CodebaseAlert("NuGet Package Version Anomaly Alert", CodebaseAlertPriority.Critical, null)
                        .AddDetail("Package {0} is referenced instead of the standard version {1}", versionMismatchPackage, expectedVersion.ToString());

                    var affectedProjects = versionMismatchPackages[versionMismatchPackage];
                    foreach (var project in affectedProjects)
                    {
                        alert.AddDetail(" - {0}", project.DisplayPath(codebase.RootSourceDirectory));
                    }

                    alerts.Add(alert);
                }
            }


            return alerts.ToList<ICodebaseAlert>();
        }  
    }
}
