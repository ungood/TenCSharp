using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public class RegisterFile
    {
        private Word[] registers;

        public RegisterFile()
        {
            Reset();
        }

        public void Reset()
        {
            registers = new Word[8];
        }

        public Word this[Register register]
        {
            get { return registers[(int)register]; }
            set { registers[(int)register] = value; }
        }

        public Word this[int register]
        {
            get { return registers[register]; }
            set { registers[register] = value; }
        }
    }
}
