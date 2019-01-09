using CompileTimeAnalyzer;
using Xunit;

namespace CompileTimeAnalyzerTests
{
    public class TemplateEvaluatorTests
    {
        [Fact]
        public void Evaluate_template_that_has_no_expressions()
        {
            TemplateEvaluator templateEvaluator = new TemplateEvaluator();
            string template =
@"namespace ProgramTest
{
    public class Program
    {
        static void Main()
        {

        }
    }
}";

            string[] actualEvaluations = templateEvaluator.Evaluate(template);

            Assert.Equal(template, actualEvaluations[0]);
        }
    }
}
