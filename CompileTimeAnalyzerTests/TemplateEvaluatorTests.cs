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

            string[] actualEvaluations = templateEvaluator.Evaluate(StringConstants.TemplateWithNoExpressions);

            Assert.Equal(StringConstants.TemplateWithNoExpressions, actualEvaluations[0]);
        }
    }
}
