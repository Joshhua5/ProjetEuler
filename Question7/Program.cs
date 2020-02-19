using System;
using System.Linq;
using Tools;

namespace Question7
{
    class Program
    {
        static void Main(string[] args)
        {
            var primes = Common.GetPrimesSieve(2, 1_000_000).OrderBy(x => x).ToArray();
            
            for(var i = 1; i != 10_003; ++i)
                Console.WriteLine($"{i} : {primes[i - 1]}");
             
        }
    }
}
