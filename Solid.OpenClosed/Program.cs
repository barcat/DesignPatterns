using System;
using System.Collections.Generic;

namespace Solid.OpenClosed
{
    public enum Color
    {
        Red, Green, Blue
    }
    public enum Size
    {
        Small, Medium, Large, Huge
    }
    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;
        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    #region old filter
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("apple", Color.Green, Size.Small);
            var tree = new Product("tree", Color.Green, Size.Large);
            var house = new Product("house", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var bf = new BetterFilter();

            Console.WriteLine("Color:");
            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name}");
            }

            Console.WriteLine("Size:");
            foreach (var p in bf.Filter(products, new SizeSpecification(Size.Small)))
            {
                Console.WriteLine($" - {p.Name}");
            }

            var config = new AndSpecification<Product>(
                new SizeSpecification(Size.Small), new ColorSpecification(Color.Green));
            Console.WriteLine("Combinator:");
            foreach (var p in bf.Filter(products, config))
            {
                Console.WriteLine($" - {p.Name}");
            }
        }
    }
    #region pattern implementation - new filter
    // Specification pattern:

    // check whether a particular type satisfy some - not set yet - criteria 
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> specification);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;

        public bool IsSatisfied(Product t)
        {
            return _color == t.Color;
        }
        public ColorSpecification(Color color)
        {
            _color = color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
           _size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return _size == t.Size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> _first, _second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            _first = first;
            _second = second;
        }

        public bool IsSatisfied(T t)
        {
            return _first.IsSatisfied(t) && _second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> specification)
        {
            foreach (var i in items)
                if (specification.IsSatisfied(i))
                    yield return i;
        }
    }
    #endregion
}
