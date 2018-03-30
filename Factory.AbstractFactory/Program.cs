using System;
using System.Collections.Generic;

namespace Factory.AbstractFactory
{
    public interface IHotDrink
    {
        void Consume();
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);

    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice");
        }
    }
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Great coffee, dude!");
        }
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Prepare {amount} ml");
            return new Tea();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
            Coffee, Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
            new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var typeName = "Factory.AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory";
                var type = Type.GetType(typeName);
                var factory = (IHotDrinkFactory)Activator.CreateInstance(type);
                factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(AvailableDrink drink, int amount) {
            return factories[drink].Prepare(amount);
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Prepare {amount} ml");
            return new Coffee();
        }
    }
    class Demo
    {
        static void Main(string[] args)
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 50);
        }
    }
}
