using Inputs;

namespace ConsoleApp.Solutions
{
    public static class Day1
    {
        public static int? Part1()
        {
            var ints = DataExtractor.Day1();

            foreach (var i in ints)
            {
                foreach (var j in ints)
                {
                    if (i + j == 2020)
                    {
                        return i * j;
                    }
                }
            }

            return null;
        }

        public static int? Part2()
        {
            var ints = DataExtractor.Day1();

            foreach (var i in ints)
            {
                foreach (var j in ints)
                {
                    foreach (var k in ints)
                    {
                        if (i + j + k == 2020)
                        {
                            return i * j * k;
                        }
                    }
                }
            }

            return null;
        }
    }
}
