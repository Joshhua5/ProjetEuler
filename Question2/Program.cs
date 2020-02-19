using System;

namespace Question2
{
    class Program
    {
        private const int ceiling = 4_000_000;

        static void Main(string[] args)
        { 
            var current = 1;
            var previous = 1;
            var sum = 0;

            while (current <= ceiling)
            { 
                if (current % 2 == 0)
                    sum += current;

                (current, previous) = (current + previous, current);  
            } 

            Console.WriteLine(sum);
        }
    }
}
