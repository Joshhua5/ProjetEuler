using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Tools;

namespace Question51
{
    class Program
    {

        static Dictionary<string, List<int[]>> IndexCombinationCache = new Dictionary<string, List<int[]>>();

        static IEnumerable<int[]> GetReplacementIndices(string value, int iterations)
        {
            if(!IndexCombinationCache.TryGetValue($"{iterations}:{value.Length}", out var indices))
            {
                int charConvert = 48; 
                indices =
                    Enumerable.Range(0, (int) Math.Pow(10, iterations + 1))  
                        .Select(x => x.ToString().Select(y => ((int)y) - charConvert).ToArray())
                        .Where(x => x.Length == iterations)
                        .Where(x => x.All(y => y < value.Length))
                        .Where(x => x.Distinct().Count() == iterations)
                        .ToList();
                 
                IndexCombinationCache.Add($"{iterations}:{value.Length}", indices);
            }


            return indices;
        }


        static void Main(string[] args)
        {
            var primes = Common.GetPrimesSieve(1_000, 1_000_000).Select(x => x.ToString()).ToArray();
            var groupedPrimes = primes.GroupBy(x => x.Length).ToArray();
            var primeLookup = primes.ToHashSet();
            
            var families = new Dictionary<string, string[]>();

            foreach (var prime in primes)
            {
                var pString = prime.ToString();
                var pArray = pString.ToCharArray();
                foreach (var iteration in Enumerable.Range(2, pString.Length - 2)) 
                {
                    var indexSets = GetReplacementIndices(pString, iteration); 
                    foreach (var set in indexSets)
                    {
                        var cArray = pString.ToCharArray();
                        foreach (var n in Enumerable.Range(0, 10).Select(x => (char) (x + '0')))
                        { 
                            var family = groupedPrimes
                                .FirstOrDefault(x => x.Key == pString.Length)
                                .Select(targets =>
                                {
                                    foreach (var i in set)
                                        cArray[i] = n;

                                    return new string(cArray);
                                })
                                .Where(x => primeLookup.Contains(x)) 
                                .ToArray();


                            // var family = GetReplacementIndices(pString, n, iterations) 
                            //     .Where(x => primeLookup.Contains(x))
                            //     .Distinct()
                            //     .ToArray();

                            if (family.Length >= 8)
                            {
                                if(family.Distinct().Count() != 8)
                                    continue;
                                
                                lock (families)
                                {
                                    families.Add($"{prime}:{n}:{iteration}", family);
                                }

                                Console.WriteLine(
                                    $"Prime: {pString}, Replacement: {n}, Family: {family.Aggregate((a, b) => $"{a} {b}")}");
                            }
                        }
                    }
                }
            }


            var min8Family = families
                .OrderBy(x => x.Key)
                .FirstOrDefault(x => x.Value.Length == 8);

            var minPrime = families
                .SelectMany(x => x.Value)
                .OrderBy(x => x)
                .FirstOrDefault();

            Console.WriteLine(min8Family.Key);

            Console.WriteLine(min8Family.Key);

            Console.WriteLine();
        } 
    } 
}
