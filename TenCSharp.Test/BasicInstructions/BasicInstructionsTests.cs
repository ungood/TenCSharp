using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    [TestFixture]
    public abstract class BasicInstructionsTests
    {
        protected Processor Processor { get; private set; }

        [SetUp]
        public void Setup()
        {
            Processor = new Processor();
        }

        protected void SetupInstruction(BasicOpcode opcode)
        {
            var instruction = (ushort)((byte)opcode | (byte)Register.A << 4 | (byte)Register.B << 10);
            Processor.Memory[0] = instruction;
        }

        protected void AssertResult(int cycles, ushort result, ushort overflow)
        {
            Processor.Step();
            Assert.AreEqual(1, Processor.InstructionCount);
            Assert.AreEqual(cycles, Processor.CycleCount);
            Assert.AreEqual(result, Processor.Registers[Register.A]);
            Assert.AreEqual(overflow, Processor.Overflow);
        }
    }
}
