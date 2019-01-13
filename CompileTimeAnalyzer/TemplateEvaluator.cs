namespace CompileTimeAnalyzer
{
    public class TemplateEvaluator : ITemplateEvaluator
    {
        public string[] Evaluate(string template)
        {
            return new string[] { template };
        }
    }

    public interface ITemplateEvaluator
    {
        string[] Evaluate(string template);
    }
}