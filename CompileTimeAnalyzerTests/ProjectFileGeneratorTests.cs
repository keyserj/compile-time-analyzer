using CompileTimeAnalyzer;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace CompileTimeAnalyzerTests
{
    public class ProjectFileGeneratorTests
    {
        private readonly MockFileSystem _mockFileSystem;
        private readonly ProjectFileGenerator _projectFileGenerator;

        public ProjectFileGeneratorTests()
        {
            _mockFileSystem = new MockFileSystem();
            _projectFileGenerator = new ProjectFileGenerator(_mockFileSystem);
        }

        [Fact]
        public void Get_csproj_text()
        {
            string expected =
@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.0</TargetFramework>
  </PropertyGroup>

</Project>";

            string actual = _projectFileGenerator.GetCsprojText();

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
        }

        [Fact]
        public void Generate_writes_csproj_text_to_file()
        {
            string csprojText = _projectFileGenerator.GetCsprojText();

            string path = _projectFileGenerator.Generate();

            Assert.Equal(csprojText, _mockFileSystem.File.ReadAllText(path));
        }
    }
}
