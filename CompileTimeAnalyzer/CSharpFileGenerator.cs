using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;

namespace CompileTimeAnalyzer
{
    public class CSharpFileGenerator
    {
        private IFileSystem _fileSystem;

        public CSharpFileGenerator(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public CSharpFileGenerator()
            : this(new FileSystem())
        {

        }

        public void Generate(List<string> templatePaths, string outputDirectory, string outputFileBaseName)
        {
            string outputFilePath = Path.Join(outputDirectory, $"{outputFileBaseName}.cs");
            _fiS
        }
    }
}