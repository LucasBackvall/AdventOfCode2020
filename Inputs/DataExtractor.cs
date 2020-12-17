using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Inputs.BagRules;
using Inputs.MachineCode;

namespace Inputs
{
    public static class DataExtractor
    {
        public static int[] Day1()
        {
            return GetTextFile("Day1")
                .AsStringLines()
                .Select(int.Parse)
                .ToArray();
        }

        public static (int Low, int High, char Letter, string Password)[] Day2()
        {
            return GetTextFile("Day2")
                .AsStringLines()
                .Select(row =>
                    {
                        var low = int.Parse(row.Split('-')[0]);
                        var high = int.Parse(row.Split('-')[1].Split(' ')[0]);
                        var letter = row.Split(' ')[1][0];
                        var password = row.Split(' ')[2];

                        return (low, high, letter, password);
                    })
                    .ToArray();
        }

        public static string[] Day3()
        {
            return GetTextFile("Day3")
                .AsStringLines()
                .ToArray();
        }

        public static Dictionary<string, string>[] Day4()
        {
            return GetTextFile("Day4")
                .AsStringGroups()
                .Select(x =>
                {
                    var group = x.Replace('\n', ' ');
                    var dict = new Dictionary<string, string>();
                    foreach (var keyValue in group.Split(' ').Where(y => !string.IsNullOrWhiteSpace(y)))
                    {
                        dict.Add(keyValue.Split(':')[0], keyValue.Split(':')[1]);
                    }

                    return dict;
                })
                .ToArray();
        }

        public static (int Row, int Col)[] Day5()
        {
            return GetTextFile("Day5")
                .AsStringLines()
                .Select(x =>
                    (
                        Convert.ToInt32(
                            x.Substring(0, 7)
                                .Replace('F', '0')
                                .Replace('B', '1'),
                            2
                        ),
                        Convert.ToInt32(
                            x.Substring(7, 3)
                                .Replace('L', '0')
                                .Replace('R', '1'),
                            2
                        )
                    )
                ).ToArray();
        }

        public static List<List<string>> Day6()
        {
            return GetTextFile("Day6")
                .AsGroups()
                .Select(x =>
                    x.AsStringLines().ToList()
                )
                .ToList();
        }

        public static BagRule[] Day7(string testData = null)
        {
            return
                (
                    testData != null
                        ? GetInputFromString(testData)
                        : GetTextFile("Day7")
                )
                .AsStringLines()
                .Select(rule =>
                {
                    var split = rule.Replace("contain", "~").Split('~');

                    if (split[1].Trim().StartsWith("no"))
                    {
                        return new BagRule
                        {
                            Bag = Bag(split[0])
                        };
                    }
                    
                    return new BagRule
                    {
                        Bag = Bag(split[0]),
                        Contents = split[1]
                            .Split(',')
                            .Select(Bag)
                            .Select(x =>
                                (Bag(x.Substring(2)), int.Parse(x[0].ToString())))
                            .ToArray()
                    };
                })
                .ToArray();

            string Bag(string bag)
            {
                return bag
                    .Replace(" bags", "")
                    .Replace(" bag", "")
                    .Replace(".", "")
                    .Trim();
            }
        }

        public static MachineCodeInstruction[] Day8()
        {
            return GetTextFile("Day8")
                .AsStringLines()
                .Select((row, address) =>
                    new MachineCodeInstruction
                    {
                        Address = address,
                        Operation = row.Split(' ').FirstOrDefault(),
                        Argument = int.Parse(
                            row
                                .Split(' ')
                                .LastOrDefault()?
                                .Trim()
                                ?? ""
                        )
                    }
                ).ToArray();
        }

        private static Input GetTextFile(string filename)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = string.Format("Inputs.Raw.{0}.txt", filename);
            string result;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return new Input(result);
        }

        private static Input GetInputFromString(string input)
        {
            return new Input(input);
        }
    }
}
