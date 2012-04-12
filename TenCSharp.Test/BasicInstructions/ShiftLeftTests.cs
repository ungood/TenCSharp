using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class ShiftLeftTests : BasicInstructionsTests
    {
        [Test]
        public void ShiftLeft()
        {
            Processor.Registers[Register.A] = 0x000f;
            Processor.Registers[Register.B] = 4;
            SetupInstruction(BasicOpcode.ShiftLeft);
            AssertResult(2, 0x00f0, 0);
        }

        [Test]
        public void ShiftLeftWithOverflow()
        {
            Processor.Registers[Register.A] = 0x0f0f;
            Processor.Registers[Register.B] = 12;
            SetupInstruction(BasicOpcode.ShiftLeft);
            AssertResult(2, 0xf000, 0x00f0);
        }
    }
}
