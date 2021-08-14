using System;

namespace CSharp_003
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello anonimous class test");

            var anon = new { Name = "Saulo", Age = 29 };
            Console.WriteLine (anon.Name + " " + anon.Age);
        }
    }
}
