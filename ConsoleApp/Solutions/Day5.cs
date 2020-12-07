using System.Linq;
using Inputs;

namespace ConsoleApp.Solutions
{
    public static class Day5
    {
        public static int Part1()
        {
            return DataExtractor.Day5()
                .Select(x => x.Row * 8 + x.Col)
                .Max();
        }

        public static string Part2()
        {
            return DataExtractor.Day5()
                .Select(x =>
                    (
                        x.Row,
                        x.Col,
                        Id: x.Row * 8 + x.Col
                    )
                )
                .GroupBy(x => x.Row)
                .OrderBy(x => x.Key)
                .Single(x => x.Count() == 7)
                .Aggregate("", (acc, x) => $"{acc},{x.Row}:{x.Col}:{x.Id}");
        }
    }
}