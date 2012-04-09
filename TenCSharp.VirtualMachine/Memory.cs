using Word = System.UInt16;

namespace TenCSharp.VirtualMachine
{
    public class Memory
    {
        private Word[] ram = new Word[0xffff];

        public Word this[Word address]
        {
            get
            {
                return ram[address];
            }
            set
            {
                ram[address] = value;
            }
        }

        public void Reset()
        {
            ram = new Word[0xffff];
        }
    }
}
