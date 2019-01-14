using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace CompileTimeAnalyzer
{
    public class CSharpFileGenerator
    {
        private readonly IFileSystem _fileSystem;
        private readonly ITemplateEvaluator _templateEvaluator;

        public CSharpFileGenerator(IFileSystem fileSystem, ITemplateEvaluator templateEvaluator)
        {
            _fileSystem = fileSystem;
            _templateEvaluator = templateEvaluator;
        }

        public CSharpFileGenerator()
            : this(new FileSystem(), new TemplateEvaluator())
        {

        }

        public void Generate(List<Template> templates, string outputDirectory)
        {
            _fileSystem.Directory.CreateDirectory(outputDirectory);

            foreach (Template template in templates)
            {
                string outputFilePath = Path.Join(outputDirectory, $"{template.Name}.cs");
                string evaluatedText = _templateEvaluator.Evaluate(template.Text)[0];

                _fileSystem.File.WriteAllText(outputFilePath, evaluatedText);
            }
        }
    }
}