using System.Collections.Generic;
using System.Linq;

namespace Inputs.BagRules
{
    public class BagRule
    {
        public string Bag { get; set; }

        public (string Bag, int Count)[] Contents { get; set; }
    }
}