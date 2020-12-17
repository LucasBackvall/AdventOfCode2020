using System.Linq;
using Inputs;

namespace ConsoleApp.Solutions
{
    public class Day9
    {
        public static long Part1()
        {
            var input = DataExtractor.Day9();
            var i = 25;
            long[] Preamble() => input
                .Skip(i - 25)
                .Take(25).ToArray();

            while (i < input.Length)
            {
                var valid = Preamble().Any(p =>
                    Preamble().Any(q =>
                        input[i] == p + q
                        && p != q
                    )
                );

                if (!valid)
                    return input[i];
                
                i++;
            }

            return -1;
        }

        public static long Part2()
        {
            return 0;
        }
    }
}