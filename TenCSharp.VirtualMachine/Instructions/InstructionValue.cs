using System;
using Word = System.UInt16;

namespace TenCSharp.VirtualMachine.Instructions
{
    public abstract class InstructionValue
    {
        public abstract Word Get(Processor processor);
        public abstract void Set(Processor processor, Word value);

        public static InstructionValue Decode(byte value)
        {
            //if (0x00 <= value && value <= 0x07)
            //    return processor => processor[(Register)value];
            //if (0x08 <= value && value <= 0x0f)
            //    return processor => processor.Memory[processor[(Register)value]];
            //if (0x10 <= value && value <= 0x17)
            //    return processor => processor.Memory[(Word)(processor.ProgramCounter++ + processor[(Register)value])];
            //if (value == 0x18)
            //    return processor => processor.Memory[processor.StackPointer++];
            return new DirectRegisterValue();
        }
    }

    public class DirectRegisterValue : InstructionValue
    {
        public Register Register { get; private set; }

        public override Word Get(Processor processor)
        {
            return processor[Register];
        }

        public override void Set(Processor processor, Word value)
        {
            processor[Register] = value;
        }
    }

    public class IndirectRegisterValue : InstructionValue
    {
        public Register Register { get; private set; }

        public override Word Get(Processor processor)
        {
            return processor.Memory[processor[Register]];
        }

        public override void Set(Processor processor, Word value)
        {
            processor.Memory[processor[Register]] = value;
        }
    }

    public class PushValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.Push;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.Push = value;
        }
    }

    public class PeekValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.Peek;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.Peek = value;
        }
    }

    public class PopValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.Pop;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.Peek = value;
        }
    }

    public class StackPointerValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.StackPointer;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.StackPointer = value;
        }
    }

    public class ProgramCounterValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.ProgramCounter;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.ProgramCounter = value;
        }
    }

    public class OverflowValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.Overflow;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.Overflow = value;
        }
    }

    public class ImmediateLiteralValue : InstructionValue
    {
        public override Word Get(Processor processor)
        {
            return processor.NextWord;
        }

        public override void Set(Processor processor, Word value)
        {
            processor.NextWord = value;
        }
    }
}
