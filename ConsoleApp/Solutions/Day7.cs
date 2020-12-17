using System.Collections.Generic;
using System.Linq;
using Inputs;
using Inputs.BagRules;

namespace ConsoleApp.Solutions
{
    public static class Day7
    {
        public static int Part1(string testData = null)
        {
            var rules = DataExtractor.Day7(testData);

            return GetParentBags("shiny gold", rules)
                .Distinct()
                .Count();
        }

        public static int Part2(string testData = null)
        {
            var rules = DataExtractor.Day7(testData);

            return Recursively("shiny gold", rules);
            
            static int Recursively(string bag, BagRule[] rules)
            {
                var rule = rules.SingleOrDefault(x => x.Bag == bag);
                
                return rule.Contents?
                    .Aggregate(
                        0,
                        (acc, x)
                            => acc + x.Count + x.Count * Recursively(x.Bag, rules)
                    ) ?? 0;
            }
        }

        private static List<string> GetParentBags(string bag, BagRule[] rules)
        {
            return Recursively(bag, rules, new List<string>());

            static List<string> Recursively(string bag, BagRule[] rules, List<string> result)
            {
                var applicableRules = rules
                    .Where(x =>
                        x.Contents != null
                        && x.Contents.Any(y => y.Bag == bag)
                    ).ToArray();

                foreach (var rule in applicableRules)
                {
                    result.Add(rule.Bag);
                    Recursively(rule.Bag, rules, result);
                }

                return result;
            }
        }
    }
}
