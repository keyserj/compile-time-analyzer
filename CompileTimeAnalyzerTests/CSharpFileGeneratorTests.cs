using CompileTimeAnalyzer;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace CompileTimeAnalyzerTests
{
    public class CSharpFileGeneratorTests
    {
        private readonly MockFileSystem _mockFileSystem;
        private readonly CSharpFileGenerator _cSharpFileGenerator;

        public CSharpFileGeneratorTests()
        {
            _mockFileSystem = new MockFileSystem();
            _cSharpFileGenerator = new CSharpFileGenerator(_mockFileSystem);
        }

        [Fact]
        public void Generate_basic_csharp_file_in_specific_path()
        {
            // Arrange
            MockFileData mockTemplate = new MockFileData(StringConstants.TemplateWithNoExpressions);
            string mockTemplatePath = @"C:\template.txt";
            _mockFileSystem.AddFile(mockTemplatePath, mockTemplate);
            List<string> templatePaths = new List<string>() { mockTemplatePath };

            string outputDirectory = @"C:\output\";
            string outputFileBaseName = "CSharpOutputFiles";

            // Act
            _cSharpFileGenerator.Generate(templatePaths, outputDirectory, outputFileBaseName);

            // Assert
            string outputFilePath = Path.Join(outputDirectory, $"{outputFileBaseName}.cs");
            string actualTextInGeneratedFile = _mockFileSystem.File.ReadAllText(outputFilePath);
            Assert.Equal(StringConstants.TemplateWithNoExpressions, actualTextInGeneratedFile);
        }
    }
}
