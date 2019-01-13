using CompileTimeAnalyzer;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using Xunit;

namespace CompileTimeAnalyzerTests
{
    public class CSharpFileGeneratorTests
    {
        private readonly MockFileSystem _mockFileSystem;
        private readonly Mock<ITemplateEvaluator> _mockTemplateEvaluator;
        private readonly CSharpFileGenerator _cSharpFileGenerator;

        private readonly string _outputDirectory = @"C:\output\";
        private readonly List<string> _templatePaths = new List<string>();

        public CSharpFileGeneratorTests()
        {
            _mockFileSystem = new MockFileSystem();
            _mockTemplateEvaluator = new Mock<ITemplateEvaluator>();
            _mockTemplateEvaluator.Setup(te => te.Evaluate(It.IsAny<string>())).Returns<string>(t => new string[] { t });

            _cSharpFileGenerator = new CSharpFileGenerator(_mockFileSystem, _mockTemplateEvaluator.Object);
        }

        [Fact]
        public void Generate_basic_csharp_file_in_specific_path()
        {
            AddTemplateToMockFileSystem(@"C:\template.txt");

            _cSharpFileGenerator.Generate(_templatePaths, _outputDirectory);

            AssertFileIsCreatedInOutputDirectory("template.cs");
        }

        [Fact]
        public void Generate_three_basic_csharp_files_in_specific_path()
        {
            AddTemplateToMockFileSystem(@"C:\template1.txt");
            AddTemplateToMockFileSystem(@"C:\template2.txt");
            AddTemplateToMockFileSystem(@"C:\template3.txt");

            _cSharpFileGenerator.Generate(_templatePaths, _outputDirectory);

            AssertFileIsCreatedInOutputDirectory("template1.cs");
            AssertFileIsCreatedInOutputDirectory("template2.cs");
            AssertFileIsCreatedInOutputDirectory("template3.cs");
        }

        [Fact]
        public void Generate_evaluates_one_template()
        {
            AddTemplateToMockFileSystem(@"C:\template.txt");

            _cSharpFileGenerator.Generate(_templatePaths, _outputDirectory);

            _mockTemplateEvaluator.Verify(te => te.Evaluate(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public void Generate_evaluates_multiple_templates()
        {
            AddTemplateToMockFileSystem(@"C:\template1.txt");
            AddTemplateToMockFileSystem(@"C:\template2.txt");
            AddTemplateToMockFileSystem(@"C:\template3.txt");

            _cSharpFileGenerator.Generate(_templatePaths, _outputDirectory);

            _mockTemplateEvaluator.Verify(te => te.Evaluate(It.IsAny<string>()), Times.Exactly(3));
        }

        private void AddTemplateToMockFileSystem(string templatePath)
        {
            MockFileData mockTemplate = new MockFileData(string.Empty);
            _mockFileSystem.AddFile(templatePath, mockTemplate);
            _templatePaths.Add(templatePath);
        }

        private void AssertFileIsCreatedInOutputDirectory(string fileName)
        {
            string filePath = Path.Join(_outputDirectory, fileName);
            Assert.True(_mockFileSystem.File.Exists(filePath));
        }
    }
}
