using CompileTimeAnalyzer;
using Xunit;

namespace CompileTimeAnalyzerTests
{
    public class ProjectFileGeneratorTests
    {
        [Fact]
        public void Get_csproj_text()
        {
            string expected =
@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.0</TargetFramework>
  </PropertyGroup>

</Project>";
            ProjectFileGenerator projectFileGenerator = new ProjectFileGenerator();

            string actual = projectFileGenerator.GetCsprojText();

            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true, ignoreWhiteSpaceDifferences: true);
        }
    }
}
