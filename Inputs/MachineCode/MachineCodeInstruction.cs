namespace Inputs.MachineCode
{
    public class MachineCodeInstruction
    {
        // Index in memory array
        public int Address { get; set; }
        
        public string Operation { get; set; }

        public int Argument { get; set; }

        public MachineCodeInstruction Clone()
        {
            return new MachineCodeInstruction
            {
                Operation = Operation,
                Argument = Argument
            };
        }

        public override string ToString()
        {
            return $"{Operation} {Argument}";
        }
    }
}