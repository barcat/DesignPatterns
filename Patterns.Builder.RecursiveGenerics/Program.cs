using System;

namespace Patterns.Builder.RecursiveGenerics
{

    public class Person
    {
        public string Name;
        public string Position;
        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

        public class Builder : PersonalJobBuilder<Builder> 
        {
            
        }
    
        public static Builder New => new Builder();
    }

    public abstract class PersonBuilder 
    {
        protected Person person = new Person();
        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF>: PersonBuilder 
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF)this;
        }
    }

    public class PersonalJobBuilder<SELF> : PersonInfoBuilder<PersonalJobBuilder<SELF>> 
        where SELF :PersonalJobBuilder<SELF> 
    {
        public SELF WorkAsA(string position)
        {   
            person.Position = position;
            return (SELF)this;
        }
    }

    class Demo
    {
        static void Main(string[] args)
        {
           var p =  Person.New
                  .Called("Prze")
                  .WorkAsA("SBT")
                  .Build();
            Console.WriteLine(p);
            Console.ReadKey();
        }
    }
}