using System;

using Word = System.UInt16;

namespace TenCSharp.VirtualMachine
{
    public abstract class VirtualMachineException : Exception
    {
        protected VirtualMachineException(string message)
            : base(message)
        {
        }
    }


    public class ReservedInstructionException : VirtualMachineException
    {
        public Word Instruction { get; set; }
        
        public ReservedInstructionException(Word instruction)
            : base("Instruction reserved for future expansion.")
        {
            Instruction = instruction;
        }
    }
}
