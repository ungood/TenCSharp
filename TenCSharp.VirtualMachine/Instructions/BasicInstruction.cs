using System;
using System.Data;
using Word = System.UInt16;

namespace TenCSharp.VirtualMachine.Instructions
{
    public abstract class BasicInstruction : Instruction
    {
        protected InstructionValue A { get; private set; }
        protected InstructionValue B { get; private set; }

        public static BasicInstruction Decode(byte opcode, Word encoded)
        {
            var instruction = DecodeOpcode(opcode);
            instruction.A = InstructionValue.Decode((byte)((encoded >> 4) & 0x3f));
            instruction.B = InstructionValue.Decode((byte)((encoded >> 10) & 0x3f));
            return instruction;
        }

        private static BasicInstruction DecodeOpcode(byte opcode)
        {
            switch (opcode)
            {
                case 0x01:
                    return new SetInstruction();
                case 0x02:
                    return new AddInstruction();
                case 0x03:
                    return new SubtractInstruction();
                case 0x04:
                    return new MultiplyInstruction();
                case 0x05:
                    return new DivideInstruction();
                case 0x06:
                    return new ModulusInstruction();
                case 0x07:
                    return new ShiftLeftInstruction();
                case 0x08:
                    return new ShiftRightInstruction();
                case 0x09:
                    return new AndInstruction();
                case 0x0a:
                    return new BorInstruction();
                case 0x0b:
                    return new XorInstruction();
                default:
                    throw new NotImplementedException();
            }
        }
    }


    public class SetInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            A.Set(B.Get());
        }
    }

    public class AddInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var result = A.Get() + B.Get();
            A.Set((Word)result);
            processor.Overflow = (Word)(result >> 16);
        }
    }

    public class SubtractInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var result = A.Get() - B.Get();
            A.Set((Word)result);
            processor.Overflow = (Word)(result >> 16);
        }
    }

    public class MultiplyInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var result = A.Get() * B.Get();
            A.Set((Word)result);
            processor.Overflow = (Word)(result >> 16);
        }
    }

    public class DivideInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var b = B.Get();

            if (b == 0)
            {
                A.Set(0);
                processor.Overflow = 0;
                return;
            }

            var a = A.Get();
            B.Set((Word)(a / b));
            processor.Overflow = (Word)((a << 16) / b);
        }
    }

    public class ModulusInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var b = B.Get();

            if(b == 0)
            {
                A.Set(0);
                return;
            }

            A.Set((Word)(A.Get() % B.Get()));
        }
    }

    public class ShiftLeftInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var result = A.Get() << B.Get();
            A.Set((Word)result);
            processor.Overflow = (Word)(result >> 16);
        }
    }

    public class ShiftRightInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            var a = A.Get();
            var b = B.Get();

            var result = A.Get() >> b;
            A.Set((Word)result);
            processor.Overflow = (Word)((a << 16) >> b);
        }
    }

    public class AndInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            A.Set((Word)(A.Get() & B.Get()));
        }
    }

    public class BorInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            A.Set((Word)(A.Get() | B.Get()));
        }
    }

    public class XorInstruction : BasicInstruction
    {
        public override void Execute(Processor processor)
        {
            A.Set((Word)(A.Get() ^ B.Get()));
        }
    }
}
