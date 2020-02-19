using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Tools;

namespace Question3
{
    class Program
    {  
        /// <summary>
        /// Find the primes that are can be multiplied to produce the value
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //var value = 13_195;
            var value = 600_851_475_143;


            var candidatePrimes = Tools.Common.GetPrimesSieve(2, (long)Math.Sqrt(value));

            var primeFactors = candidatePrimes
                .Where(x => (value % x) == 0)
                .ToArray();

            foreach (var prime in primeFactors)
                Console.WriteLine(prime);

            Console.WriteLine($"Largest: {primeFactors.Max()}");
        }
    }
}
