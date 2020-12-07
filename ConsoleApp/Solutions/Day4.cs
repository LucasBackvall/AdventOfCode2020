using System.Linq;
using Inputs;

namespace ConsoleApp.Solutions
{
    public static class Day4
    {
        public static int Part1()
        {
            var data = DataExtractor.Day4();
            var count = 0;

            foreach (var dict in data)
            {
                if (
                    dict.TryGetValue("byr", out _)
                    && dict.TryGetValue("iyr", out _)
                    && dict.TryGetValue("eyr", out _)
                    && dict.TryGetValue("hgt", out _)
                    && dict.TryGetValue("hcl", out _)
                    && dict.TryGetValue("ecl", out _)
                    && dict.TryGetValue("pid", out _)
                )
                {
                    count++;
                }
            }

            return count;
        }

        public static int Part2()
        {
            var data = DataExtractor.Day4();
            var count = 0;

            foreach (var dict in data)
            {
                if (
                    dict.TryGetValue("byr", out var byr)
                    && byr.Length == 4 && int.Parse(byr) >= 1920 && int.Parse(byr) <= 2002
                    && dict.TryGetValue("iyr", out var iyr)
                    && iyr.Length == 4 && int.Parse(iyr) >= 2010 && int.Parse(iyr) <= 2020
                    && dict.TryGetValue("eyr", out var eyr)
                    && eyr.Length == 4 && int.Parse(eyr) >= 2020 && int.Parse(eyr) <= 2030
                    && dict.TryGetValue("hgt", out var hgt)
                    && (
                        (hgt.Contains("cm") && int.Parse(hgt.Replace("cm", "")) >= 150 && int.Parse(hgt.Replace("cm", "")) <= 193)
                        || (hgt.Contains("in") && int.Parse(hgt.Replace("in", "")) >= 59 && int.Parse(hgt.Replace("in", "")) <= 76)
                    )
                    && dict.TryGetValue("hcl", out var hcl)
                    && hcl.StartsWith('#') && hcl.Substring(1).Length == 6 && hcl.Substring(1).All(x =>
                        int.TryParse($"{x}", out _) || x == 'a' || x == 'b' || x == 'c' || x == 'd' || x == 'e' || x == 'f'
                    )
                    && dict.TryGetValue("ecl", out var ecl)
                    && (ecl == "amb" || ecl == "blu" || ecl == "brn" || ecl == "gry" || ecl == "grn" || ecl == "hzl" || ecl == "oth")
                    && dict.TryGetValue("pid", out var pid)
                    && pid.Length == 9 && int.TryParse(pid, out _)
                )
                {
                    count++;
                }
            }

            return count;
        }
    }
}