using System;
using System.Collections.Generic;
using Word = System.UInt16;

namespace TenCSharp.VirtualMachine
{
    public partial class Processor
    {
        private IEnumerator<Action> nextMicroInstruction; 

        public ALU ALU { get; private set; }
        public Memory Memory { get; private set; }
        public RegisterFile Registers { get; private set; }

        public Word ProgramCounter { get; private set; }
        public Word StackPointer { get; private set; }
        public Word Overflow { get; private set; }

        public bool Halt { get; private set; }

        public long InstructionCount { get; private set; }
        public long CycleCount { get; private set; }

        public Processor(Memory memory = null)
        {
            ALU = new ALU();
            Registers = new RegisterFile();
            Memory = memory ?? new Memory();

            Reset();
        }

        public void Reset()
        {
            Registers.Reset();
            Memory.Reset();

            ProgramCounter = 0;
            StackPointer = 0;
            Overflow = 0;

            InstructionCount = 0;
            CycleCount = 0;

            Halt = false;
            nextMicroInstruction = ExecuteLoop().GetEnumerator();
        }

        public void Run()
        {
            while(!Halt)
                MicroStep();
        }

        public void Step()
        {
            var count = InstructionCount;
            while(InstructionCount == count)
                MicroStep();
        }

        /// <summary>
        /// Executes the next micro instruction
        /// </summary>
        public void MicroStep()
        {
            if(nextMicroInstruction.MoveNext())
                nextMicroInstruction.Current();
        }

        private IEnumerable<Action> ExecuteLoop()
        {
            while(true)
            {
                var instruction = NextWord;
                foreach(var micro in Decode(instruction))
                {
                    yield return micro;
                    CycleCount++;
                }

                InstructionCount++;
            }
        }
        
        private Word NextWord
        {
            get { return Memory[ProgramCounter++]; }
        }

        private void SetOverflow()
        {
            Overflow = (Word)(ALU.Output >> 16);
        }
    }
}
