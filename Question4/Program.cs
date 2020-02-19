using System;
using System.Linq;

namespace Question4
{
    class Program
    {

        static bool IsPalindrome(int value)
        {
            var str = value.ToString();
            var flip = new string(str.Reverse().ToArray());
            return str == flip;
        }


        static void Main(string[] args)
        {
            int largest = 0;
            foreach (var i in Enumerable.Range(100, 999 - 100))
            {
                foreach (var j in Enumerable.Range(100, 999 - 100))
                {
                    var value = i * j;
                    if (value > largest && IsPalindrome(value))
                        largest = value;
                }
            }


            Console.WriteLine($"Largest {largest}"); 
        }
    }
}
