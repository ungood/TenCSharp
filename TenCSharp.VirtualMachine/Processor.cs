using TenCSharp.VirtualMachine.Instructions;
using Word = System.UInt16;

namespace TenCSharp.VirtualMachine
{
    public enum Register
    {
        A = 0x00,
        B = 0x01,
        C = 0x02,
        X = 0x03,
        Y = 0x04,
        Z = 0x05,
        I = 0x06,
        J = 0x07,
    }

    public class Processor
    {
        private Word[] registers = new Word[8];

        public Memory Memory { get; private set; }

        public Word ProgramCounter { get; internal set; }
        public Word StackPointer { get; internal set; }
        public Word Overflow { get; internal set; }

        public Processor(Memory memory)
        {
            Memory = memory;
        }

        public void Reset()
        {
            registers = new Word[8];
            ProgramCounter = 0;
            StackPointer = 0;
            Overflow = 0;
        }

        public void Step()
        {
            var fetch = NextWord;
            var instruction = Instruction.Decode(fetch);
            instruction.Execute(this);
        }

        public Word this[Register register]
        {
            get { return registers[(int)register]; }
            internal set { registers[(int)register] = value; }
        }

        #region Stack Operations

        public Word Push
        {
            get { return Memory[StackPointer++]; }
            set { Memory[StackPointer++] = value; }
        }

        public Word Peek
        {
            get { return Memory[StackPointer]; }
            set { Memory[StackPointer] = value; }
        }

        public Word Pop
        {
            get { return Memory[--StackPointer]; }
            set { Memory[--StackPointer] = value; }
        }

        #endregion

        #region Program Counter

        public Word NextWord
        {
            get { return Memory[ProgramCounter++]; }
            set { /* Silent Fail */ }
        }

        public void Jump(Word address)
        {
            ProgramCounter = address;
        }

        #endregion
    }
}
