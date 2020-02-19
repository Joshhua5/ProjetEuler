using System;
using System.Linq;
using System.Xml.Serialization;

namespace Question5
{
    class Program
    {
        static void Main(string[] args)
        {
            long value = 0;

            do {
                value++; 
            } while (Enumerable.Range(1, 19).Any(x => value % x != 0));

            Console.WriteLine(value);
        }
    }
}
