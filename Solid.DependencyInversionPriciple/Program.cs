using System.Collections.Generic;
using System.Linq;

namespace Solid.DependencyInversionPriciple
{
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }
    public class Person
    {
        public string Name;
    }
    // low-level
    public class Relationships : IRelationshipsBrowser
    {
        private List<(Person, Relationship, Person)> relations =
            new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        public IEnumerable<Person> FindAllChildrenOfParent(string name)
        {
            return relations
                .Where(x => x.Item1.Name == name && x.Item2 == Relationship.Parent)
                .Select(r => r.Item3);
        }
    }
    public interface IRelationshipsBrowser
    {
        IEnumerable<Person> FindAllChildrenOfParent(string name);
    }
    class Research
    {

        static void Main(string[] args)
        {
            var parent = new Person() { Name = "Mark" };
            var child1 = new Person() { Name = "Paul" };
            var child2 = new Person() { Name = "Ann" };

            Relationships _relationships = new Relationships();

            _relationships.AddParentAndChild(parent, child1);
            _relationships.AddParentAndChild(parent, child2);

            _relationships.FindAllChildrenOfParent("Mark")
        }
    }
}
