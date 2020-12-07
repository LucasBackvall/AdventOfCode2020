using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Inputs;

namespace ConsoleApp.Solutions
{
    public static class Day6
    {
        public static int Part1()
        {
            return DataExtractor.Day6()
                .Sum(group =>
                {
                    var dict = new Dictionary<char, bool>();
                    foreach (var person in group)
                    {
                        foreach (var x in person)
                        {
                            dict[x] = true;
                        }
                    }

                    return dict.Count;
                });
        }

        public static int Part2()
        {
            return DataExtractor.Day6()
                .Sum(group =>
                {
                    var dict = new Dictionary<char, int>();
                    foreach (var person in group)
                    {
                        foreach (var x in person)
                        {
                            if (dict.TryGetValue(x, out var count))
                            {
                                dict[x] = count + 1;
                            }
                            else
                            {
                                dict.Add(x, 1);
                            }
                        }
                    }

                    return dict.Count(x => x.Value == group.Count);
                });
        }
    }
}
