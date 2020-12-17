using System;
using System.Collections.Generic;
using Inputs;
using Inputs.MachineCode;

namespace ConsoleApp.Machine
{
    public class MachineState
    {
        public MachineState(Action<MachineState> action = null)
        {
            Memory = DataExtractor.Day8();
            
            action?.Invoke(this);
        }
        
        public MachineCodeInstruction[] Memory { get; }

        private MachineCodeInstruction _instruction;
        
        public MachineCodeInstruction Instruction => _instruction ?? Memory[ProgramCounter];

        public int Accumilator { get; set; }
        
        public int ProgramCounter { get; set; }

        public int? ModifiedAt { get; set; }

        public List<MachineState> Log { get; } = new List<MachineState>();

        public void LogState()
        {
            Log.Add(new MachineState
            {
                _instruction = Instruction.Clone(),
                Accumilator = Accumilator,
                ProgramCounter = ProgramCounter
            });
        }
    }
}
