using System;

namespace CompileTimeAnalyzer
{
    public class ProjectFileGenerator
    {
        public string GetCsprojText()
        {
            return
@"<Project Sdk=""Microsoft.NET.Sdk"">

  <PropertyGroup>
    <TargetFramework>netcoreapp3.0</TargetFramework>
  </PropertyGroup>

</Project>";
        }

        public void Generate()
        {
            throw new NotImplementedException();
        }
    }
}