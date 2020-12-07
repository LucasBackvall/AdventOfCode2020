using System.Linq;
using Inputs;

namespace ConsoleApp.Solutions
{
    public static class Day3
    {
        public static int Part1()
        {
            var data = DataExtractor.Day3();
            return TreeCount(data, 3, 1);
        }

        public static decimal Part2()
        {
            var data = DataExtractor.Day3();

            var slopes = new []
            {
                TreeCount(data, 1, 1),
                TreeCount(data, 3, 1),
                TreeCount(data, 5, 1),
                TreeCount(data, 7, 1),
                TreeCount(data, 1, 2)
            };

            return slopes.Aggregate((decimal) 1, (acc, x) => acc * (decimal) x);
        }

        private static int TreeCount(string[] data, int xStep, int yStep)
        {
            bool Forest(int x, int y) => data[y][x % 31] == '#';

            var treeCount = 0;
            var x = 0;
            var y = 0;
            var outOfBounds = false;

            while (!outOfBounds)
            {
                try
                {
                    if (Forest(x, y))
                        treeCount++;

                    x += xStep;
                    y += yStep;
                }
                catch
                {
                    outOfBounds = true;
                }
            }

            return treeCount;
        }
    }
}