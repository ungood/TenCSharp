using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class DivideTests : BasicInstructionsTests
    {
        [Test]
        public void Divide()
        {
            Processor.Registers[Register.A] = 18;
            Processor.Registers[Register.B] = 5;
            SetupInstruction(BasicOpcode.Divide);
            Processor.Step();

            AssertResult(2, 3, 0);
        }

        [Test]
        public void DivideByZero()
        {
            Processor.Registers[Register.A] = 18;
            Processor.Registers[Register.B] = 0;
            SetupInstruction(BasicOpcode.Divide);
            Processor.Step();

            AssertResult(2, 0, 0);
        }

        [Test]
        public void DivideWithOverflow()
        {
            Processor.Registers[Register.A] = 65000;
            Processor.Registers[Register.B] = 10;
            SetupInstruction(BasicOpcode.Divide);
            Processor.Step();

            AssertResult(2, 0xeb10, 0x9);
        }
    }
}
