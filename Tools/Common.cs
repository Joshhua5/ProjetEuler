using System;
using System.Collections.Generic;
using System.Linq;

namespace Tools
{
    public static class Common
    {
        public static IEnumerable<long> GetPrimesSieve(int start, long end)
        {
            var nonPrimes = new HashSet<int>();

            foreach (var x in Enumerable.Range(start, (int)Math.Sqrt(end) - start))
            {
                if (nonPrimes.Contains(x))
                    continue;

                var x2 = x * x;

                for (var multiplier = 1; x2 + (multiplier * x) <= end; ++multiplier)
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
