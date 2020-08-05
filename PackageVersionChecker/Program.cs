using System;
using System.Collections.Generic;
using System.Linq;
using PackageVersionChecker.Models;

namespace PackageVersionChecker
{
    /// <summary>
    /// When the tool starts, it asks for a filepath. The path and it's subfolders will be searched for packages.config files.
    /// After the files are collected, a comparison can be made to see if there are mismatches or all found versions can be shown.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main program.
        /// </summary>
        /// <param name="args">Arguments, not used but required.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Standard required.")]
        public static void Main(string[] args)
        {
            var allVersions = false;

            if (args.Any())
            {
                Console.WriteLine("Commandline arguments are not supported.");
                return;
            }

            // Ask user for directory
            Console.WriteLine($"Enter path to scan for package files (subfolders will be searched):");
            string userPath = Console.ReadLine();

            Console.WriteLine($"Do you want to see all versions and not just the differences? (type 'y'):");
            string userAllVersions = Console.ReadLine();
            if (userAllVersions == "y")
            {
                allVersions = true;
            }

            // Scan all CSPRoj files
            List<KeyValuePair<string, ICollection<PackageVersionItem>>> packages
                = ProjectFilesChecker.FindPackageReferences(userPath, allVersions);

            if (allVersions)
            {
                // Display all the versions found of all the packages used
                DisplayAllVersions(packages);
            }
            else
            {
                // Display projects which use various versions of packages
                DisplayDifferences(packages);
            }

            Console.ReadKey();
        }

        private static void DisplayDifferences(List<KeyValuePair<string, ICollection<PackageVersionItem>>> packages)
        {
            foreach (var mismatchedPackage in packages)
            {
                Console.WriteLine($"Mismatch in package: {mismatchedPackage.Key}");
                foreach (var mismatch in mismatchedPackage.Value)
                {
                    Console.WriteLine($"{mismatch}");
                }

                Console.WriteLine(Environment.NewLine);
            }
        }

        private static void DisplayAllVersions(List<KeyValuePair<string, ICollection<PackageVersionItem>>> packages)
        {
            var groupedPackages = packages.GroupBy(p => p.Key);
            foreach (var package in groupedPackages)
            {
                var versionList = package.Select(p => p.Value
                        .Select(pv => pv.Version)).SelectMany(v => v).Distinct();
                foreach (var version in versionList)
                {
                    Console.WriteLine($"Package: {package.Key} > {version}");
                }
            }
        }
    }
}