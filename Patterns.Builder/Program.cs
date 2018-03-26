// When piecewise object construction is complicated,
// provide an API for doing it.

using System.Collections.Generic;
using System.Text;
using System;

namespace Patterns.Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elemensts = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement() {
        }

        public HtmlElement(string name, string text)
        {
            Name = name ?? throw new AggregateException(nameof(name));
            Text = text ?? throw new AggregateException(nameof(text));
        }
        private string ToString(int indent)
        {
            var stringBuilder = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            stringBuilder.AppendLine($"{i}<{Name}>");

            if (!string.IsNullOrWhiteSpace(Text))
            {
                stringBuilder.Append(new string(' ', indentSize * (indent + 1)));
                stringBuilder.AppendLine(Text);
            }

            foreach (var element in Elemensts)
            {
                stringBuilder.Append(element.ToString(indent + 1));
            }
            stringBuilder.AppendLine($"{i}</{Name}>");
            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            return ToString(0);
        }
    }

    public class HtmlBuilder
    {
        private readonly string _rootName;
        HtmlElement root = new HtmlElement();
        public HtmlBuilder(string rootName)
        {
            _rootName = rootName;
            root.Name = rootName;
        }
        public void AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elemensts.Add(e);   
        }
        public override string ToString()
        {
            return root.ToString();
        }
    }

    class Demo
    {
        static void Main(string[] args)
        {
            //var htmlBuilder = new HtmlBuilder("ul");
            //htmlBuilder.AddChild("li", "Apple");
            //htmlBuilder.AddChild("li", "Banana");
            //Console.WriteLine(htmlBuilder.ToString());
            //Console.ReadKey();
            var sqlUpdateBuilder = new SqlUpdateBuilder("dbo.Persons");

            sqlUpdateBuilder
                   .AddValueToBeSet("name", "Ann")
                   .AddValueToBeSet("sureName", "Ann")
                   .AddValueToBeSet("age", "29");

            sqlUpdateBuilder
                .AddCondition("country", "Poland")
                .AddCondition("email", "ann@test.pl");

            Console.WriteLine(sqlUpdateBuilder.ToString());
            Console.ReadKey();
        }
    }
}
