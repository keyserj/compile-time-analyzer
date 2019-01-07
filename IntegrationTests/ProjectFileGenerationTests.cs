using CompileTimeAnalyzer;
using System.IO;
using Xunit;

namespace IntegrationTests
{
    public class ProjectFileGenerationTests
    {
        [Fact]
        public void Generate_project_file_in_default_directory()
        {
            ProjectFileGenerator projectFileGenerator = new ProjectFileGenerator();

            projectFileGenerator.Generate();

            string expectedDirectoryPath = "./CompiledProject/";
            string expectedFilePath = Path.Join(expectedDirectoryPath, "CompiledProject.csproj");
            Assert.True(File.Exists(expectedFilePath));

            Directory.Delete(expectedDirectoryPath, recursive: true);
        }

        //[Fact]
        //public void Generate_project_file_in_specific_directory()
        //{

        //}

        //[Fact]
        //public void Generate_project_file_with_csproj_text()
        //{

        //}
    }
}
