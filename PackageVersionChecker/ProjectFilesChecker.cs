using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using PackageVersionChecker.Models;

namespace PackageVersionChecker
{
    /// <summary>
    /// Checks every CSProj file for package dependencies.
    /// Idea and code loosely based on:
    /// https://stackoverflow.com/questions/26792624/how-to-enforce-same-nuget-package-version-across-multiple-c-sharp-projects
    /// </summary>
    public static class ProjectFilesChecker
    {
        /// <summary>
        /// Find all references in CSProj files.
        /// </summary>
        /// <param name="pathToScan">The folder to search (root). Subfolders are also searched.</param>
        /// <param name="listAllVersions">List all found versions, not only different versions.</param>
        /// <returns>List of packages with found versions: only different versions or all versions.</returns>
        public static List<KeyValuePair<string, ICollection<PackageVersionItem>>> FindPackageReferences(
            string pathToScan, bool listAllVersions)
        {
            IDictionary<string, ICollection<PackageVersionItem>> packageVersionsById =
                new Dictionary<string, ICollection<PackageVersionItem>>();

            var projectFilePaths = GetAllCsProjectFilePaths(pathToScan);
            foreach (string csProjectFilePath in projectFilePaths)
            {
                var doc = new XmlDocument();
                doc.Load(csProjectFilePath);

                // Take all ItemGroup elements
                XmlNodeList itemgroupNodes = doc.SelectNodes("Project/ItemGroup");
                foreach (XmlNode itemgroup in itemgroupNodes)
                {
                    XmlNodeList packageReferenceNodes = itemgroup.SelectNodes("PackageReference");
                    if (packageReferenceNodes.Count <= 0)
                    {
                        // No PackageReference element found, assume this is not the element we need
                        continue;
                    }

                    // If an ItemGroup contains PackageReference, process it
                    foreach (XmlNode reference in packageReferenceNodes)
                    {
                        string packageId = reference.Attributes["Include"].Value;
                        string packageVersion = reference.Attributes["Version"].Value;

                        // Add initial found version to list
                        if (!packageVersionsById.TryGetValue(packageId,
                            out ICollection<PackageVersionItem> packageVersions))
                        {
                            packageVersions = new List<PackageVersionItem>();
                            packageVersionsById.Add(packageId, packageVersions);
                        }

                        // Add version to list if version is NOT in the list already (unless all versions must be output)
                        if (!packageVersions.Any(o =>
                            o.Version.Equals(packageVersion, StringComparison.OrdinalIgnoreCase)) || listAllVersions)
                        {
                            packageVersions.Add(new PackageVersionItem
                            {
                                SourceFile = csProjectFilePath,
                                Version = packageVersion
                            });
                        }
                    }
                }
            }

            // Show only difference or all versions found
            var versionCount = listAllVersions ? 0 : 1;

            List<KeyValuePair<string, ICollection<PackageVersionItem>>> packagesWithIncoherentVersions =
                packageVersionsById.Where(kv => kv.Value.Count > versionCount).ToList();
            return packagesWithIncoherentVersions;
        }

        private static IEnumerable<string> GetAllCsProjectFilePaths(string pathToScan)
        {
            return Directory.GetFiles(pathToScan, "*.csproj", SearchOption.AllDirectories);
        }
    }
}