// When piecewise object construction is complicated,
// provide an API for doing it.

using System.Collections.Generic;

namespace Patterns.Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elemensts = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
    class Program
    {
        static void Demo(string[] args)
        {
        }
    }
}
