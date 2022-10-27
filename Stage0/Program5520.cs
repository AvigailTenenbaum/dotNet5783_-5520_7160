using System;
namespace Stage0
{
    partial class Program
    {
        private static void Main(string[] args)
        {
            Welcome5520();
            Welcome7160();
            Console.ReadKey();
        }
        static partial void Welcome7160();
        private static void Welcome5520()
        {
            string name;
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}