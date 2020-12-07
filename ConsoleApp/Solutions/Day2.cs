using Inputs;

namespace ConsoleApp.Solutions
{
    public static class Day2
    {
        public static int Part1()
        {
            var validCount = 0;
            var data = DataExtractor.Day2();
            foreach (var (low, high, letter, password) in data)
            {
                var count = password.Split(letter).Length - 1;
                if (count >= low && count <= high)
                {
                    validCount++;
                }
            }

            return validCount;
        }

        public static int Part2()
        {
            var validCount = 0;
            var data = DataExtractor.Day2();
            foreach (var (low, high, letter, password) in data)
            {
                var atLow = password[low - 1] == letter;
                var atHigh = password[high - 1] == letter;
                if (atLow ^ atHigh)
                {
                    validCount++;
                }
            }

            return validCount;
        }
    }
}