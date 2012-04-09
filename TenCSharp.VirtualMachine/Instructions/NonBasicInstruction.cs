using Word = System.UInt16;

namespace TenCSharp.VirtualMachine.Instructions
{
    public abstract class NonBasicInstruction : Instruction
    {
        public InstructionValue A { get; private set; }

        public new static NonBasicInstruction Decode(Word encoded)
        {
            var opcode = (byte)((encoded >> 4) & 0x3f);
            if(opcode == 0x01)
                return new JumpSubRoutineInstruction();

            throw new ReservedInstructionException(encoded);
        }
    }

    public class JumpSubRoutineInstruction : NonBasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var a = A.Get(processor);
            processor.Push = a;
            processor.Jump(a);
        }
    }
}
