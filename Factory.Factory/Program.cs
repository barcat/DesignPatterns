using System;

namespace Factory.FactoryMethod
{
    public class Point
    {
        private double _a, _b;

        internal Point(double a, double b)
        {
            _a = a;
            _b = b;
        }

        public override string ToString()
        {
            return $"{_a} - {_b}";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var p = PointFactory.NewPolarPoint(1, 2);
            Console.WriteLine(p);
            Console.ReadKey();
        }
    }

    public class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }
        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
}
