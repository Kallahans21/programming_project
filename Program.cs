using System;
using programming_project.model;

namespace programming_project
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Console.WriteLine($"Total: {user.test(new double[] {2.5, 2.5})}");
        }
    }
}
