using System.IO;
using System.IO.Abstractions;

namespace CompileTimeAnalyzer
{
    public class ProjectFileGenerator
    {
        private readonly IFileSystem _fileSystem;

        public ProjectFileGenerator(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public ProjectFileGenerator() :
            this(fileSystem: new FileSystem())
        {

        }

        public string GetCsprojText()
        {
            return
@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.0</TargetFramework>
  </PropertyGroup>

</Project>";
        }

        public string Generate(string outputDirectory = "./CompiledProject", string projectName = "CompiledProject")
        {
            _fileSystem.Directory.CreateDirectory(outputDirectory);

            string projectFilePath = Path.Combine(outputDirectory, $"{projectName}.csproj");
            _fileSystem.File.WriteAllText(projectFilePath, GetCsprojText());

            return projectFilePath;
        }
    }
}