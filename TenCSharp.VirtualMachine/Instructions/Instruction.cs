using Word = System.UInt16;

namespace TenCSharp.VirtualMachine.Instructions
{
    public abstract class Instruction
    {
        public abstract void Execute(Processor processor);

        public static Instruction Decode(Word encoded)
        {
            var opcode = (byte)(encoded & 0x000F);

            if (opcode == 0)
                return NonBasicInstruction.Decode(encoded);
            else
                return BasicInstruction.Decode(opcode, encoded);
        }

        public static Word GetValue(Processor processor, byte valueCode)
        {

            //if (0x00 <= value && value <= 0x07)
            //    return processor => processor[(Register)value];
            //if (0x08 <= value && value <= 0x0f)
            //    return processor => processor.Memory[processor[(Register)value]];
            //if (0x10 <= value && value <= 0x17)
            //    return processor => processor.Memory[(Word)(processor.ProgramCounter++ + processor[(Register)value])];
            //if (value == 0x18)
            //    return processor => processor.Memory[processor.StackPointer++];
        }
    }
}
