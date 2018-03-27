using System;

namespace Factory.FactoryMethod
{
    public class Point {
        private double _a, _b;

        public static Point NewCartesianPoint(double x, double y){
            return new Point(x,y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));     
        }

        private Point(double a, double b)
        {
            _a = a;
            _b = b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var p = Point.NewCartesianPoint(2, 3);
        }        
    }
}
