using System;

namespace Triangle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("неизвестная ошибка");
                return;
            }
            try
            {
                double a = Double.Parse(args[0]);
                double b = Double.Parse(args[1]);
                double c = Double.Parse(args[2]);
                if (a + b <= c || a + c <= b || b + c <= a || a < 0 || b < 0 || c < 0)
                {
                    Console.WriteLine("не треугольник");
                }
                else if (a != b && a != c && b != c)
                {
                    Console.WriteLine("обычный");
                }
                else if (a == b && b == c)
                {
                    Console.WriteLine("равносторонний");
                }
                else
                {
                    Console.WriteLine("равнобедренный");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("неизвестная ошибка");
            }
        }
    }
}
