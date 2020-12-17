using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp.Machine;
using Inputs.MachineCode;

namespace ConsoleApp.Solutions
{
    public class Day8
    {
        public static int Part1()
        {
            var state = new MachineState();

            while (state.Log.All(x => state.ProgramCounter != x.ProgramCounter))
            {
                ExecuteInstruction(state);
            }

            return state.Accumilator;
        }

        public static int Part2()
        {
            // End state, the last state the machine would end up at before exiting successfully
            var endState = new MachineState(x =>
                {

                    x.ProgramCounter = 640;
                    x.ModifiedAt = null;
                    
                    // Here's a deleted section of code:
                    // 
                    // // Okay, sorry.
                    // // I could not resist replacing a simple boolean fixAppliedYet property in the state class
                    // // with a pointlessly dumb "variable" in the state memory.
                    // x.Memory[622] = null;
                    // // I promise I only do this in puzzle-code.
                    // 
                    // Yes this caused a bug.
                }
            );

            var stateTree = CreateStateTreeFromEndState(endState);

            var anySolution = ContainsAddressZero(stateTree);
            
            var path = FindPathToAddressZero(stateTree);

            var fixedState = new MachineState(x =>
            {
                var modifiedAt = path.FirstOrDefault()?.ModifiedAt ?? 0;

                x.Memory[modifiedAt].Operation
                    = x.Memory[modifiedAt].Operation == "nop"
                        ? "jmp"
                        : "nop";
            });

            while (fixedState.Log.All(x => fixedState.ProgramCounter != x.ProgramCounter)
                   && fixedState.ProgramCounter < fixedState.Memory.Length)
            {
                ExecuteInstruction(fixedState);
            }

            return fixedState.Accumilator;
        }

        private static MachineState CreateStateTreeFromEndState(MachineState state)
        {
            if (state.ProgramCounter == 0)
                return state;

            // Of all instructions
            var paths = state.Memory
                .Where(x =>
                    x.Operation == "jmp" && (
                        // Either direct path from JMP instruction
                        state.ProgramCounter == x.Address + x.Argument
                        // Or change the JMP to a NOP and traverse that path
                        || (
                            state.ModifiedAt == null
                            && state.ProgramCounter == x.Address + 1
                        )
                    )
                    ||
                    x.Operation != "jmp" && (
                        // Either direct path from non-JMP instruction
                        state.ProgramCounter == x.Address + 1
                        // Or change a NOP to become a JMP and traverse that path
                        || (
                            x.Operation == "nop"
                            && state.ModifiedAt == null
                            && state.ProgramCounter == x.Address + x.Argument
                        )
                    )
                )
                .Select(x =>
                {
                    if ( // this path requires this instruction to be switched...
                        (
                            x.Operation == "jmp"
                            && state.ProgramCounter != x.Address + x.Argument
                            && x.Address + 1 == state.ProgramCounter
                        )
                        ||(
                            x.Operation == "nop"
                            && state.ProgramCounter != x.Address + 1
                            && state.ProgramCounter == x.Address + x.Argument
                        )
                    )
                    {
                        state.ModifiedAt = x.Address;
                    }

                    return CreateStateTreeFromEndState(
                        new MachineState(y =>
                        {
                            y.ProgramCounter = x.Address;
                            y.ModifiedAt = state.ModifiedAt;
                        })
                    );
                })
                .ToList();
            
            state.Log.AddRange(
                paths
            );

            return state;
        }

        private static List<MachineState> FindPathToAddressZero(MachineState state)
        {
            return state.ProgramCounter == 0
                ? new List<MachineState> {state}
                : state.Log
                    .Where(ContainsAddressZero)
                    .SelectMany(x =>
                    {
                        var path = FindPathToAddressZero(x);
                        path.Add(state);

                        return path;
                    })
                    .ToList();
        }

        private static bool ContainsAddressZero(MachineState state)
        {
            return state.ProgramCounter == 0
                   || state.Log.Any(ContainsAddressZero);
        }

        /// <summary>
        /// This method was used because I thought it would be trivial to debug the code and find the error.
        /// It was not.
        /// </summary>
        public static string Part2_Debug(Action<MachineState> action = null)
        {
            var state = new MachineState();
            
            action?.Invoke(state);

            while (state.Log.All(x => state.ProgramCounter != x.ProgramCounter))
            {
                ExecuteInstruction(state);
            }

            var result = state.Log
                .OrderBy(x => x.ProgramCounter)
                .Select(x =>
                    $"\n{x.ProgramCounter}: {x.Instruction.Operation} {x.Instruction.Argument};"
                    + $"{(x.Instruction.Operation == "nop" && x.Instruction.Argument > 0 ? "<--------------" : "")}"
                )
                .Aggregate("\n", (acc, x) => $"{acc}{x}");

            return new MachineState().Memory
                .Select((instruction, pc) => new
                    {
                        Index = pc,
                        Instruction = instruction,
                        Visited = state.Log.FindIndex(x => x.ProgramCounter == pc)
                    }
                )
                .Aggregate("", (acc, x) =>
                    $"{acc}{(acc == "" ? "" : "\n")}{Foo(x.Index, x.Instruction, x.Visited)}"
                );
        }

        private static string Foo(int index, MachineCodeInstruction instruction, int visited)
        {
            return
                $"{index}: {instruction} {(visited == -1 ? "" : $"({visited})")}"
                + $"{(instruction.Operation == "nop" && instruction.Argument > 0 && visited != -1 ? " <- NOP" : "")}"
                + $"{(instruction.Operation == "jmp" && instruction.Argument < 0 && visited != -1 ? " <- JMP" : "")}"
                + $"{(instruction.Operation == "jmp" && instruction.Argument > 0 && visited == -1 ? " <- BIG" : "")}";
        }

        public static string Memdump()
        {
            var state = new MachineState();

            var pc = 0;
            var result = "\n";

            foreach (var instruction in state.Memory)
            {
                result = UpdateResult(result, instruction);
            }

            return result;
        }

        private static string UpdateResult(string result, MachineCodeInstruction instruction)
        {
            result +=
                $"\n{instruction.Operation} {instruction.Argument};"
                + $"{(instruction.Operation == "nop" && instruction.Argument > 0 ? "<--------------" : "")}";
            return result;
        }

        private static void ExecuteInstruction(MachineState state)
        {
            state.LogState();

            switch (state.Instruction.Operation)
            {
                case "nop":
                    state.ProgramCounter++;
                    return;

                case "acc":
                    state.Accumilator += state.Instruction.Argument;
                    state.ProgramCounter++;
                    return;

                case "jmp":
                    state.ProgramCounter += state.Instruction.Argument;
                    return;
            }
        }
    }
}