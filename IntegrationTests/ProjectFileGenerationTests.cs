using CompileTimeAnalyzer;
using System.IO;
using Xunit;

namespace IntegrationTests
{
    public class ProjectFileGenerationTests
    {
        private ProjectFileGenerator _projectFileGenerator;

        public ProjectFileGenerationTests()
        {
            _projectFileGenerator = new ProjectFileGenerator();
        }

        private void Cleanup(string directoryPath)
        {
            Directory.Delete(directoryPath, recursive: true);
        }

        [Fact]
        public void Generate_project_file_in_default_directory()
        {

            _projectFileGenerator.Generate();

            string expectedDirectoryPath = "./CompiledProject/";
            string expectedFilePath = Path.Join(expectedDirectoryPath, "CompiledProject.csproj");
            Assert.True(File.Exists(expectedFilePath));

            Cleanup(expectedDirectoryPath);
        }

        [Fact]
        public void Generate_project_file_in_specific_directory_with_specific_name()
        {
            string directoryPath = "./TestDirectory";
            string projectName = "TestProjectName";

            _projectFileGenerator.Generate(directoryPath, projectName);

            string expectedFilePath = Path.Join(directoryPath, projectName + ".csproj");
            Assert.True(File.Exists(expectedFilePath));

            Cleanup(directoryPath);
        }

        [Fact]
        public void Generate_project_returns_file_path()
        {
            string directoryPath = "TestDirectory";
            string projectName = "TestProjectName";

            string actualFilePath = _projectFileGenerator.Generate(directoryPath, projectName);

            string expectedFilePath = Path.Join(directoryPath, projectName + ".csproj");
            Assert.Equal(expectedFilePath, actualFilePath);

            Cleanup(directoryPath);
        }

        [Fact]
        public void Generate_project_file_with_csproj_text()
        {

            string filePath = _projectFileGenerator.Generate();

            string expectedCsprojText = _projectFileGenerator.GetCsprojText();
            string actualCsprojText = File.ReadAllText(filePath);
            Assert.Equal(expectedCsprojText, actualCsprojText);

            Cleanup(Path.GetDirectoryName(filePath));
        }
    }
}
