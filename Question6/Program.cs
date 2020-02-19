using System;

namespace Question6
{
    class Program
    {
        static void Main(string[] args)
        {

            long squaresSum = 0;
            long sumSquared = 0;

            for (long i = 1; i <= 100; ++i)
            {
                sumSquared += i;
                squaresSum += i * i;
            }

            sumSquared *= sumSquared;

            Console.WriteLine(sumSquared - squaresSum);
        }
    }
}
