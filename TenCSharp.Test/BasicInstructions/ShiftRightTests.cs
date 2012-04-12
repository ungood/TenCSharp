using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class ShiftRightTests : BasicInstructionsTests
    {
        [Test]
        public void ShiftRight()
        {
            Processor.Registers[Register.A] = 0x00f0;
            Processor.Registers[Register.B] = 4;
            SetupInstruction(BasicOpcode.ShiftRight);
            AssertResult(2, 0x000f, 0);
        }

        [Test]
        public void ShiftRightWithOverflow()
        {
            Processor.Registers[Register.A] = 0x0f0f;
            Processor.Registers[Register.B] = 4;
            SetupInstruction(BasicOpcode.ShiftRight);
            AssertResult(2, 0x00f0, 0xf000);
        }
    }
}
