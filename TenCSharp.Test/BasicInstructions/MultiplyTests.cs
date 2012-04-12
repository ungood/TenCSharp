using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class MultiplyTests : BasicInstructionsTests
    {
        [Test]
        public void Multiply()
        {
            Processor.Registers[Register.A] = 2;
            Processor.Registers[Register.B] = 3;
            SetupInstruction(BasicOpcode.Multiply);
            AssertResult(2, 6, 0);
        }

        [Test]
        public void MultiplyWithOverflow()
        {
            Processor.Registers[Register.A] = 65000;
            Processor.Registers[Register.B] = 10;
            SetupInstruction(BasicOpcode.Multiply);
            AssertResult(2, 0xeb10, 0x9);
        }
    }
}
