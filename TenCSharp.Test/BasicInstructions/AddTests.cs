using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class AddTests : BasicInstructionsTests
    {
        [Test]
        public void Add()
        {
            Processor.Registers[Register.A] = 2;
            Processor.Registers[Register.B] = 3;
            SetupInstruction(BasicOpcode.Add);
            AssertResult(2, 5, 0);
        }

        [Test]
        public void AddWithOverflow()
        {
            Processor.Registers[Register.A] = ushort.MaxValue;
            Processor.Registers[Register.B] = 1;
            SetupInstruction(BasicOpcode.Add);
            AssertResult(2, 0, 1);
        }
    }
}
