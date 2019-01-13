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

        public void Generate(List<string> templatePaths, string outputDirectory)
        {
            _fileSystem.Directory.CreateDirectory(outputDirectory);

            foreach (string templatePath in templatePaths)
            {
                string outputFileName = Path.GetFileNameWithoutExtension(templatePath);
                string outputFilePath = Path.Join(outputDirectory, $"{outputFileName}.cs");

                string template = _fileSystem.File.ReadAllText(templatePath);
                string evaluatedText = _templateEvaluator.Evaluate(template)[0];

                _fileSystem.File.WriteAllText(outputFilePath, evaluatedText);
            }
        }
    }
}