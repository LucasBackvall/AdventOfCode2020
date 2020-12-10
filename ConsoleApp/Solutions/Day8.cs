using System.Collections.Generic;
using System.Net.NetworkInformation;
using Inputs;
using Inputs.MachineCode;

namespace ConsoleApp.Solutions
{
    public class Day8
    {
        public static int Part1()
        {
            var mem = DataExtractor.Day8();
            var state = new MachineState();
            var visited = new List<int>();

            while (!visited.Contains(state.ProgramCounter))
            {
                visited.Add(state.ProgramCounter);

                var instruction = mem[state.ProgramCounter];
                switch (instruction.Operation)
                {
                    case "nop":
                        state.ProgramCounter++;
                        continue;
                    
                    case "acc":
                        state.Accumilator += instruction.Argument;
                        state.ProgramCounter++;
                        continue;
                    
                    case "jmp":
                        state.ProgramCounter += instruction.Argument;
                        continue;
                }
            }

            return state.Accumilator;
        }

        public static int Part2()
        {
            return 0;
        }
        
        class MachineState
        {
            public int Accumilator { get; set; }
            
            public int ProgramCounter { get; set; }
        }
    }
}