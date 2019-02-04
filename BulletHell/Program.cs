using System;

namespace BulletHell
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            LinearEquation equation = new LinearEquation(0,4);

            Tuple<double, double> result = equation.updateLocation(5);

            Console.WriteLine(result);
        }
    }
}