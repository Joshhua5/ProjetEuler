using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using Tools;

namespace Question51
{
    class Program
    {

        static readonly Dictionary<string, int[][]> IndexCombinationCache = new Dictionary<string, int[][]>();

        static int[][] GetReplacementIndices(int valueLength, int iterations)
        {
            if (!IndexCombinationCache.TryGetValue($"{iterations}:{valueLength}", out var indices))
            {
                indices =
                    Enumerable.Range(0, (int)Math.Pow(10, iterations + 1))  // Get a set of values which will contain a subset of all possible indices
                        .Select(x => x.ToString().Select(y => (int)y - '0').ToArray()) // Convert each character into a index 0-9 
                        .Where(x => x.Length == iterations) // Limit the index count to how many iterations we're using
                        .Where(x => x.All(y => y < valueLength)) // Remove all combinations that contain a index too large for our value.
                        .Where(x => x.Distinct().Count() == iterations)
                        .Select(x => new
                        {
                            identifier = x.OrderBy(y => y).Select(y => y.ToString()).Aggregate((a, b) => $"{a}{b}"),
                            sequence = x
                        }) // Construct a ordered string, that we can use to remove duplicates
                        .GroupBy(x => x.identifier) // Remove duplicates using the new identifier
                        .Select(x => x.First().sequence)
                        .ToArray();

                IndexCombinationCache.Add($"{iterations}:{valueLength}", indices);
            }
             
            return indices;
        }

        /// <summary>
        /// Generate a list of values using the prime as a base and set of replacement indices to replace.
        /// </summary>
        /// <returns></returns>
        static IEnumerable<int> GetModifiedValues(ReadOnlySpan<char> prime, int[] replacementIndex)
        {
            var constructedPrimes = new int[10];

            for (var i = 0; i <= 9; ++i)
            {
                for (var pos = 0; pos < prime.Length; ++pos)
                {
                    //Shift the last digit we worked on up a position
                    constructedPrimes[i] *= 10;

                    var replaced = false;

                    for (var j = 0; j < replacementIndex.Length; ++j)
                        replaced |= replacementIndex[j] == pos;

                    constructedPrimes[i] += replaced ? i : (prime[pos] - '0');
                }
            }
 
            return constructedPrimes;
        }


        static void Main(string[] args)
        {
            var primes = Common.GetPrimesSieve(2, 10_000_000).ToArray();
            var primeLookup = primes.ToHashSet();

            var iterations = new[] { 2, 3, 4, 5};

            foreach (var iteration in iterations)
            {
                foreach (var prime in primes.Select(x => x.ToString()).Where(x => x.Length > iteration))
                {
                    var primeSpan = prime.AsSpan();

                    var indices = GetReplacementIndices(primeSpan.Length, iteration);
                    foreach (var index in indices)
                    {
                        var family = GetModifiedValues(primeSpan, index)
                            .Where(x => primeLookup.Contains(x));
                          
                        if (family.Count() == 8)
                        {
                            var familyString = family.Select(x => x.ToString()).ToArray();
                            // Validate that no numbers have had 0 placed in front
                            if (familyString.Any(x => x.Length != prime.Length) | !familyString.Contains(prime))
                                continue;

                            Console.WriteLine($"{prime} : {iteration} | Family {familyString.Aggregate((a, b) => $"{a}, {b}")}");
                            return;
                        }
                    }
                }
            } 
        } 
    }
}
