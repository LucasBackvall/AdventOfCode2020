using System.Collections.Generic;
using System.Linq;

namespace Inputs
{
    public class Input
    {
        public Input(string data)
        {
            Data = string.Concat(
                data.Where(x => x != '\r')
            );
        }

        public string Data { get; }

        public IEnumerable<Input> AsGroups()
        {
            return Data
                .Replace("\n\n", "~")
                .Split('~')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new Input(x));
        }

        public IEnumerable<string> AsStringGroups()
        {
            return AsGroups().AsStrings();
        }

        public IEnumerable<Input> AsLines()
        {
            return Data
                .Split('\n')
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new Input(x));
        }

        public IEnumerable<string> AsStringLines()
        {
            return AsLines().AsStrings();
        }

        public override string ToString()
        {
            return Data;
        }
    }

    public static class InputExtensions
    {
        public static IEnumerable<string> AsStrings(this IEnumerable<Input> input)
        {
            return input.Select(x => x.Data);
        }
    }
}
