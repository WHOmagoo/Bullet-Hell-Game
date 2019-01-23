using System;

namespace BulletHell
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            LinearPath path = new LinearPath(0,4);

            Tuple<double, double> result = path.updateLocation(5);

            Console.WriteLine(result);
        }
    }
}