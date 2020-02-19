using System;
using System.Linq;

namespace Question1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = Enumerable.Range(1, 999 - 1)
                .Where(x => (x % 3) == 0 || (x % 5) == 0)
                .Sum(x => x);

            Console.WriteLine(sum);
        }
    }
}
