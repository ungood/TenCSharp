using NUnit.Framework;
using TenCSharp.VirtualMachine;

namespace TenCSharp.Test.BasicInstructions
{
    public class SetTests : BasicInstructionsTests
    {
        [Test]
        public void Set()
        {
            Processor.Registers[Register.B] = 0xf0f0;
            SetupInstruction(BasicOpcode.Set);
            AssertResult(1, 0xf0f0, 0);
        }
    }
}
