namespace PackageVersionChecker.Models
{
    /// <summary>
    /// Package version model.
    /// </summary>
    public class PackageVersionItem
    {
        /// <summary>
        /// Gets or sets the source file, CSPROJ file path or 'packages.config' file path.
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets or sets the version of the package.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Output version and source file in readable way.
        /// </summary>
        /// <returns>Readable problem string.</returns>
        public override string ToString()
        {
            return $"{Version} in {SourceFile}";
        }
    }
}