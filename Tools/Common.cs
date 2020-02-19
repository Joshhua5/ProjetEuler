using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class Common
    {
        public static IEnumerable<long> GetPrimesSieve(long start, long end)
        {
            if (start < 2)
                start = 2;

            var nonPrimes = new HashSet<long>(); 

            for(long x = start; x < end; ++x )
            //foreach (var x in Enumerable.Range((int)start, (int)Math.Sqrt(end) - (int)start))
            {
                if (nonPrimes.Contains(x))
                    continue;

                var x2 = x * x;

                for (long multiplier = 0; x2 + (multiplier * x) <= end; ++multiplier)
                    nonPrimes.Add(x2 + (multiplier * x));
            }

            var primes = new List<long>();

            for (var i = start; i < end; ++i)
                if (!nonPrimes.Contains(i))
                    primes.Add(i);

            return primes;
        }

    }
}
