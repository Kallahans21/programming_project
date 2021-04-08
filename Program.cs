using System.Collections.Generic;
using System;
using programming_project.model;

namespace programming_project
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Ingrese un número");
                numbers.Add(Convert.ToInt32(Console.ReadLine()));
            }

            foreach (var item in numbers)
            {
                Console.WriteLine(item);
            }
        }
    }
}
