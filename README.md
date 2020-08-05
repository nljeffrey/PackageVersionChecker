# Package Version Checker

This tool can be used to:
 1. Retrieve all used (NuGet) dependencies from .NET (Core) csproj files in a folder and it's subfolders.
 2. Find differences in dependency versions in csproj files in a folder and it's subfolders.

All you have to do is provide the tool with a folder, and all *.csproj* files will be scanned.  

You can let the tool scan a single solution folder to see which dependencies and versions you are using in your projects in that solution.  
Or you can let the tool scan your complete code source folder to find out which dependencies are used and which versions in all your code.  

Another scenario would be to let the tool find only the differences of the same dependencies.
This way, you can see if you use different versions of the same dependency in various projects in the same solution, or accross solutions/projects.

The tool works solely with csproj files.

Idea and code loosely based on:
https://stackoverflow.com/questions/26792624/how-to-enforce-same-nuget-package-version-across-multiple-c-sharp-projects

You can optionally add this code to a unit test to enforce versions to match while your projects are tested.  

Feel free to use this code in any way you like!  

# Example output  
---------------------------  
Show all dependencies used:
---------------------------  
Enter path to scan for package files (subfolders will be searched):  
c:\sources  

Do you want to see all versions and not just the differences? (type 'y'):  
y  

Package: Microsoft.CodeAnalysis.FxCopAnalyzers > 2.9.6  
Package: Microsoft.CodeAnalysis.FxCopAnalyzers > 2.9.8  
Package: StyleCop.Analyzers > 1.1.118  
Package: Microsoft.EntityFrameworkCore.Proxies > 3.0.0  
Package: Microsoft.EntityFrameworkCore.Proxies > 3.1.1  

----------------------------------------  
Show different versions of dependencies:  
----------------------------------------  
Enter path to scan for package files (subfolders will be searched):  
c:\sources  

Do you want to see all versions and not just the differences? (type 'y'):  
no  

Mismatch in package: Microsoft.CodeAnalysis.FxCopAnalyzers  
2.9.6 in c:\sources\SolutionA\Project\Project.csproj  
2.9.8 in c:\sources\SolutionB\Project\Project.csproj  

Mismatch in package: Microsoft.EntityFrameworkCore.Proxies  
3.0.0 in c:\sources\SolutionA\Project\Project.csproj  
3.1.1 in c:\sources\SolutionB\Project\Project.csproj  
