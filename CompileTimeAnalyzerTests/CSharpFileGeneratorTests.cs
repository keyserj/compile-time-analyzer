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
        private readonly List<Template> _templates = new List<Template>();

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
            AddTemplate("template");

            _cSharpFileGenerator.Generate(_templates, _outputDirectory);

            AssertFileIsCreatedInOutputDirectory("template.cs");
        }

        [Fact]
        public void Generate_three_basic_csharp_files_in_specific_path()
        {
            AddTemplate("template1");
            AddTemplate("template2");
            AddTemplate("template3");

            _cSharpFileGenerator.Generate(_templates, _outputDirectory);

            AssertFileIsCreatedInOutputDirectory("template1.cs");
            AssertFileIsCreatedInOutputDirectory("template2.cs");
            AssertFileIsCreatedInOutputDirectory("template3.cs");
        }

        [Fact]
        public void Generate_evaluates_one_template()
        {
            AddTemplate("template");

            _cSharpFileGenerator.Generate(_templates, _outputDirectory);

            _mockTemplateEvaluator.Verify(te => te.Evaluate(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public void Generate_evaluates_multiple_templates()
        {
            AddTemplate("template1");
            AddTemplate("template2");
            AddTemplate("template3");

            _cSharpFileGenerator.Generate(_templates, _outputDirectory);

            _mockTemplateEvaluator.Verify(te => te.Evaluate(It.IsAny<string>()), Times.Exactly(3));
        }

        private void AssertFileIsCreatedInOutputDirectory(string fileName)
        {
            string filePath = Path.Join(_outputDirectory, fileName);
            Assert.True(_mockFileSystem.File.Exists(filePath));
        }

        private void AddTemplate(string templateName)
        {
            _templates.Add(new Template(templateName, It.IsAny<string>()));
        }
    }
}
