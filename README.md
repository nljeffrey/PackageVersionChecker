# PackageVersionChecker
This tool can be used to retrieve all used (NuGet) dependencies in .NET (Core) projects and to find different versions between projects in the same solution.

Idea and code loosely based on:
https://stackoverflow.com/questions/26792624/how-to-enforce-same-nuget-package-version-across-multiple-c-sharp-projects

You can optionally add this code to a unit test to enforce versions to match while your projects are tested.

# Example  
------------------------------  
Show all versions of packages:  
------------------------------  
Enter path to scan for package files (subfolders will be searched):  
c:\sources  

Do you want to see all versions and not just the differences? (type 'y'):  
y  
Package: Microsoft.CodeAnalysis.FxCopAnalyzers > 2.9.6  
Package: Microsoft.CodeAnalysis.FxCopAnalyzers > 2.9.8  
Package: StyleCop.Analyzers > 1.1.118  
Package: Microsoft.EntityFrameworkCore.Proxies > 3.0.0  
Package: Microsoft.EntityFrameworkCore.Proxies > 3.1.1  

------------------------------------  
Show different versions of packages:  
------------------------------------  
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
