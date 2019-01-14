namespace CompileTimeAnalyzer
{
    public class Template
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Template(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}
