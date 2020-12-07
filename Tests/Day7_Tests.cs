using Inputs;
using Inputs.BagRules;
using NUnit.Framework;

namespace Tests.Day7
{
    [TestFixture]
    public class FixtureDay7
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Example_Part2_Input_extraction()
        {
            var expected = ExampleData.Extraction1;
            var result = DataExtractor.Day7(ExampleData.Input1);

            Assert.AreEqual(expected.Length, result.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i].Bag, result[i].Bag);
                Assert.AreEqual(expected[i].Contents?.Length, result[i].Contents?.Length);

                for (int j = 0; j < (expected[i].Contents?.Length ?? 0); j++)
                {
                    Assert.AreEqual(expected[i].Contents[j], result[i].Contents[j]);
                }
            }
        }

        [Test]
        public void Example_Part1()
        {
            var solution = ConsoleApp.Solutions.Day7.Part1(ExampleData.Input1);

            Assert.AreEqual(4, solution);
        }

        [Test]
        public void Example_Part2()
        {
            var solution = ConsoleApp.Solutions.Day7.Part2(ExampleData.Input2);

            Assert.AreEqual(126, solution);
        }

        static class ExampleData
        {
            public static string Input1
                = "light red bags contain 1 bright white bag, 2 muted yellow bags.\n"
                  + "dark orange bags contain 3 bright white bags, 4 muted yellow bags.\n"
                  + "bright white bags contain 1 shiny gold bag.\n"
                  + "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.\n"
                  + "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.\n"
                  + "dark olive bags contain 3 faded blue bags, 4 dotted black bags.\n"
                  + "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.\n"
                  + "faded blue bags contain no other bags.\n"
                  + "dotted black bags contain no other bags.";

            public static readonly BagRule[] Extraction1
                = new[]
                {
                    new BagRule
                    {
                        Bag = "light red",
                        Contents = new[]
                        {
                            ("bright white", 1),
                            ("muted yellow", 2)
                        }
                    },
                    new BagRule
                    {
                        Bag = "dark orange",
                        Contents = new[]
                        {
                            ("bright white", 3),
                            ("muted yellow", 4)
                        }
                    },
                    new BagRule
                    {
                        Bag = "bright white",
                        Contents = new[]
                        {
                            ("shiny gold", 1)
                        }
                    },
                    new BagRule
                    {
                        Bag = "muted yellow",
                        Contents = new[]
                        {
                            ("shiny gold", 2),
                            ("faded blue", 9)
                        }
                    },
                    new BagRule
                    {
                        Bag = "shiny gold",
                        Contents = new[]
                        {
                            ("dark olive", 1),
                            ("vibrant plum", 2)
                        }
                    },
                    new BagRule
                    {
                        Bag = "dark olive",
                        Contents = new[]
                        {
                            ("faded blue", 3),
                            ("dotted black", 4)
                        }
                    },
                    new BagRule
                    {
                        Bag = "vibrant plum",
                        Contents = new[]
                        {
                            ("faded blue", 5),
                            ("dotted black", 6)
                        }
                    },
                    new BagRule
                    {
                        Bag = "faded blue",
                        Contents = null
                    },
                    new BagRule
                    {
                        Bag = "dotted black",
                        Contents = null
                    }
                };

                public static string Input2
                    = "shiny gold bags contain 2 dark red bags.\n"
                      + "dark red bags contain 2 dark orange bags.\n"
                      + "dark orange bags contain 2 dark yellow bags.\n"
                      + "dark yellow bags contain 2 dark green bags.\n"
                      + "dark green bags contain 2 dark blue bags.\n"
                      + "dark blue bags contain 2 dark violet bags.\n"
                      + "dark violet bags contain no other bags.\n";
            }
    }
}
