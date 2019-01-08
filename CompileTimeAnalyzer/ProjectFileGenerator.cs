using System.IO;

namespace CompileTimeAnalyzer
{
    public class ProjectFileGenerator
    {
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
            Directory.CreateDirectory(outputDirectory);

            string projectFilePath = Path.Combine(outputDirectory, $"{projectName}.csproj");
            File.WriteAllText(projectFilePath, GetCsprojText());

            return projectFilePath;
        }
    }
}