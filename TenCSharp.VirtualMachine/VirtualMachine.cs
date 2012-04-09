namespace TenCSharp.VirtualMachine
{
    public class VirtualMachine
    {
        public Memory Memory { get; private set; }
        public Processor Processor { get; private set; }

        public VirtualMachine()
        {
            Memory = new Memory();
            Processor = new Processor(Memory);
        }
    }
}
