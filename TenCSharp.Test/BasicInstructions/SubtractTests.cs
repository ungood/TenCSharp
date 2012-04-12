using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class SubtractTests : BasicInstructionsTests
    {
        [Test]
        public void Subtract()
        {
            Processor.Registers[Register.A] = 5;
            Processor.Registers[Register.B] = 3;
            SetupInstruction(BasicOpcode.Subtract);
            Processor.Step();

            AssertResult(2, 2, 0);
        }

        [Test]
        public void SubtractWithOverflow()
        {
            Processor.Registers[Register.A] = 2;
            Processor.Registers[Register.B] = 3;
            SetupInstruction(BasicOpcode.Subtract);
            Processor.Step();

            AssertResult(2, 1, ushort.MaxValue);
        }
    }
}
