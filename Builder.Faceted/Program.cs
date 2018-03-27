using System;

namespace Builder.Faceted
{
    public class Person {
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

		public override string ToString()
		{
            return $"{ StreetAddress } - { Postcode } - { City } - { CompanyName }" +
                   $" - { Position } - { AnnualIncome }";
        }
	}

    public class PersonBuilder // facade 
    {
        protected Person _person = new Person();
        public PersonJobBuilder works => new PersonJobBuilder(_person);
        // expression-bodied member
        public PersonAddressBuilder lives 
        {
            get 
            {
                return new PersonAddressBuilder(_person);
            }
        }

        public Person Return() 
        {
            return _person;    
        }
    }

    public class PersonJobBuilder: PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            _person = person;
        }
    
        public PersonJobBuilder At(string companyName)
        {
            _person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            _person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            _person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            _person = person;
        }

        public PersonAddressBuilder At(string StreetAddress)
        {
            _person.StreetAddress = StreetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            _person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string City)
        {
            _person.City = City;
            return this;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var builder = new PersonBuilder();
            var p = builder.lives.At("Gombrowicza").Return();
            Console.WriteLine(p);
        }
    }
}
